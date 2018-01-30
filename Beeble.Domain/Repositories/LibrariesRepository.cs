using Beeble.Data;
using Beeble.Data.Models;
using Beeble.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web.Http.Results;

namespace Beeble.Domain.Repositories
{
	public class LibrariesRepository
	{
		public List<ShortLLMemberUserDTO> GetLocalLibraries(Guid? userId)
		{
			using (var context = new AuthContext())
			{
				var localLibraryMembers = context.LocalLibraryMembers
					.Include(localLibraryMember => localLibraryMember.LocalLibrary)
					.Include(localLibraryMember => localLibraryMember.BatchesOfBorrowedBooks)
					.Include(localLibraryMember => localLibraryMember.Reservations)
					.Where(localLibraryMember => localLibraryMember.OnlineUser.Id == userId.ToString());

				var localLibraries = localLibraryMembers
					.ToList()
					.Select(ShortLLMemberUserDTO.FromData)
					.ToList();

				return localLibraries;
			}
		}

		public LongLLMemberUserDTO GetLibraryById(int libraryId, Guid? userId)
		{
			using (var context = new AuthContext())
			{
				return context.LocalLibraryMembers
					.Include("LocalLibrary")
					.Include("BatchesOfBorrowedBooks")
					.Include("Reservations")
					.Include("BatchesOfBorrowedBooks.Books")
                    .Include("BatchesOfBorrowedBooks.Books.Author")
                    .Include("Reservations.Book")
                    .Where(localLibraryMember => localLibraryMember.OnlineUser.Id == userId.ToString() &&
					                             localLibraryMember.LocalLibrary.Id == libraryId)
					.ToList().Select(LongLLMemberUserDTO.FromData)
					.FirstOrDefault();
			}
		}

		public LocalLibrary GetLibraryByIdForMembership(int libraryId, Guid? userId)
		{
			using (var context = new AuthContext())
			{
				return context.LocalLibraries.FirstOrDefault(library => library.Id == libraryId);
			}
		}

		public List<LocalLibrary> GetAll(Guid? userId)
		{
			using (var context = new AuthContext())
			{
				return context.LocalLibraries
					.Where(library => library.Members.All(member => member.OnlineUser.Id != userId.ToString()))
					.ToList();
			}
		}

        public bool EnrollToLibraryWithBarcode(int libraryId, string barcodeNumber, Guid? userId)
        {
            using (var context = new AuthContext())
            {
                var onlineUser = context.Users.FirstOrDefault(user => user.Id == userId.ToString());

                var localLibraryMember = context.LocalLibraryMembers.FirstOrDefault(member => member.BarcodeNumber == barcodeNumber && member.OnlineUser.Id != userId.ToString());

                // if the barcode inputted is incorrect
                if (localLibraryMember == null)
                    return false;

                localLibraryMember.OnlineUser = onlineUser;

                if (onlineUser.LocalLibraryMembers == null)
                    onlineUser.LocalLibraryMembers = new List<LocalLibraryMember>();

                onlineUser.LocalLibraryMembers.Add(localLibraryMember);

                context.Entry(localLibraryMember).State = EntityState.Modified;
                context.Entry(onlineUser).State = EntityState.Modified;
                context.SaveChanges();

                // return information that the method successfuly completed
                return true;
            }
        }

		public ShortLLMemberUserDTO GetMemberByBarcode(string memberBarcode)
		{
			using (var context = new AuthContext())
			{
				return context.LocalLibraryMembers
					.Include(localLibraryMember => localLibraryMember.LocalLibrary)
					.Include(localLibraryMember => localLibraryMember.BatchesOfBorrowedBooks)
					.Include(localLibraryMember => localLibraryMember.Reservations)
					.Where(member => member.BarcodeNumber == memberBarcode)
					.Select(ShortLLMemberUserDTO.FromData)
					.FirstOrDefault(); 
			}
		}

        public bool LendAndReturnScanned(List<string> bookBarcodes, string memberBarcode, Guid? UserId)
        {
            using (var context = new AuthContext())
            {
                var libraryMember = context.LocalLibraryMembers.FirstOrDefault(member => member.BarcodeNumber == memberBarcode);

                var localLibrary = context.LocalLibraries
                    .Include(library => library.Administrators)
                    .FirstOrDefault(library => library.Administrators
                                                        .Select(onlineUser => onlineUser.Id).ToList()
                                                        .Contains(UserId.ToString()));

                var booksToBorrow = context.Books
                    .Where(book => bookBarcodes.Contains(book.BarcodeNumber) && !book.IsBorrowed)
                    .ToList();

                var booksToReturn = context.Books
                    .Where(book => bookBarcodes.Contains(book.BarcodeNumber) && book.IsBorrowed)
                    .ToList();

                if (bookBarcodes == null || libraryMember == null || localLibrary == null)
                    return false;

                context.BatchesOfBorrowedBooks.Add(new BatchOfBorrowedBooks()
                {
                    Books = booksToBorrow,
                    PickupDate = DateTime.Now,
                    LibraryMember = libraryMember,
                    ReturnDeadline = DateTime.Now.AddDays(localLibrary.DefaultLendDuration),
                    WasPreviouslyReserved = booksToBorrow.Any(book => book.IsReserved)
                });

                foreach (var book in booksToBorrow)
                {
                    book.IsBorrowed = true;
                    context.Entry(book).State = EntityState.Modified;
                }

                foreach (var book in booksToReturn)
                {
                    var batchOfBorrowedBooks = context.BatchesOfBorrowedBooks
                        .FirstOrDefault(batch => batch.Books.Contains(book));

                    batchOfBorrowedBooks.Books.Remove(book);

                    book.IsBorrowed = false;
                    context.Entry(batchOfBorrowedBooks).State = EntityState.Modified;
                }

                context.SaveChanges();
            }

            return true;
        }

    }
}