using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Beeble.Data.Models;

namespace Beeble.Domain.DTOs
{
	public class ShortLLMemberUserDTO
	{
		public string LibraryName { get; set; }
		public int LibraryId { get; set; }
		public DateTime? MembershipExpiryDate { get; set; }
		public int NumberOfBorrowedBooks { get; set; }
		public int NumberOfReservedBooks { get; set; }
		public DateTime? LastBorrowDate { get; set; }

		public static ShortLLMemberUserDTO FromData(LocalLibraryMember member)
		{
			return new ShortLLMemberUserDTO
			{
				LibraryName = member.LocalLibrary.Name,
				LibraryId = member.LocalLibrary.Id,
				MembershipExpiryDate = member.MembershipExpiryDate,
				NumberOfBorrowedBooks = member.BatchesOfBorrowedBooks.Count,
				NumberOfReservedBooks = member.BatchesOfReservedBooks.Count,
				LastBorrowDate = member.BatchesOfBorrowedBooks.OrderBy(book => book.PickupDate).LastOrDefault()?.PickupDate
			};
		}

	}

	public class LongLLMemberUserDTO : ShortLLMemberUserDTO
	{
		public string OpenHours { get; set; }
		public string Address { get; set; }
		public IEnumerable<ShortBookDTO> BorrowedBooks { get; set; }
		public IEnumerable<ShortBookDTO> ReservedBooks { get; set; }
		//public string Number { get; set; } //first add to models library
		//public string Email { get; set; } //first add to models library

		public static LongLLMemberUserDTO FromData(LocalLibraryMember member)
		{

			return new LongLLMemberUserDTO()
			{
				LibraryName = member.LocalLibrary.Name,
				LibraryId = member.LocalLibrary.Id,
				MembershipExpiryDate = member.MembershipExpiryDate,
				NumberOfBorrowedBooks = member.BatchesOfBorrowedBooks.Count,
				NumberOfReservedBooks = member.BatchesOfReservedBooks.Count,
				LastBorrowDate = member.BatchesOfBorrowedBooks.OrderBy(book => book.PickupDate).LastOrDefault()?.PickupDate,
				OpenHours = member.LocalLibrary.OpenHours,
				Address = member.LocalLibrary.Address,
				BorrowedBooks = member
					.BatchesOfBorrowedBooks
					.Select(batchOfBorrowedBooks => batchOfBorrowedBooks.Books.Select(book => ShortBookDTO.FromData(book, batchOfBorrowedBooks.ReturnDeadline)))
					.SelectMany(book => book)
					.ToList(),
  
                ReservedBooks = member
                    .BatchesOfReservedBooks
                    .Select(batchOfReservedBooks => batchOfReservedBooks.Books.Select(book => ShortBookDTO.FromData(book, batchOfReservedBooks.PickupDeadline)))
                    .SelectMany(book => book)
                    .ToList()
            };
		}

	}
}
