using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Beeble.Api.Models;
using Beeble.Api.UserManager;
using Beeble.Data;
using Beeble.Data.Models;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Web.Http;

namespace Beeble.Api
{
    public class AuthRepository : IDisposable
    {
        private AuthContext _ctx;

        private UserManager<OnlineUser> _userManager;

        public AuthRepository()
        {
            _ctx = new AuthContext();
            _userManager = new UserManager<OnlineUser>(new UserStore<OnlineUser>(_ctx));
        }

        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            OnlineUser user = new OnlineUser
            {
                UserName = userModel.UserName
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);

            await _userManager.AddToRoleAsync(user.Id, "User");

            return result;
        }

        public async Task<OnlineUser> FindUser(string userName, string password)
        {
            OnlineUser user = await _userManager.FindAsync(userName, password);

            return user;
        }

        public string GetUserRole(string userId)
        {
            using (var context = new AuthContext())
            {

                var roleId = context.Users.Where(x => x.Id == userId).FirstOrDefault().Roles.FirstOrDefault().RoleId;
                return context.Roles.Where(x => x.Id == roleId).FirstOrDefault().Name;
            }
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();

        }

	}
}