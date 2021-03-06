﻿using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using Beeble.Data;
using Beeble.Data.Models;
using Beeble.Domain.DTOs;
using Beeble.Domain.Repositories;

namespace Beeble.Api.Controllers
{

	[RoutePrefix("api/search")]
	public class BooksController : AuthorizationController
	{
        private BooksRepository repo = null;

		public BooksController()
		{
			repo = new BooksRepository();
		}

		[HttpGet]
		[Route("byquery")]
		public List<LongBookDTO> SearchBooks(int pageNumber, string searchQuery, [FromUri]List<string> selectedFilters)
		{
			return repo.SearchBooks(searchQuery, pageNumber, selectedFilters);
		}

        [HttpGet]
        [Route("get-filters")]
        public List<List<List<string>>> GetFiltersForSearch(string searchQuery, [FromUri]List<string> selectedFilters)
        {
            return repo.GetAllFilters(searchQuery, selectedFilters);
        }

		[HttpGet]
		[Route("get-books-byname")]
		public List<List<LongBookDTO>> GetBooksByName(string bookName, bool booksOfLibrariesWithMembership)
		{
			return repo.GetBooksByName(bookName, booksOfLibrariesWithMembership, UserId);
		}

		[HttpGet]
		[Route("get-book-numbers")]
		public List<int> GetNumberOfAvailableAndReservedBooks(string bookName)
		{
			return repo.GetNumberOfAvailableAndReservedBooks(bookName);
		}

        [HttpGet]
        [Route("reserve")]
        public void MakeAReservation(int libraryId, string bookName, string authorName)
        {
            repo.MakeAReservation(libraryId, bookName, authorName);
        }

		[HttpGet]
		[Route("get-one-time-borrow")]
		public LongBookDTO GetBookForOneTimeBorrow(int libraryId, string bookName, string authorName)
		{
			return repo.GetBookForOneTimeBorrow(libraryId, bookName, authorName);
		}

		[HttpGet]
		[Route("get-by-barcode")]
		public Book GetBookByBarcode(string bookBarcode)
		{
			return repo.GetBookByBarcode(bookBarcode);
		}

        [HttpGet]
        [Route("get-authors")]
        public List<string> GetAllAuthors()
        {
            return repo.GetAllAuthors();
        }

        [HttpGet]
        [Route("get-categories")]
        public List<string> GetAllCategories()
        {
            return repo.GetAllCategories();
        }

        [HttpGet]
        [Route("get-nationalities")]
        public List<string> GetAllNationalities()
        {
            return repo.GetAllNationalities();
        }

        [HttpGet]
        [Route("get-languages")]
        public List<string> GetAllLanguages()
        {
            return repo.GetAllLanguages();
        }
    }
}
