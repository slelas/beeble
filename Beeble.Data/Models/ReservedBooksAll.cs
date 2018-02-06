using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beeble.Data.Models
{
    public class ReservedBooksAll
    {
        public int Id { get; set; }
        public LocalLibrary LocalLibrary { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
