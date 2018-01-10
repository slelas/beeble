using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace Beeble.Api.Controllers
{
    public abstract class AuthorizationController : ApiController
    {
		public Guid UserId {
			get
			{
				var principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
				var customClaimValue = principal.Claims.Where(c => c.Type == "userId").Single().Value;
				return Guid.Parse(customClaimValue);

			}
		}

	}
}
