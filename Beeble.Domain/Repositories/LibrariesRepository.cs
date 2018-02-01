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

                if (bookBarcodes == null || localLibrary == null || (booksToBorrow.Count > 0 && libraryMember == null))
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
                        .FirstOrDefault(batch => batch.Books.Select(_book => _book.Id).Contains(book.Id));

                    batchOfBorrowedBooks.Books.Remove(book);

                    book.IsBorrowed = false;
                    context.Entry(batchOfBorrowedBooks).State = EntityState.Modified;
                }

                context.SaveChanges();
            }

            return true;
        }

        public List<LibraryBookDTO> GetBookList(string sortOption, bool descending, string searchQuery, int pageNumber, Guid? userId)
        {
            if (searchQuery == null)
                searchQuery = "";

            using (var context = new AuthContext())
            {
                var library = context.LocalLibraries
                    .FirstOrDefault(localLibrary => localLibrary.Administrators.Select(admin => admin.Id).ToList().Contains(userId.ToString()));

                var books = new List<Book>();

                if (sortOption == "name")
                {
                    if (descending)
                    {
                        books = context.Books
                            .Include(book => book.Author)
                            .Include(book => book.LocalLibrary)
                            .Where(book => book.Name.Contains(searchQuery))
                            .GroupBy(book => book.Name)
                    .Select(bookGroup => bookGroup.FirstOrDefault())
                            .OrderByDescending(book => book.Name).Skip(pageNumber * 10)
                                    //.Where(book => book.LocalLibrary == library)
                                    .OrderByDescending(book => book.Name)
                                    .Include(book => book.Author).Include(book => book.LocalLibrary).Take(10)
                                    .ToList();
                    }
                    else
                    {
                        books = context.Books
                            .Include(book => book.Author)
                            .Include(book => book.LocalLibrary)
                            .GroupBy(book => book.Name)
                    .Select(bookGroup => bookGroup.FirstOrDefault())
                            .Where(book => book.Name.Contains(searchQuery))
                            .OrderBy(book => book.Name).Skip(pageNumber * 10)

                                    //.Where(book => book.LocalLibrary == library)
                                    .OrderBy(book => book.Name)
                                    .Include(book => book.Author).Include(book => book.LocalLibrary).Take(10)
                                    .Include(book => book.Author)
                            .Include(book => book.LocalLibrary)
                                    .ToList();
                    }
                }
                else if (sortOption == "author")
                {
                    if (descending)
                    {
                        books = context.Books
                            .Include(book => book.Author)
                            .Include(book => book.LocalLibrary)
                            .GroupBy(book => book.Name)
                    .Select(bookGroup => bookGroup.FirstOrDefault())
                            .Where(book => book.Name.Contains(searchQuery))
                                                        .OrderByDescending(book => book.Author.Name).Skip(pageNumber * 10)
                                    //.Where(book => book.LocalLibrary == library)
                                    .OrderByDescending(book => book.Author.Name)
                                    .Include(book => book.Author).Include(book => book.LocalLibrary).Take(10)
                                    .ToList();
                    }
                    else
                    {
                        books = context.Books
                            .Include(book => book.Author)
                            .Include(book => book.LocalLibrary)
                            .GroupBy(book => book.Name)
                    .Select(bookGroup => bookGroup.FirstOrDefault())
                             .Where(book => book.Name.Contains(searchQuery))
                             .OrderBy(book => book.Author.Name).Skip(pageNumber * 10)
                                    //.Where(book => book.LocalLibrary == library)
                                    .OrderBy(book => book.Author.Name)
                                    .Include(book => book.Author).Include(book => book.LocalLibrary).Take(10)
                                    .ToList();
                    }
                }
                else if (sortOption == "quantity")
                {
                    if (descending)
                    {
                        books = context.Books
                            .Include(book => book.Author)
                            .Include(book => book.LocalLibrary)
                            .GroupBy(book => book.Name)
                    .Select(bookGroup => bookGroup.FirstOrDefault())
                             .Where(book => book.Name.Contains(searchQuery))
                             .OrderByDescending(book =>
                            context.Books
                                   .Where(_book => _book.Name == book.Name && _book.Author.Name == book.Author.Name/* && book.LocalLibrary.Id == library.Id*/)
                                   .ToList()
                                   .Count)
                                   .Skip(pageNumber * 10)
                            //.OrderByDescending(book => GetBooksInLibrary(book, library).Count)
                            .OrderByDescending(book =>
                            context.Books
                                   .Where(_book => _book.Name == book.Name && _book.Author.Name == book.Author.Name/* && book.LocalLibrary.Id == library.Id*/)
                                   .ToList()
                                   .Count)
                                   .Include(book => book.Author).Include(book => book.LocalLibrary).Take(10)
                            .ToList();
                    }
                    else
                    {
                        books = context.Books
                            .Include(book => book.Author)
                            .Include(book => book.LocalLibrary)
                            .GroupBy(book => book.Name)
                    .Select(bookGroup => bookGroup.FirstOrDefault())
                             .Where(book => book.Name.Contains(searchQuery))
                             .OrderBy(book =>
                            context.Books
                                   .Where(_book => _book.Name == book.Name && _book.Author.Name == book.Author.Name/* && book.LocalLibrary.Id == library.Id*/)
                                   .ToList()
                                   .Count)
                                   .Skip(pageNumber * 10)
                            .OrderBy(book =>
                            context.Books
                                   .Where(_book => _book.Name == book.Name && _book.Author.Name == book.Author.Name/* && book.LocalLibrary.Id == library.Id*/)
                                   .ToList()
                                   .Count)
                                   .Include(book => book.Author).Include(book => book.LocalLibrary).Take(10)
                            .ToList();
                    }
                }
                else if (sortOption == "reserved")
                {
                    if (descending)
                    {
                        books = context.Books
                            .Include(book => book.Author)
                            .Include(book => book.LocalLibrary)
                            .GroupBy(book => book.Name)
                    .Select(bookGroup => bookGroup.FirstOrDefault())
                             .Where(book => book.Name.Contains(searchQuery))
                             .OrderByDescending(book =>
                            context.Books
                                   .Where(_book => _book.Name == book.Name && _book.Author.Name == book.Author.Name/* && book.LocalLibrary.Id == library.Id*/)
                                   .Where(_book => _book.IsReserved)
                                   .ToList()
                                   .Count)
                                   .Skip(pageNumber * 10)
                            //.OrderByDescending(book => GetBooksInLibrary(book, library).Count)
                            .OrderByDescending(book =>
                            context.Books
                                   .Where(_book => _book.Name == book.Name && _book.Author.Name == book.Author.Name/* && book.LocalLibrary.Id == library.Id*/)
                                   .Where(_book => _book.IsReserved)
                                   .ToList()
                                   .Count)
                                   .Include(book => book.Author).Include(book => book.LocalLibrary).Take(10)
                            .ToList();
                    }
                    else
                    {
                        books = context.Books
                            .Include(book => book.Author)
                            .Include(book => book.LocalLibrary)
                            .GroupBy(book => book.Name)
                    .Select(bookGroup => bookGroup.FirstOrDefault())
                             .Where(book => book.Name.Contains(searchQuery))
                             .OrderBy(book =>
                            context.Books
                                   .Where(_book => _book.Name == book.Name && _book.Author.Name == book.Author.Name/* && book.LocalLibrary.Id == library.Id*/)
                                   .Where(_book => _book.IsReserved)
                                   .ToList()
                                   .Count)
                                   .Skip(pageNumber * 10)
                            .OrderBy(book =>
                            context.Books
                                   .Where(_book => _book.Name == book.Name && _book.Author.Name == book.Author.Name/* && book.LocalLibrary.Id == library.Id*/)
                                   .Where(_book => _book.IsReserved)
                                   .ToList()
                                   .Count)
                                   .Include(book => book.Author).Include(book => book.LocalLibrary).Take(10)
                            .ToList();
                    }
                }
                else if (sortOption == "borrowed")
                {
                    if (descending)
                    {
                        books = context.Books
                            .Include(book => book.Author)
                            .Include(book => book.LocalLibrary)
                            .GroupBy(book => book.Name)
                    .Select(bookGroup => bookGroup.FirstOrDefault())
                             .Where(book => book.Name.Contains(searchQuery))
                             .OrderByDescending(book =>
                            context.Books
                                   .Where(_book => _book.Name == book.Name && _book.Author.Name == book.Author.Name/* && book.LocalLibrary.Id == library.Id*/)
                                   .Where(_book => _book.IsBorrowed)
                                   .ToList()
                                   .Count)
                                   .Skip(pageNumber * 10)
                            //.OrderByDescending(book => GetBooksInLibrary(book, library).Count)
                            .OrderByDescending(book =>
                            context.Books
                                   .Where(_book => _book.Name == book.Name && _book.Author.Name == book.Author.Name/* && book.LocalLibrary.Id == library.Id*/)
                                   .Where(_book => _book.IsBorrowed)
                                   .ToList()
                                   .Count)
                                   .Include(book => book.Author).Include(book => book.LocalLibrary).Take(10)
                            .ToList();
                    }
                    else
                    {
                        books = context.Books
                            .Include(book => book.Author)
                            .Include(book => book.LocalLibrary)
                            .GroupBy(book => book.Name)
                    .Select(bookGroup => bookGroup.FirstOrDefault())
                             .Where(book => book.Name.Contains(searchQuery))
                             .OrderBy(book =>
                            context.Books
                                   .Where(_book => _book.Name == book.Name && _book.Author.Name == book.Author.Name/* && book.LocalLibrary.Id == library.Id*/)
                                   .Where(_book => _book.IsBorrowed)
                                   .ToList()
                                   .Count)
                                   .Skip(pageNumber * 10)
                            .OrderBy(book =>
                            context.Books
                                   .Where(_book => _book.Name == book.Name && _book.Author.Name == book.Author.Name/* && book.LocalLibrary.Id == library.Id*/)
                                   .Where(_book => _book.IsBorrowed)
                                   .ToList()
                                   .Count)
                                   .Include(book => book.Author).Include(book => book.LocalLibrary).Take(10)
                            .ToList();
                    }
                }
                else if (sortOption == "damage")
                {
                    if (descending)
                    {
                        books = context.Books
                            .Include(book => book.Author)
                            .Include(book => book.LocalLibrary)
                            .GroupBy(book => book.Name)
                    .Select(bookGroup => bookGroup.FirstOrDefault())
                             .Where(book => book.Name.Contains(searchQuery))
                             .OrderByDescending(book => book.DamageLevel)
                             .Skip(pageNumber * 10)
                            .OrderByDescending(book => book.DamageLevel)
                            .Include(book => book.Author).Include(book => book.LocalLibrary).Take(10)
                            .ToList();
                    }
                    else
                    {
                        books = context.Books
                            .Include(book => book.Author)
                            .Include(book => book.LocalLibrary)
                            .GroupBy(book => book.Name)
                    .Select(bookGroup => bookGroup.FirstOrDefault())
                             .Where(book => book.Name.Contains(searchQuery))
                             .OrderBy(book => book.DamageLevel)
                             .Skip(pageNumber * 10)
                            .OrderBy(book => book.DamageLevel)
                            .Include(book => book.Author).Include(book => book.LocalLibrary).Take(10)
                            .ToList();
                    }
                }

                var test = context.Books.GroupBy(x => x.Name).Select(y => y.FirstOrDefault()).Count();
                return books.
                    GroupBy(book => book.Name)
                    .Select(bookGroup => bookGroup.FirstOrDefault())
                    .Select(book => LibraryBookDTO.FromData(GetBooksInLibrary(book, library))).ToList();
            }
        }

        public List<Book> GetBooksInLibrary(Book book, LocalLibrary library)
        {
            using (var context = new AuthContext())
            {
                return context.Books
                    .Include(_book => _book.Author)
                    .Include(_book => _book.LocalLibrary)
                    .Where(_book => _book.Name == book.Name && _book.Author.Name == book.Author.Name/* && book.LocalLibrary.Id == library.Id*/)
                    .ToList();
            }
        }

        public List<LocalLibraryMemberDTO> GetMemberList(string sortOption, bool descending, string searchQuery, int pageNumber, Guid? userId)
        {

            if (searchQuery == null)
                searchQuery = "";

            using (var context = new AuthContext())
            {
                var library = context.LocalLibraries
                    .FirstOrDefault(localLibrary => localLibrary.Administrators.Select(admin => admin.Id).ToList().Contains(userId.ToString()));

                var members = new List<LocalLibraryMemberDTO>();

                var getMembersQuery = context.LocalLibraryMembers
                            .Include("BatchesOfBorrowedBooks")
                            .Include("BatchesOfBorrowedBooks.Books")
                            .Include("Reservations")
                            .Include("Reservations.Book")
                            .Include("BatchesOfBorrowedBooks.Books.Author")
                            //.Where(member => member.LocalLibrary == library)
                            .Where(member => member.Name.Contains(searchQuery) || member.LastName.Contains(searchQuery));

                if (sortOption == "name")
                {
                    if (descending)
                    {
                        getMembersQuery = getMembersQuery
                            .OrderByDescending(member => member.Name);
                    }
                    else
                    {
                        getMembersQuery = getMembersQuery
                            .OrderBy(member => member.Name);
                    }
                }
                else if (sortOption == "lastName")
                {
                    if (descending)
                    {
                        getMembersQuery = getMembersQuery
                            .OrderByDescending(member => member.LastName);
                    }
                    else
                    {
                        getMembersQuery = getMembersQuery
                            .OrderBy(member => member.LastName);
                    }
                }
                else if (sortOption == "borrowed")
                {
                    if (descending)
                    {
                        getMembersQuery = getMembersQuery
                            .OrderByDescending(member => member.BatchesOfBorrowedBooks.SelectMany(batch => batch.Books).ToList().Count);
                    }
                    else
                    {
                        getMembersQuery = getMembersQuery
                            .OrderBy(member => member.BatchesOfBorrowedBooks.SelectMany(batch => batch.Books).ToList().Count);
                    }
                }
                else if (sortOption == "reserved")
                {
                    if (descending)
                    {
                        getMembersQuery = getMembersQuery
                            .OrderByDescending(member => member.Reservations.Count);
                    }
                    else
                    {
                        getMembersQuery = getMembersQuery
                            .OrderBy(member => member.Reservations.Count);
                    }
                }
                else if (sortOption == "booksWithLateReturnFee")
                {
                    if (descending)
                    {
                        getMembersQuery = getMembersQuery
                            .OrderByDescending(member => member.BatchesOfBorrowedBooks.SelectMany(batch => batch.Books).Where(book => book.LateReturnFee > 0).ToList().Count);
                    }
                    else
                    {
                        getMembersQuery = getMembersQuery
                            .OrderBy(member => member.BatchesOfBorrowedBooks.SelectMany(batch => batch.Books).Where(book => book.LateReturnFee > 0).ToList().Count);
                    }
                }
                //else if (sortOption == "lateReturnFee")
                //{
                //    if (descending)
                //    {
                //        getMembersQuery = getMembersQuery

                //            //TotalLateReturnFee = borrowedBooks.Count == 0 ? 0 : borrowedBooks.Select(book => book.LateReturnFee).Aggregate((sum, ele) => sum += ele)

                //            .OrderByDescending(member => member.BatchesOfBorrowedBooks.SelectMany(batch => batch.Books).Select(book => book.LateReturnFee).ToList().Aggregate((sum, ele) => sum += ele))
                //    }
                //    else
                //    {
                //        getMembersQuery = getMembersQuery
                //            .OrderBy(member => member.BatchesOfBorrowedBooks.SelectMany(batch => batch.Books).Where(book => book.LateReturnFee > 0).ToList().Count);
                //    }
                //}

                members = getMembersQuery
                    .Skip(pageNumber * 10)
                            .Take(10)
                            .ToList()
                            .Select(LocalLibraryMemberDTO.FromData)
                            .ToList();

                return members;
            }
        }
    }
}