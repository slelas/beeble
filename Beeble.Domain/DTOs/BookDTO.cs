using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beeble.Data.Models;

namespace Beeble.Domain.DTOs
{
	public class ShortBookDTO
	{
		public string ImageUrl { get; set; }
		public string Name { get; set; }
		public string Author { get; set; }
		public DateTime? ReturnDeadline { get; set; }
		public long BookId { get; set; }

		public static ShortBookDTO FromData(Book book, DateTime? returnDeadline)
		{
			return new ShortBookDTO()
			{
				ImageUrl = book.ImageUrl,
				Name = book.Name,
				Author = book.Author.Name,
				ReturnDeadline = returnDeadline,
				BookId = book.Id
			};
		}
	}

	public class LongBookDTO
	{
		public string ImageUrl { get; set; }
		public string Name { get; set; }
		public string Author { get; set; }
		public string NumOfPages { get; set; }
		public string Publisher { get; set; }
		public string Language { get; set; }
		public string Isbn { get; set; }
		public string Categories { get; set; }
		public long BookId { get; set; }
		public LocalLibrary LocalLibrary { get; set; }
		public DateTime? ReturnDeadline { get; set; }

		public static LongBookDTO FromData(Book book, DateTime? returnDeadline)
		{
			return new LongBookDTO()
			{
				ImageUrl = book.ImageUrl,
				Name = book.Name,
				Author = book.Author.Name,
				BookId = book.Id,
				NumOfPages = book.NumOfPages,
				Publisher = book.Publisher,
				Language = book.Language.Name,
				Isbn = book.ISBN,
				Categories = book.Categories.Select(category => category.Name).Aggregate((sum, ele) => $"{sum} , {ele}"),

				LocalLibrary = new LocalLibrary()
				{
					Id = book.LocalLibrary.Id,
					Name = book.LocalLibrary.Name,
					Address = book.LocalLibrary.Address,
					DefaultLendDuration = book.LocalLibrary.DefaultLendDuration,
					GuestBorrowPrice = book.LocalLibrary.GuestBorrowPrice,
				},

				ReturnDeadline = returnDeadline 
			};
		}
	}
}
