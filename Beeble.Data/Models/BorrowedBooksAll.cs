using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beeble.Data.Models
{
    public class BorrowedBooksAll
    {
        public int Id { get; set; }
        public LocalLibrary LocalLibrary { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool WasBookReserved { get; set; }
    }
}
