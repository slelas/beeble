using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beeble.Data.Models;

namespace Beeble.Domain.DTOs
{
	public class OnlineUserDTO
	{
		public string Name { get; set; }
		public string LastName { get; set; }
		public string Oib { get; set; }
		public string Email { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string PhoneNumber { get; set; }
		public List<ShortBookDTO> BorrowedBooks { get; set; }
		public List<Reservation> Reservations { get; set; }


		public static OnlineUserDTO FromData(OnlineUser onlineUser)
		{
			var a = onlineUser;
			var b = onlineUser.LocalLibraryMembers.Select(member => member.Reservations).SelectMany(reservation => reservation)
				.ToList();
			var test = onlineUser.LocalLibraryMembers
				.Select(member => member.Reservations.Select(
					reservation => new Reservation() {Book = reservation.Book, PickupDeadline = reservation.PickupDeadline}))
				.SelectMany(reservation => reservation).ToList();

			return new OnlineUserDTO()
			{
				Name = onlineUser.Name,
				Email = onlineUser.Email,
				LastName = onlineUser.LastName,
				Oib = onlineUser.Oib,
				Address = onlineUser.Address,
				City = onlineUser.City,
				PhoneNumber = onlineUser.PhoneNumber,

				BorrowedBooks = onlineUser.LocalLibraryMembers.Select(member => member.BatchesOfBorrowedBooks
						.Where(batch => batch.Books != null)
						.Select(batchOfBorrowedBooks => batchOfBorrowedBooks.Books.Select(
							book => ShortBookDTO.FromData(book, batchOfBorrowedBooks.ReturnDeadline)))
						.SelectMany(book => book))
					.SelectMany(x => x)
					.ToList(),

				Reservations = onlineUser.LocalLibraryMembers.Select(member => member.Reservations.Select(reservation => new Reservation(){Book = reservation.Book, PickupDeadline = reservation.PickupDeadline})).SelectMany(reservation => reservation).ToList()
			};
		}
	}
}
