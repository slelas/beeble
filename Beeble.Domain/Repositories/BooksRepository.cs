using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
using MoreLinq;
using System.Data;
using Newtonsoft.Json;

namespace Beeble.Domain.Repositories
{
    public class BooksRepository
    {
	    private readonly int _numberOfBooksPerSearchQuery = int.Parse(ConfigurationManager.AppSettings["numberOfBooksPerSearchQuery"]);

        public List<LongBookDTO> SearchBooks(string searchQuery, int pageNumber, List<string> selectedFilters)
        {
            using (var context = new AuthContext())
            {
				var searchResultsQuery = context.Books
		            .Where(x => x.Name.Contains(searchQuery) || x.Keyword == searchQuery)
		            .OrderBy(x => x.Name)
		            .GroupBy(x => x.Name)
		            .Select(x => x.FirstOrDefault())
		            .Include(x => x.Categories)
		            .Include(x => x.Language)
		            .Include(x => x.Author)
		            .Include(x => x.YearOfIssue)
                    .Include(x => x.LocalLibrary);

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

                // Languages
                if (selectedFilters.Intersect(context.Languages.Select(y => y.Name)).Any())
                    searchResultsQuery = searchResultsQuery.Where(book => selectedFilters.Contains(book.Language.Name));

                var books = searchResultsQuery.OrderBy(x => x.Name).Skip(_numberOfBooksPerSearchQuery * pageNumber)
					.Take(_numberOfBooksPerSearchQuery).ToList();

                return books.Select(book => LongBookDTO.FromData(book, null))
                    .OrderByDescending(book => book.YearOfIssue)
                    //.ThenByDescending(book => book.Name)
                    .ToList();
            }

        }

		public List<List<List<string>>> GetAllFilters(string searchQuery, List<string> selectedFilters)
        {
            using (var context = new AuthContext())
            {
				var searchResultsQuery = context.Books
		            .Where(x => x.Name.Contains(searchQuery) || searchQuery == x.Keyword)
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

                // Languages
                if (selectedFilters.Intersect(context.Languages.Select(y => y.Name)).Any())
                    searchResultsQuery = searchResultsQuery.Where(book => selectedFilters.Contains(book.Language.Name));

                var allFilters = new List<List<List<string>>>
	            {
		            GetFilters(searchResultsQuery, "Nationality"),
		            GetFilters(searchResultsQuery, "Author"),
		            GetFilters(searchResultsQuery, "Category"),
		            GetFilters(searchResultsQuery, "Year"),
                    GetFilters(searchResultsQuery, "Language")
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
            else if(filterName == "Language")
            {
                var filtersInBooks = searchResultsQuery
                    .Select(x => x.Language.Name).ToList();

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

				var userIdAsString = userId.ToString();

                var allBooksWithName = context.Books
                    .Include(book => book.Author)
                    .Include(book => book.Categories)
                    .Include(book => book.Language)
                    .Include(book => book.Nationality)
                    .Include(book => book.YearOfIssue)
                    .Include(book => book.LocalLibrary)
                    .OrderBy(book => book.LocalLibrary.Name)
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

                                return member.OnlineUser.Id == userIdAsString;
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
						return !book.LocalLibrary.Members.Any(member =>
                        {
                            if (member.OnlineUser == null)
                                return false;

                            return member.OnlineUser.Id == userIdAsString;
                        });
					}).ToList();

				result.Add(booksFromOtherLibraries.Select(book => LongBookDTO.FromData(book, null)).DistinctBy(x => x.LocalLibrary.Id).ToList());

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

		        context.ReservedBooksAll.Add(new ReservedBooksAll()
		        {
		            LocalLibrary = libraryMember.LocalLibrary,
		            TimeStamp = DateTime.Now
		        });

			    bookToReserve.IsReserved = true;

                context.SaveChanges();
		    }
	    }

	    public LongBookDTO GetBookForOneTimeBorrow(int libraryId, string bookName, string authorName)
	    {
		    using (var context = new AuthContext())
		    {
			    return context.Books
					.Where(book => book.Name == bookName && book.Author.Name == authorName && book.LocalLibrary.Id == libraryId)
					.Include("Author")
					.Include("Language")
					.Include("Categories")
					.Include("LocalLibrary")
                    .Include("YearOfIssue")
					.ToList()
					.Select(book => LongBookDTO.FromData(book, null))
					.FirstOrDefault();
		    }
	    }

	    public Book GetBookByBarcode(string bookBarcode)
	    {
		    using (var context = new AuthContext())
		    {
			    return context.Books
					.Include("Author")
					.FirstOrDefault(book => book.BarcodeNumber == bookBarcode);
		    }
	    }

        public List<string> GetAllAuthors()
        {
            using (var context = new AuthContext())
            {
                return context.Authors
                    .Select(author => author.Name)
                    .ToList();
            }
        }

        public List<string> GetAllCategories()
        {
            using (var context = new AuthContext())
            {
                return context.Categories
                    .Select(category => category.Name)
                    .ToList();
            }
        }

        public List<string> GetAllLanguages()
        {
            using (var context = new AuthContext())
            {
                return context.Languages
                    .Select(language => language.Name)
                    .ToList();
            }
        }

        public List<string> GetAllNationalities()
        {
            using (var context = new AuthContext())
            {
                return context.Nationalities
                    .Select(nationality => nationality.Name)
                    .ToList();
            }
        }

	    public static string[] Convert(object input)
	    {
		    return input as string[];
	    }

		public void AddNewBook(NameValueCollection bookData, string blobUrl, Guid? userId)
	    {
		    var authorName = bookData["author"];
			var yearValue = bookData["year"];
		    var languageValue = bookData["language"];
		    var nationalityValue = bookData["nationality"];
		    var categoriesValue = JsonConvert.DeserializeObject<Object[]>(bookData["categories"]);


			using (var context = new AuthContext())
			{
				var bookCategories = context.Categories.ToList()
					.Where(c => categoriesValue.Select(x => x.ToString()).Contains(c.Name)).ToList();

				if (!context.Authors.Any(author => author.Name == authorName))
				{
					context.Authors.Add(new Author() { Name = authorName });
					context.SaveChanges();
				}

			    if (!context.YearsOfIssue.Any(year => year.Year == yearValue))
			    {
				    context.YearsOfIssue.Add(new YearOfIssue() { Year = yearValue });
				    context.SaveChanges();
			    }

			    if (!context.Languages.Any(language => language.Name == languageValue))
			    {
				    context.Languages.Add(new Language() { Name = languageValue });
				    context.SaveChanges();
			    }

			    if (!context.Nationalities.Any(nationality => nationality.Name == nationalityValue))
			    {
				    context.Nationalities.Add(new Nationality() { Name = nationalityValue });
				    context.SaveChanges();
			    }

				var bookAuthor = context.Authors.FirstOrDefault(author => author.Name == authorName);
				var bookYearOfIssue = context.YearsOfIssue.FirstOrDefault(year => year.Year == yearValue);
				var bookLanguage = context.Languages.FirstOrDefault(language => language.Name == languageValue);
				var bookNationality = context.Nationalities.FirstOrDefault(nationality => nationality.Name == nationalityValue);

                var library = context.LocalLibraries
                                .FirstOrDefault(localLibrary => localLibrary.Administrators.Select(admin => admin.Id).ToList().Contains(userId.ToString()));

                var barcodeNumber = long.Parse(context.Books
                    //.Where(book => book.LocalLibrary.Id == library.Id)
                    .OrderBy(book => book.BarcodeNumber)
                    .LastOrDefault()
                    .BarcodeNumber) + 1;

                var bookToAdd = new Book()
                {
                    Name = bookData["name"],
                    NumOfPages = bookData["numOfPages"],
                    ISBN = bookData["isbn"],
                    Description = bookData["description"],
                    Publisher = bookData["publisher"],
                    IsBorrowed = false,
                    IsReserved = false,
                    ImageUrl = blobUrl,
                    LocalLibrary = library,
                    BarcodeNumber = barcodeNumber.ToString(),

                    Author = bookAuthor,
                    YearOfIssue = bookYearOfIssue,
                    Language = bookLanguage,
                    Nationality = bookNationality,
                    Categories = bookCategories

                };

			    context.Books.Add(bookToAdd);

			    context.SaveChanges();
		    }
	    }
    }
}