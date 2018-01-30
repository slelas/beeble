using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beeble.Data.Models
{
    public class LocalLibrary
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string OIB { get; set; }
        public string OpenHours { get; set; }
        public string IBAN { get; set; }
        public int MembershipPrice { get; set; }
        public int DefaultLendDuration  { get; set; }
        public int BookLendLimit { get; set; }
        public int GuestBorrowPrice { get; set; }
        public int ReservationDuration { get; set; }
	    public string Number { get; set; }
	    public string Email { get; set; }

		public List<LocalLibraryMember> Members { get; set; }
        public List<OnlineUser> Administrators { get; set; }

    }
}
