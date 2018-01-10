using System;
using System.Collections.Generic;
using System.Web.Http;
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
	    public LongLLMemberUserDTO GetLibraryById(int libraryId = 1)
	    {
		    var userId = Guid.Parse("ae00135b-d69f-4e1b-bc6c-f57fd919b015");
			var a = repo.GetLibraryById(libraryId, userId);
			return repo.GetLibraryById(libraryId, userId);
	    }
	}
}
