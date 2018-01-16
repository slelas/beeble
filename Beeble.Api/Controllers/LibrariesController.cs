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
        public bool EnrollToLibraryWithBarcode(int libraryId, long barcodeNumber)
        {
            return repo.EnrollToLibraryWithBarcode(libraryId, barcodeNumber, UserId);
        }

	    [HttpGet]
	    [Authorize]
	    [Route("get-member-by-id")]
		public ShortLLMemberUserDTO getMemberById(long memberId)
	    {
		    return repo.GetMemberById(memberId);
	    }
	}
}
