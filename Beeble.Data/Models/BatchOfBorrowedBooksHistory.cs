using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beeble.Data.Models
{
    public class BatchOfBorrowedBooksHistory
    {
        public int Id { get; set; }
        public DateTime? ReturnDeadline { get; set; }
        public DateTime? PickupDate { get; set; }
        public bool WasPreviouslyReserved { get; set; }
        public bool IsGuestBorrowed { get; set; }

        public LocalLibraryMember LibraryMember { get; set; }
        public List<Book> Books { get; set; }
    }
}
