using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beeble.Data.Models;

namespace Beeble.Domain.DTOs
{
    public class ExportObjectDTO
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public int BorrowCount { get; set; }
        public int ReserveCount { get; set; }

        public static ExportObjectDTO FromData (IGrouping<string, Book> books)
        {
            return new ExportObjectDTO()
            {
                Author = books.FirstOrDefault().Author.Name,
                Name = books.FirstOrDefault().Name,
                BorrowCount = books.Sum(book => book.BorrowCount),
                ReserveCount = books.Sum(book => book.ReserveCount)
            };
        }

    }
}
