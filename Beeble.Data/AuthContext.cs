using System.Data.Entity;
using Beeble.Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Beeble.Data
{
    public class AuthContext : IdentityDbContext<OnlineUser>
    {
        public AuthContext()
            : base("AuthContext")
        {
            Database.SetInitializer(new BeebleInitializer());
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BatchOfBorrowedBooks> BorrowedBooks { get; set; }
        public virtual DbSet<BatchOfBorrowedBooksHistory> BorrowedBooksHistories { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<LocalLibrary> LocalLibraries { get; set; }
        public virtual DbSet<LocalLibraryMember> LocalLibraryMembers { get; set; }
        public virtual DbSet<Nationality> Nationalities { get; set; }
        public virtual DbSet<BatchOfReservedBooks> ReservedBooks { get; set; }
	    public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<YearOfIssue> YearsOfIssue { get; set; }

    }
}