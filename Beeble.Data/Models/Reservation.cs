using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beeble.Data.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime? PickupDeadline { get; set; }
        public bool IsGuestMember { get; set; }

        public LocalLibraryMember LibraryMember { get; set; }
        public Book Book { get; set; }
    }
}
