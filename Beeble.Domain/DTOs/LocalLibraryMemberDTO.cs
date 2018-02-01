using Beeble.Data;
using Beeble.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beeble.Domain.DTOs
{
    public class LocalLibraryMemberDTO : LocalLibraryMember
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool IsVerifiedLocal { get; set; }
        public bool IsGuest { get; set; }
        public string Oib { get; set; }
        public string Address { get; set; }
        public string BarcodeNumber { get; set; }
        public DateTime? MembershipExpiryDate { get; set; }

        public List<ShortBookDTO> BorrowedBooks { get; set; }
        public double TotalLateReturnFee { get; set; }



        public static LocalLibraryMemberDTO FromData(LocalLibraryMember member)
        {
            var borrowedBooks = member.BatchesOfBorrowedBooks
                    .Select(batchOfBorrowedBooks => batchOfBorrowedBooks.Books.Select(book => ShortBookDTO.FromData(book, batchOfBorrowedBooks.ReturnDeadline)))
                   .SelectMany(book => book)
                   .ToList();

            return new LocalLibraryMemberDTO()
            {
                Name = member.Name,
                LastName = member.LastName,
                PhoneNumber = member.PhoneNumber,
                Email = member.Email,
                IsVerifiedLocal = member.IsVerifiedLocal,
                IsGuest = member.IsGuest,
                Oib = member.Oib,
                Address = member.Address,
                BarcodeNumber = member.BarcodeNumber,
                MembershipExpiryDate = member.MembershipExpiryDate,

                BorrowedBooks = borrowedBooks,

                Reservations = member.Reservations,

                TotalLateReturnFee = borrowedBooks.Count == 0 ? 0 : borrowedBooks.Select(book => book.LateReturnFee).Aggregate((sum, ele) => sum += ele)
            };
        }
    }
}
