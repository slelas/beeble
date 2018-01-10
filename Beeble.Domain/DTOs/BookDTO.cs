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
		public int BookId { get; set; }

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
}
