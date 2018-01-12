using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Beeble.Data;
using Beeble.Data.Models;
using Beeble.Domain.DTOs;
using Microsoft.Owin.Security.Provider;

namespace Beeble.Domain.Repositories
{
    public class BooksRepository
    {
        int numberOfBooksPerSearchQuery = int.Parse(ConfigurationManager.AppSettings["numberOfBooksPerSearchQuery"]);

        public List<Book> SearchBooks(string searchQuery, int pageNumber, List<string> selectedFilters)
        {
            using (var context = new AuthContext())
            {
                var whichFilters = DetermineWhichFilters(selectedFilters);
                var firstFilter = whichFilters[0];
                var secondFilter = whichFilters[1];
                var thirdFilter = whichFilters[2];
                var fourthFilter = whichFilters[3];
                var searchResultsQuery = context.Books
                    .Where(x => x.Name.Contains(searchQuery))
                    .OrderBy(x => x.Name)
                    .Where(x => 
                    ((!selectedFilters.Any())
                    || (
                    (firstFilter && selectedFilters.Contains(x.Nationality.Name))
                    || (secondFilter && selectedFilters.Contains(x.Author.Name))
                    || (thirdFilter && selectedFilters.Intersect(x.Categories.Select(y => y.Name)).Any())
                    || (fourthFilter && selectedFilters.Contains(x.YearOfIssue.Year))
                    )))
                    .GroupBy(x => x.Name)
                    .Select(x => x.FirstOrDefault());



                var books = searchResultsQuery
                    .GroupBy(x => x.Name)
                    .Select(x => x.FirstOrDefault())
                    .Include(x => x.Categories)
                    .Include(x => x.Language)
                    .Include(x => x.Author)
                    .Include(x => x.YearOfIssue)
                    .OrderBy(x => x.Name)
                    .Skip(numberOfBooksPerSearchQuery * pageNumber)
                    .Take(numberOfBooksPerSearchQuery).ToList();

                // preventing circular reference
                books.Select(x => x.Categories =
                x.Categories
                .Select(y => new Category() { Name = y.Name, Books = null })
                .ToList())
                .ToList();

                return books;
            }

        }

        public List<List<List<string>>> GetAllFilters(string searchQuery, List<string> selectedFilters)
        {
            using (var context = new AuthContext())
            {
                var searchResultsQuery = context.Books
                    .Where(x => x.Name.Contains(searchQuery))
                    .OrderBy(x => x.Name)
                    .Where(x => (!selectedFilters.Any() || selectedFilters.Contains(x.Nationality.Name) || (selectedFilters.Contains(x.Author.Name) || (selectedFilters.Intersect(x.Categories.Select(y => y.Name)).Any()) || (selectedFilters.Contains(x.YearOfIssue.Year)))))
                    .GroupBy(x => x.Name)
                    .Select(x => x.FirstOrDefault());

                var allFilters = new List<List<List<string>>>
            {
                GetFilters(searchResultsQuery, "Nationality"),
                GetFilters(searchResultsQuery, "Author"),
                GetFilters(searchResultsQuery, "Category"),
                GetFilters(searchResultsQuery, "Year")
            };

                return allFilters;
            }
        }

        public List<List<string>> GetFilters(IQueryable<Book> searchResultsQuery, string filterName)
        {

            Dictionary<string, int> filterCount = new Dictionary<string, int>();

            if (filterName == "Nationality")
            {
                var filtersInBooks = searchResultsQuery
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
                var filtersInBooks = searchResultsQuery
                    .Select(x => x.Author.Name).ToList();

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
                var filtersListInBooks = searchResultsQuery
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
            else if (filterName == "Year")
            {
                var filtersInBooks = searchResultsQuery
                    .Select(x => x.YearOfIssue.Year).ToList();

                filtersInBooks.ForEach(x =>
                {
                    if (!filterCount.Keys.Contains(x))
                        filterCount.Add(x, 1);
                    else
                        filterCount[x]++;
                });
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

        public List<Book> GetBooksByName(string bookName, bool booksOfLibrariesWithMembership, Guid userId)
        {
            using (var context = new AuthContext())
			{
				//if(booksOfLibrariesWithMembership)
				//	var booksQuery = context.Books
				//		.Where(x => x.Name == bookName && x.IsAvailable)

				var usedIdAsString = userId.ToString();
				var llid = 1;

				var test = context.Users
					.Include(user => user.LocalLibraryMembers)
					.Include("LocalLibraryMembers.LocalLibrary")
					//.FirstOrDefault(user => user.Id == usedIdAsString)
					.Where(user => user.LocalLibraryMembers.Any(member => member.OnlineUser.Id == usedIdAsString &&
					                                                      user.Id == usedIdAsString))
					.FirstOrDefault();
					//.LocalLibraryMembers.Contains(user);

				/*var books = context.Books
					.Where(book => book.Name == bookName &&
					               book.IsAvailable &&
					               context.Users
						               .FirstOrDefault(user => user.Id == usedIdAsString)
						               .LocalLibraryMembers.Any(member => member.LocalLibrary.Id == book.LocalLibrary.Id))
					.GroupBy(book => book.LocalLibrary)
					.Select(bookGroup => bookGroup.FirstOrDefault())
					.Include(x => x.Categories)
					.Include(x => x.LocalLibrary)
					.Include(x => x.Author)
					.Include(x => x.Language)
					.Include(x => x.YearOfIssue)
					.Include(x => x.Nationality)
					.ToList();*/

				
				List<Book> books = null;/*context.Books
					.Where(book => book.Name == bookName &&
					               true &&
						               //.Include(user => user.LocalLibraryMembers)
						               //.FirstOrDefault(user => user.Id == usedIdAsString)
						               test2
						               /*.LocalLibraryMembers.Any(member => member.LocalLibrary.Id == book.LocalLibrary.Id))
					.GroupBy(book => book.LocalLibrary)
					.Select(bookGroup => bookGroup.FirstOrDefault())
					.Include(x => x.Categories)
					.Include(x => x.LocalLibrary)
					.Include(x => x.Author)
					.Include(x => x.Language)
					.Include(x => x.YearOfIssue)
					.Include(x => x.Nationality)
					.ToList();*/

				var localLibraryMembers = test.LocalLibraryMembers;
				var allBooksWithName = context.Books
					.Include(book => book.Author)
					.Include(book => book.Categories)
					.Include(book => book.Language)
					.Include(book => book.Nationality)
					.Include(book => book.YearOfIssue)
					.Where(book => book.Name == bookName).ToList();
				var booksFromUsersLibraries = allBooksWithName
					.Where(book => localLibraryMembers.Any(member => member.LocalLibrary.Id == book.LocalLibrary.Id)).ToList();

				/*// preventing circular reference
                books.Select(x => x.Categories =
                            x.Categories
                                .Select(y => new Category() { Name = y.Name, Books = null })
                                .ToList())
                    .ToList();*/

				booksFromUsersLibraries = booksFromUsersLibraries.Select(x => new Book() {Author = x.Author}).ToList();
				
				return booksFromUsersLibraries;
            }
        }

        public List<bool> DetermineWhichFilters(List<string> selectedFilters)
        {
            using (var context = new AuthContext())
            {
                var whichFilters = new List<bool>();
                /*var searchResultsQueryBase = context.Books
                    .Where(x => x.Name.Contains(searchQuery))
                    .OrderBy(x => x.Name);*/

                // true if there are any categories in the filter list
                whichFilters.Add(selectedFilters.Intersect(context.Nationalities.Select(y => y.Name)).Any());
                whichFilters.Add(selectedFilters.Intersect(context.Authors.Select(y => y.Name)).Any());
                whichFilters.Add(selectedFilters.Intersect(context.Categories.Select(y => y.Name)).Any());
                whichFilters.Add(selectedFilters.Intersect(context.YearsOfIssue.Select(y => y.Year.ToString())).Any());

                return whichFilters;
            }
        }

	    public void MakeABookReservation(int libraryId, string bookName, string authorName, Guid UserId)
	    {
		    using (var context = new AuthContext())
		    {
			    var bookToReserve = context.Books
				    .FirstOrDefault(book => book.Name == bookName && book.Author.Name == authorName && book.LocalLibrary.Id == libraryId);

			    var libraryMember = context.LocalLibraryMembers
					.FirstOrDefault(member => member.LocalLibrary.Id == libraryId);

			    context.Reservations.Add(new Reservation()
			    {
				    Book = bookToReserve,
				    LibraryMember = libraryMember,
					PickupDeadline = DateTime.Now.Add(TimeSpan.FromHours(libraryMember.LocalLibrary.ReservationDuration)),
					IsGuestMember = libraryMember == null
			    });

			    bookToReserve.IsAvailable = false;
		    }
	    }
    }
}