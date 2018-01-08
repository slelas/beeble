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

        public List<string> SearchBooks(string searchQuery, int pageNumber, List<string> selectedFilters)
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
                        .Select(x => x.Name)
                        .Distinct()
                        .OrderBy(x => x)
                    .Skip(numberOfBooksPerSearchQuery * pageNumber)
                    .Take(numberOfBooksPerSearchQuery).ToList();
                }

                return searchResultsQuery
                    .Where(x => (selectedFilters.Contains(x.Nationality.Name) && (selectedFilters.Contains(x.Author) && (selectedFilters.Intersect(x.Categories.Select(y => y.Name)).Any()))))
                    .Select(x => x.Name)
                    .Distinct()
                    .OrderBy(x => x)
                    .Skip(numberOfBooksPerSearchQuery * pageNumber)
                    .Take(numberOfBooksPerSearchQuery).ToList();
            }
        }

        public List<List<List<string>>> GetAllFilters(string searchQuery)
        {
            var allFilters = new List<List<List<string>>>
            {
                GetFilters(searchQuery, "Nationality"),
                GetFilters(searchQuery, "Author"),
                GetFilters(searchQuery, "Category")
            };

            return allFilters;
        }

        /*public List<List<string>> GetNationalityFilters(string searchQuery)
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
        }*/

        public List<List<string>> GetFilters(string searchQuery, string filterName)
        {

            Dictionary<string, int> filterCount = new Dictionary<string, int>();

            using (var context = new AuthContext())
            {

                if (filterName == "Nationality")
                {
                    var filtersInBooks = context.Books
                        .Where(x => x.Name.Contains(searchQuery))
                        .GroupBy(x => x.Name)
                        .Select(x => x.FirstOrDefault())
                        .Select(x => x.Nationality).ToList();

                    filtersInBooks.ForEach(x =>
                    {
                        if (!filterCount.Keys.Contains(x.Name))
                            filterCount.Add(x.Name, 1);
                        else
                            filterCount[x.Name]++;
                    });
                }
                if (filterName == "Author")
                {
                    var filtersInBooks = context.Books
                        .Where(x => x.Name.Contains(searchQuery))
                        .GroupBy(x => x.Name)
                        .Select(x => x.FirstOrDefault())
                        .Select(x => x.Author).ToList();

                    filtersInBooks.ForEach(x =>
                    {
                        if (!filterCount.Keys.Contains(x))
                            filterCount.Add(x, 1);
                        else
                            filterCount[x]++;
                    });
                }
                else if (filterName == "Category")
                {
                    var filtersListInBooks = context.Books
                        .Where(x => x.Name.Contains(searchQuery))
                        .GroupBy(x => x.Name)
                        .Select(x => x.FirstOrDefault())
                        .Select(x => x.Categories).ToList();

                    var filtersInBooks = new List<Category>();

                    foreach (var categoryList in filtersListInBooks)
                    {
                        foreach (var category in categoryList)
                        {
                            filtersInBooks.Add(category);
                        }
                    }

                    filtersInBooks.ForEach(x =>
                    {
                        if (!filterCount.Keys.Contains(x.Name))
                            filterCount.Add(x.Name, 1);
                        else
                            filterCount[x.Name]++;
                    });
                }

            }

                var listOfValuesAsStrings = new List<string>();

                foreach (var value in filterCount.Values)
                {
                    listOfValuesAsStrings.Add(value.ToString());
                }

            return new List<List<string>>
            {
                filterCount.Keys.ToList(),
                listOfValuesAsStrings
            };

        }
    }
}