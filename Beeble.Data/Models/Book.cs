using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beeble.Data.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NumOfPages { get; set; }
        public string Author { get; set; }
        public int YearOfIssue { get; set; }
        public string ISBN { get; set; }
        public int DamageLevel { get; set; }
        public string Description { get; set; }

        public List<Category> Categories { get; set; }
        public Nationality Nationality { get; set; }
        public LocalLibrary LocalLibrary { get; set; }

    }
}
