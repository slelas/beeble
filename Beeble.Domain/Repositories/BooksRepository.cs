using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beeble.Data;
using Beeble.Data.Models;

namespace Beeble.Domain.Repositories
{
	public class BooksRepository
	{
		int numberOfBooksPerSearchQuery = int.Parse(ConfigurationManager.AppSettings["numberOfBooksPerSearchQuery"]);

		public List<Book> SearchBooks(string search, int pageNumber)
		{
			using (var context = new AuthContext())
			{
				return context.Books
					.Where(x => x.Name.Contains(search))
					.OrderBy(x => x.Name)
					.Skip(numberOfBooksPerSearchQuery * pageNumber)
					.Take(numberOfBooksPerSearchQuery).ToList();
			}
		}
	}
}
