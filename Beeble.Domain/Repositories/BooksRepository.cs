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

		public List<Book> SearchBooks(string searchQuery, int pageNumber, List<string> selectedFilters)
		{

			using (var context = new AuthContext())
			{
				var searchResultsQuery = context.Books
					.Where(x => x.Name.Contains(searchQuery))
					.OrderBy(x => x.Name);

				// short circuit to prevent Index out of range
				if (selectedFilters.Any() && selectedFilters[0] == "-1")
				{
					return searchResultsQuery
					.Skip(numberOfBooksPerSearchQuery * pageNumber)
					.Take(numberOfBooksPerSearchQuery).ToList();
				}

				return searchResultsQuery.Where(x => selectedFilters.Contains(x.Nationality.Name)).OrderBy(x => x.Name)
					.Skip(numberOfBooksPerSearchQuery * pageNumber)
					.Take(numberOfBooksPerSearchQuery).ToList();
			}
		}

        public List<List<string>> GetFilters(string searchQuery)
        {
            Dictionary<string, int> nationalitiesCount = new Dictionary<string, int>();

            using (var context = new AuthContext())
            {
                var nationalitiesOfBooks = context.Books
                    .Where(x => x.Name.Contains(searchQuery))
                    .Select(x => x.Nationality).ToList();

                nationalitiesOfBooks.ForEach(x =>
                {
                    if (!nationalitiesCount.Keys.Contains(x.Name))
                            nationalitiesCount.Add(x.Name, 1);
                    else
                        nationalitiesCount[x.Name]++;
                });

			}

            var listOfValuesAsStrings = new List<string>();

            foreach (var value in nationalitiesCount.Values)
            {
                listOfValuesAsStrings.Add(value.ToString());
            }

            return new List<List<string>>
            {
                nationalitiesCount.Keys.ToList(),
                listOfValuesAsStrings
            };
        }
    }
}
