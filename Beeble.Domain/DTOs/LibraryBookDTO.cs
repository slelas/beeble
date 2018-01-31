using Beeble.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beeble.Domain.DTOs
{
    public class LibraryBookDTO
    {
        public long BookId { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int DamageLevel { get; set; }
        public int NumberOfBooks { get; set; }
        public int NumberOfReservedBooks { get; set; }
        public int NumberOfBorrowedBooks { get; set; }

        public static LibraryBookDTO FromData(List<Book> booksInLibrary)
        {
            var book = booksInLibrary[0];

            return new LibraryBookDTO()
            {
                BookId = book.Id,
                Name = book.Name,
                Author = book.Author.Name,
                DamageLevel = (int)Math.Round((double)(booksInLibrary.Select(_book => _book.DamageLevel).Aggregate((sum,ele) => sum += ele) / booksInLibrary.Count)),
                NumberOfBooks = booksInLibrary.Count,
                NumberOfReservedBooks = booksInLibrary.Where(_book => _book.IsReserved).Count(),
                NumberOfBorrowedBooks = booksInLibrary.Where(_book => _book.IsBorrowed).Count()
            };
        }
    }
}
