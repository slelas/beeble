using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beeble.Data.Models;

namespace Beeble.Data
{
    public class BeebleInitializer : DropCreateDatabaseAlways<AuthContext>
    {
        protected override void Seed(AuthContext context)
        {
			// seed categories
	        var category1 = new Category()
	        {
		        Name = "Roman"
	        };
	        var category2 = new Category()
	        {
		        Name = "Biografija"
	        };
	        var nationality1 = new Nationality()
	        {
		        Name = "HRV"
	        };
	        var nationality2 = new Nationality()
	        {
		        Name = "EN"
	        };
	        var nationality3 = new Nationality()
	        {
		        Name = "EN"
	        };
			var localLibrary1 = new LocalLibrary()
	        {
		        Address = "Placeholder adresa",
		        BookLendLimit = 4,
		        DefaultLendDuration = new TimeSpan(3, 0, 0),
		        GuestBorrowPrice = 20,
		        IBAN = "456",
		        Members = null,
		        MembershipPrice = 20,
		        Name = "Marko Marulic",
		        OIB = "2456",
				OpenHours = "2-2",
		        ReservationDuration = new TimeSpan(3, 0, 0),

			};
			
			// seed books
	        var book1 = new Book()
	        {
		        Name = "Steve Jobs",
		        NumOfPages = "25",
		        Author = "Walter Levin",
		        YearOfIssue = 2001,
		        ISBN = "2545642",
		        DamageLevel = 2,
		        Description = "desc",
		        Publisher = "profil",
		        LocalLibrary = localLibrary1,
		        Nationality = nationality1
	        };

	        var book2 = new Book()
	        {
		        Name = "Vlak u snijegu",
		        NumOfPages = "25",
		        Author = "Walter Levin",
		        YearOfIssue = 2001,
		        ISBN = "2545642",
		        DamageLevel = 2,
		        Description = "desc",
		        Publisher = "profil",
		        LocalLibrary = localLibrary1,
		        Nationality = nationality2
	        };

	        var book3 = new Book()
	        {
		        Name = "Med i mlijeko",
		        NumOfPages = "25",
		        Author = "Walter Levin",
		        YearOfIssue = 2001,
		        ISBN = "2545642",
		        DamageLevel = 2,
		        Description = "desc",
		        Publisher = "profil",
		        LocalLibrary = localLibrary1,
		        Nationality = nationality2
	        };

	        context.Categories.Add(category1);
	        context.Categories.Add(category2);

	        context.Nationalities.Add(nationality1);
	        context.Nationalities.Add(nationality2);
	        context.Nationalities.Add(nationality3);

	        context.LocalLibraries.Add(localLibrary1);

	        context.Books.Add(book1);
	        context.Books.Add(book2);
	        context.Books.Add(book3);

			base.Seed(context);
        }
    }
}
