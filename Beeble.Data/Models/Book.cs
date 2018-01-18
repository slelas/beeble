using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beeble.Data.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
        public string Name { get; set; }
        public string NumOfPages { get; set; }
        public string ISBN { get; set; }
        public int DamageLevel { get; set; }
        public string Description { get; set; }
        public string Publisher { get; set; }
        public string ImageUrl { get; set; }
	    public bool IsBorrowed { get; set; }
        public bool IsReserved { get; set; }


        public Author Author { get; set; }
        public YearOfIssue YearOfIssue { get; set; }
        public List<Category> Categories { get; set; }
        public LocalLibrary LocalLibrary { get; set; }
	    public Nationality Nationality { get; set; }
	    public Language Language { get; set; }

	}
}
