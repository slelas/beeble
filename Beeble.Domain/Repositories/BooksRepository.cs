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
	    private readonly int _numberOfBooksPerSearchQuery = int.Parse(ConfigurationManager.AppSettings["numberOfBooksPerSearchQuery"]);

        public List<Book> SearchBooks(string searchQuery, int pageNumber, List<string> selectedFilters)
        {
            using (var context = new AuthContext())
            {
				var searchResultsQuery = context.Books
		            .Where(x => x.Name.Contains(searchQuery))
		            .OrderBy(x => x.Name)
		            .GroupBy(x => x.Name)
		            .Select(x => x.FirstOrDefault())
		            .Include(x => x.Categories)
		            .Include(x => x.Language)
		            .Include(x => x.Author)
		            .Include(x => x.YearOfIssue);

	            // only apply a filter if it is listed in the selected filters
	            // Nationalities
	            if (selectedFilters.Intersect(context.Nationalities.Select(y => y.Name)).Any())
		            searchResultsQuery = searchResultsQuery.Where(book => selectedFilters.Contains(book.Nationality.Name));

	            // Authors
	            if (selectedFilters.Intersect(context.Authors.Select(y => y.Name)).Any())
		            searchResultsQuery = searchResultsQuery.Where(book => selectedFilters.Contains(book.Author.Name));

	            // Categories
	            if (selectedFilters.Intersect(context.Categories.Select(y => y.Name)).Any())
		            searchResultsQuery = searchResultsQuery.Where(book => selectedFilters.Intersect(book.Categories.Select(y => y.Name)).Any());

	            // Years
	            if (selectedFilters.Intersect(context.YearsOfIssue.Select(y => y.Year.ToString())).Any())
		            searchResultsQuery = searchResultsQuery.Where((book => selectedFilters.Contains(book.YearOfIssue.Year)));

				var books = searchResultsQuery.OrderBy(x => x.Name).Skip(_numberOfBooksPerSearchQuery * pageNumber)
					.Take(_numberOfBooksPerSearchQuery).ToList();

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
		            .GroupBy(x => x.Name)
		            .Select(x => x.FirstOrDefault())
		            .Include(x => x.Categories)
		            .Include(x => x.Language)
		            .Include(x => x.Author)
		            .Include(x => x.YearOfIssue);

	            // only apply a filter if it is listed in the selected filters
	            // Nationalities
	            if (selectedFilters.Intersect(context.Nationalities.Select(y => y.Name)).Any())
		            searchResultsQuery = searchResultsQuery.Where(book => selectedFilters.Contains(book.Nationality.Name));

	            // Authors
	            if (selectedFilters.Intersect(context.Authors.Select(y => y.Name)).Any())
		            searchResultsQuery = searchResultsQuery.Where(book => selectedFilters.Contains(book.Author.Name));

	            // Categories
	            if (selectedFilters.Intersect(context.Categories.Select(y => y.Name)).Any())
		            searchResultsQuery = searchResultsQuery.Where(book => selectedFilters.Intersect(book.Categories.Select(y => y.Name)).Any());

	            // Years
	            if (selectedFilters.Intersect(context.YearsOfIssue.Select(y => y.Year.ToString())).Any())
		            searchResultsQuery = searchResultsQuery.Where((book => selectedFilters.Contains(book.YearOfIssue.Year)));

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

        public List<List<LongBookDTO>> GetBooksByName(string bookName, bool booksOfLibrariesWithMembership, Guid? userId)
        {
            using (var context = new AuthContext())
			{
				var result = new List<List<LongBookDTO>>();

				var usedIdAsString = userId.ToString();

                var allBooksWithName = context.Books
                    .Include(book => book.Author)
                    .Include(book => book.Categories)
                    .Include(book => book.Language)
                    .Include(book => book.Nationality)
                    .Include(book => book.YearOfIssue)
                    .Include(book => book.LocalLibrary)
                    .Include("LocalLibrary.Members")
                    .Include("LocalLibrary.Members.OnlineUser")
                    .Where(book => book.Name == bookName)
                    .ToList();

                if (userId != null)
                {
                    var booksFromUsersLibraries = allBooksWithName
                        .Where(book =>
                        {
                            return book.LocalLibrary.Members.Any(member =>
                            {
                                if (member.OnlineUser == null)
                                    return false;

                                return member.OnlineUser.Id == usedIdAsString;
                            });
                        }).ToList();



                    var booksThatAreBorrowed = booksFromUsersLibraries.Where(
                            book => context.BatchesOfBorrowedBooks.Any(batch => batch.Books.Select(x => x.Id).Contains(book.Id)) && !book.IsReserved)
                        .Select(book => LongBookDTO.FromData(book,
                            context.BatchesOfBorrowedBooks.FirstOrDefault(batch => batch.Books.Select(x => x.Id).Contains(book.Id))
                                .ReturnDeadline))
                        .OrderBy(borrowedBook => borrowedBook.ReturnDeadline)
                        .ToList();

                    var booksThatAreAvailable = booksFromUsersLibraries
                        .Where(book => !booksThatAreBorrowed.Select(borrowedBook => borrowedBook.BookId).Contains(book.Id) && !book.IsReserved)
                        .Select(book => LongBookDTO.FromData(book, null))
                        .ToList();

                    result.Add(booksThatAreBorrowed.Concat(booksThatAreAvailable)
                        .GroupBy(book => book.LocalLibrary.Name)
                        .Select(bookGroup => bookGroup.LastOrDefault())
                        .ToList());
                }

				var allAvailableBooksWithName = allBooksWithName
					.Where(book => book.Name == bookName && !book.IsBorrowed && !book.IsReserved)
					.ToList();

                if (userId == null)
                {
                    result.Add(null);
                    result.Add(allAvailableBooksWithName.Select(book => LongBookDTO.FromData(book, null)).ToList());

                    return result;
                }

				var booksFromOtherLibraries = allAvailableBooksWithName
					.Where(book =>
					{
						return book.LocalLibrary.Members.All(member =>
                        {
                            if (member.OnlineUser == null)
                                return false;

                            return member.OnlineUser.Id != usedIdAsString;
                        });
					}).ToList();

				result.Add(booksFromOtherLibraries.Select(book => LongBookDTO.FromData(book, null)).ToList());

				return result;
			}
        }

	    public List<int> GetNumberOfAvailableAndReservedBooks(string bookName)
	    {
		    using (var context = new AuthContext())
		    {

			    var totalAvailableBooks = context.Books.Count(book => book.Name == bookName && !book.IsBorrowed && !book.IsReserved);
			    var totalReservedBooks = context.Reservations.Count();

			    return new List<int>() {totalAvailableBooks, totalReservedBooks};
		    }
	    }

	    public void MakeAReservation(int libraryId, string bookName, string authorName)
	    {
		    using (var context = new AuthContext())
		    {
			    var bookToReserve = context.Books
				    .FirstOrDefault(book => book.Name == bookName && book.Author.Name == authorName && book.LocalLibrary.Id == libraryId);

			    var libraryMember = context.LocalLibraryMembers
                    .Include(member => member.LocalLibrary)
					.FirstOrDefault(member => member.LocalLibrary.Id == libraryId);

                context.Reservations.Add(new Reservation()
                {
                    Book = bookToReserve,
                    LibraryMember = libraryMember,
                    PickupDeadline = DateTime.Now.Add(TimeSpan.FromHours(libraryMember.LocalLibrary.ReservationDuration)),
                    IsGuestMember = libraryMember == null
                });

			    bookToReserve.IsReserved = true;

                context.SaveChanges();
		    }
	    }
    }
}