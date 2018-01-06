﻿using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using Beeble.Data;
using Beeble.Data.Models;
using Beeble.Domain.Repositories;

namespace Beeble.Api.Controllers
{

	[RoutePrefix("api/search")]
	public class BooksController : ApiController
	{
		private BooksRepository repo = null;

		public BooksController()
		{
			repo = new BooksRepository();
		}

		[HttpGet]
		[Route("byquery")]
		public List<Book> SearchBooks(int pageNumber, string searchQuery)
		{
			return repo.SearchBooks(searchQuery, pageNumber);
		}

        [HttpGet]
        [Route("get-filters")]
        public List<List<string>> GetFiltersForSearch(string searchQuery)
        {
            return repo.GetFilters(searchQuery);
        }
    }
}
