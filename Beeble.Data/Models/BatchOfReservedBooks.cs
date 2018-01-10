using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beeble.Data.Models
{
    public class BatchOfReservedBooks
    {
        public int Id { get; set; }
        public LocalLibraryMember LibraryMember { get; set; }
        public DateTime? PickupDeadline { get; set; }
        public bool IsGuestMember { get; set; }
    }
}
