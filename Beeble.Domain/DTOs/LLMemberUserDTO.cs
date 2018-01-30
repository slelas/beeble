using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

using Beeble.Data.Models;

namespace Beeble.Domain.DTOs
{
	public class ShortLLMemberUserDTO
	{
		public string LibraryName { get; set; }
		public int LibraryId { get; set; }
		public string MemberName { get; set; }
		public string MemberLastName { get; set; }
		public string MemberAddress { get; set; }
		public string MemberEmail { get; set; }
		public string MemberOib { get; set; }
		public DateTime? MembershipExpiryDate { get; set; }
		public int NumberOfBorrowedBooks { get; set; }
		public int NumberOfReservedBooks { get; set; }
		public DateTime? LastBorrowDate { get; set; }
        public string BarcodeNumber { get; set; }

        public static ShortLLMemberUserDTO FromData(LocalLibraryMember member)
		{
			return new ShortLLMemberUserDTO
			{
				LibraryName = member.LocalLibrary.Name,
				LibraryId = member.LocalLibrary.Id,
				MemberName = member.Name,
				MemberLastName = member.LastName,
				MemberAddress = member.Address,
				MemberEmail = member.Email,
				MemberOib = member.Oib,
				MembershipExpiryDate = member.MembershipExpiryDate,
				NumberOfBorrowedBooks = member.BatchesOfBorrowedBooks.Count,
				NumberOfReservedBooks = member.Reservations.Count,
				LastBorrowDate = member.BatchesOfBorrowedBooks.OrderBy(book => book.PickupDate).LastOrDefault()?.PickupDate,
                BarcodeNumber = member.BarcodeNumber
			};
		}

	}

	public class LongLLMemberUserDTO : ShortLLMemberUserDTO
	{
		public long MemberId { get; set; }
		public string LibraryOpenHours { get; set; }
		public string LibraryAddress { get; set; }
		public IEnumerable<ShortBookDTO> BorrowedBooks { get; set; }
		public IEnumerable<Reservation> Reservations { get; set; }
		public string LibraryNumber { get; set; }
		public string LibraryEmail { get; set; }

		public static LongLLMemberUserDTO FromData(LocalLibraryMember member)
		{

			return new LongLLMemberUserDTO()
			{
				MemberId = member.Id,
				LibraryName = member.LocalLibrary.Name,
				LibraryId = member.LocalLibrary.Id,
				MembershipExpiryDate = member.MembershipExpiryDate,
				NumberOfBorrowedBooks = member.BatchesOfBorrowedBooks.Count,/*SelectMany(batch => batch.Books).ToList().Count,*/
				NumberOfReservedBooks = member.Reservations.Count,
				LastBorrowDate = member.BatchesOfBorrowedBooks.OrderBy(book => book.PickupDate).LastOrDefault()?.PickupDate,
				LibraryOpenHours = member.LocalLibrary.OpenHours,
				LibraryAddress = member.LocalLibrary.Address,
				LibraryEmail = member.LocalLibrary.Email,
				LibraryNumber = member.LocalLibrary.Number,
				BorrowedBooks = member
					.BatchesOfBorrowedBooks
					.Select(batchOfBorrowedBooks => batchOfBorrowedBooks.Books.Select(book => ShortBookDTO.FromData(book, batchOfBorrowedBooks.ReturnDeadline)))
					.SelectMany(book => book)
					.ToList(),
  
                Reservations = member.Reservations
                    //.BatchesOfReservedBooks
                    //.Select(batchOfReservedBooks => batchOfReservedBooks.Books.Select(book => ShortBookDTO.FromData(book, batchOfReservedBooks.PickupDeadline)))
                    //.SelectMany(book => book)
                    //.ToList()
            };
		}

	}
}
