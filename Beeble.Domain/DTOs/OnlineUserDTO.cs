using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beeble.Data.Models;

namespace Beeble.Domain.DTOs
{
	public class OnlineUserDTO
	{
		public string Name { get; set; }
		public string LastName { get; set; }
		public string Oib { get; set; }
		public string Email { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string PhoneNumber { get; set; }


		public static OnlineUserDTO FromData(OnlineUser onlineUser)
		{
			var a = onlineUser;

			return new OnlineUserDTO()
			{
				Name = onlineUser.Name,
				Email = onlineUser.Email,
				LastName = onlineUser.LastName,
				Oib = onlineUser.Oib,
				Address = onlineUser.Address,
				City = onlineUser.City,
				PhoneNumber = onlineUser.PhoneNumber
			};
		}
	}
}
