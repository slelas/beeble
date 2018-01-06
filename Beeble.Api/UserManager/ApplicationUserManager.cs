using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Beeble.Data;
using Beeble.Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Beeble.Api.UserManager
{
    public class UserManager : UserManager<OnlineUser>
    {
        public UserManager(IUserStore<OnlineUser> store)
            : base(store)
        {

        }

        public static UserManager Create(IdentityFactoryOptions<UserManager> options, IOwinContext context)
        {
            var manager = new UserManager(new UserStore<OnlineUser>(context.Get<AuthContext>()));
            manager.UserValidator = new UserValidator<OnlineUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = false
            };

            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 1,
                RequireDigit = false,
            };

            return manager;
        }
    }
}