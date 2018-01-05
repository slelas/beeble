using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Web;

namespace Beeble.Api
{
    public class UsersSeed
    {
        public static void Execute()
        {
            using (var context = new AuthContext())
            {
                var store = new UserStore<IdentityUser>(context);
                var _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(context));

                if (!context.Users.Any())
                {
                    var adminUser = new IdentityUser()
                    {
                        Email = "zvonimird@dump.hr",
                        UserName = "zdelas",
                    };

                    _userManager.Create(adminUser, "123456");
                    _userManager.AddToRole(adminUser.Id, "Admin");

                    var regularUser = new IdentityUser()
                    {
                        Email = "josip@dump.hr",
                        UserName = "jsvalina",
                    };

                    _userManager.Create(regularUser, "123456");
                    _userManager.AddToRole(regularUser.Id, "User");
                }

            }
        }
    }
}