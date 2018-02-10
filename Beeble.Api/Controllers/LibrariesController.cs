using System;
using System.Collections.Generic;
using System.Web.Http;
using Beeble.Data.Models;
using Beeble.Domain.Repositories;
using Beeble.Domain.DTOs;

namespace Beeble.Api.Controllers
{
    [RoutePrefix("api/libraries")]
    public class LibrariesController : AuthorizationController
    {
        private LibrariesRepository repo = null;

        public LibrariesController()
        {
            repo = new LibrariesRepository();
        }

        [HttpGet]
        [Authorize]
        [Route("get")]
        public List<ShortLLMemberUserDTO> GetLocalLibraries()
        {
            return repo.GetLocalLibraries(userId: UserId);
        }

        [HttpGet]
        [Authorize]
        [Route("get-byid")]
        public LongLLMemberUserDTO GetLibraryById(int libraryId)
        {
            return repo.GetLibraryById(libraryId, UserId);
        }

        [HttpGet]
        [Authorize]
        [Route("get-byid-membership")]
        public LocalLibrary GetLibraryByIdForMembership(int libraryId)
        {
            return repo.GetLibraryByIdForMembership(libraryId, UserId);
        }

        [HttpGet]
        [Authorize]
        [Route("get-all")]
        public List<LocalLibrary> GetAll()
        {
            return repo.GetAll(UserId);
        }

        [HttpGet]
        [Authorize]
        [Route("enroll-with-barcode")]
        public bool EnrollToLibraryWithBarcode(int libraryId, string barcodeNumber)
        {
            return repo.EnrollToLibraryWithBarcode(libraryId, barcodeNumber, UserId);
        }

        [HttpGet]
        //[Authorize]
        [Route("get-member-by-barcode")]
        public ShortLLMemberUserDTO getMemberById(string memberBarcode)
        {
            return repo.GetMemberByBarcode(memberBarcode);
        }

        [HttpGet]
        //[Authorize(Roles="Admin")]
        [Route("lend-return")]
        public bool LendAndReturnScanned([FromUri] List<string> bookBarcodes, string memberBarcode = null)
        {
            return repo.LendAndReturnScanned(bookBarcodes, memberBarcode, UserId);
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        [Route("get-book-list")]
        public List<LibraryBookDTO> GetBookList(string sortOption, bool descending, string searchQuery, int pageNumber)
        {
            return repo.GetBookList(sortOption, descending, searchQuery, pageNumber, UserId);
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        [Route("get-member-list")]
        public List<LocalLibraryMemberDTO> GetMemberList(string sortOption, bool descending, string searchQuery,
            int pageNumber)
        {
            return repo.GetMemberList(sortOption, descending, searchQuery, pageNumber, UserId);
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        [Route("get-categories-stats")]
        public List<List<string>> GetCategoriesStats()
        {
            return repo.GetCategoriesStats();
        }

        [HttpGet]
        [Route("get-borrowed-reserved-monthly")]
        public List<List<string>> GetBorrowedReservedStats(int year)
        {
            return repo.GetBorrowedReservedStats(year);
        }

        [HttpGet]
        [Route("get-borrowed-week")]
        public List<List<string>> GetBorrowedInWeek()
        {
            return repo.GetBorrowedInWeek();
        }

        [HttpGet]
        [Route("get-active-years")]
        public List<int> GetLibraryActiveYears()
        {
            return repo.GetLibraryActiveYears(UserId);
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        [Route("get-library-id")]
        public int GetLibraryId()
        {
            return repo.GetLibraryId(UserId);
        }
    }
}
