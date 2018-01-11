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
		public List<ShortLLMemberUserDTO> GetLocalLibraries(Guid userId)
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

		public LongLLMemberUserDTO GetLibraryById(int libraryId, Guid userId)
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

		public LocalLibrary GetLibraryByIdForMembership(int libraryId, Guid userId)
		{
			using (var context = new AuthContext())
			{
				return context.LocalLibraries.FirstOrDefault(library => library.Id == libraryId);
			}
		}
	}
}