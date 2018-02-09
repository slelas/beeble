using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using Beeble.Domain.DTOs;

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
            var user = new OnlineUser
            {
                UserName = userModel.UserName,
                Email = userModel.UserName,
                Name = userModel.Name,
                LastName = userModel.Lastname,
                Oib = userModel.Oib,
                Address = userModel.Address,
                City = userModel.City,
                PhoneNumber = userModel.PhoneNumber,
                LocalLibraryMembers = new List<LocalLibraryMember>()
			};

            var result = await _userManager.CreateAsync(user, userModel.Password);

            await _userManager.AddToRoleAsync(user.Id, "User");

            return result;
        }

	    public async Task<bool> EditUser(UserModel userModel, Guid? userId)
	    {
		    var user = await _userManager.FindByIdAsync(userId.ToString());
		    var isOkPassword = await _userManager.PasswordValidator.ValidateAsync(userModel.Password);
			if (isOkPassword.Succeeded)
		    {
				user.PasswordHash = _userManager.PasswordHasher.HashPassword(userModel.Password);
			}

			user.Address = userModel.Address;
		    user.City = userModel.City;
		    user.LastName = userModel.Lastname;
		    user.Name = userModel.Name;
		    user.PhoneNumber = userModel.PhoneNumber;
		    user.Oib = userModel.Oib;

			// returns the number of changed lines
		    return _ctx.SaveChanges() > 0;
	    }

        public async Task<OnlineUser> FindUser(string userName, string password)
        {
            var user = await _userManager.FindAsync(userName, password);

            return user;
        }

        public string GetUserRole(string userId)
        {
            using (var context = new AuthContext())
            {

                var roleId = context.Users.FirstOrDefault(user => user.Id == userId).Roles.FirstOrDefault().RoleId;

                return context.Roles.FirstOrDefault(role => role.Id == roleId).Name;
            }
        }

		public OnlineUserDTO GetUser(Guid? userId)
		{
			using (var context = new AuthContext())
			{
				var user = context.Users
					.Include("LocalLibraryMembers")
					.Include("LocalLibraryMembers.BatchesOfBorrowedBooks")
					.Include("LocalLibraryMembers.BatchesOfBorrowedBooks.Books")
					.Include("LocalLibraryMembers.BatchesOfBorrowedBooks.Books.Author")
					.Include("LocalLibraryMembers.Reservations")
					.Include("LocalLibraryMembers.Reservations.Book")
                    .Include("LocalLibraryMembers.Reservations.Book.Author")
                    .SingleOrDefault(onlineUser => onlineUser.Id == userId.ToString());

				return OnlineUserDTO.FromData(user);
			}


		}

		public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();

        }

	}
}