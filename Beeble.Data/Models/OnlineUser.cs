using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Beeble.Data.Models
{
    public class OnlineUser : IdentityUser
    {
        public string Name { get; set; }
        public string LastName { get; set; }
		public string Oib { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string PhoneNumber { get; set; }
        //payment option
        public bool IsVerifiedLocal { get; set; }
        public bool IsGuest { get; set; }
        public DateTime? MembershipExpiryDate { get; set; }
        public bool IsVerifiedGlobal { get; set; }

        public List<LocalLibraryMember> LocalLibraryMembers { get; set; }
    }
}
