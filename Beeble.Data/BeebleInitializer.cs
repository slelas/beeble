using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beeble.Data.Models;

namespace Beeble.Data
{
    public class BeebleInitializer : DropCreateDatabaseIfModelChanges<AuthContext>
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
            var category3 = new Category()
            {
                Name = "Autobiografija"
            };
            var category4 = new Category()
            {
                Name = "Ep"
            };
            var category5 = new Category()
            {
                Name = "Drama"
            };
            var category6 = new Category()
            {
                Name = "Komedija"
            };


            var language1 = new Language()
	        {
		        Name = "Croatian"
	        };
	        var language2 = new Language()
	        {
		        Name = "English"
	        };
	        var language3 = new Language()
	        {
		        Name = "French"
	        };
            var language4 = new Language()
            {
                Name = "Spanish"
            };
            var language5 = new Language()
            {
                Name = "Bosnian"
            };
            var language6 = new Language()
            {
                Name = "Chinese"
            };

	        var nationality1 = new Nationality()
	        {
		        Name = "Croatia"
	        };
	        var nationality2 = new Nationality()
	        {
		        Name = "England"
	        };
	        var nationality3 = new Nationality()
	        {
		        Name = "USA"
	        };
	        var nationality4 = new Nationality()
	        {
		        Name = "France"
	        };
	        var nationality5 = new Nationality()
	        {
		        Name = "Germany"
	        };
	        var nationality6 = new Nationality()
	        {
		        Name = "Ireland"
	        };

            var year1 = new YearOfIssue()
            {
                Year = 2001
            };

            var year2 = new YearOfIssue()
            {
                Year = 2005
            };

            var year3 = new YearOfIssue()
            {
                Year = 2017
            };

            var year4 = new YearOfIssue()
            {
                Year = 1931
            };

            var author1 = new Author()
            {
                Name = "Walter Isaacson"
            };

            var author2 = new Author()
            {
                Name = "Shakespeare"
            };

            var author3 = new Author()
            {
                Name = "Mato Lovrak"
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
		        NumOfPages = "577",
		        Author = author1,
		        YearOfIssue = year1,
		        ISBN = "1451648537",
		        DamageLevel = 2,
		        Description = "desc",
		        Publisher = " Simon & Schuster",
		        LocalLibrary = localLibrary1,
				Language = language1,
		        Nationality = nationality3,
                Categories = new List<Category>() { category2 },
                ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/81VStYnDGrL.jpg"
            };

            var book2 = new Book()
            {
                Name = "Vlak u snijegu",
                NumOfPages = "25",
                Author = author3,
                YearOfIssue = year4,
                ISBN = "9531963827",
                DamageLevel = 3,
                Description = "desc",
                Publisher = "Mozaik knjiga",
                LocalLibrary = localLibrary1,
	            Language = language1,
				Nationality = nationality1,
                Categories = new List<Category>() { category1 },
                ImageUrl = "https://mojtv.hr//images/ff6b3c8b-ba33-4050-9f1f-34d3a35ffbd5.jpg"
            };

            var book3 = new Book()
            {
                Name = "Hamlet",
                NumOfPages = "115",
                Author = author2,
                YearOfIssue = year2,
                ISBN = "0230217867",
                DamageLevel = 2,
                Description = "desc",
                Publisher = "Cambridge Press",
                LocalLibrary = localLibrary1,
                Nationality = nationality2,
	            Language = language1,
				Categories = new List<Category>() { category5 },
                ImageUrl = "https://static.enotes.com/images/covers%2Fhamlet.jpg"
            };

            var book4 = new Book()
            {
                Name = "Macbeth",
                NumOfPages = "89",
                Author = author2,
                YearOfIssue = year2,
                ISBN = "0486278026",
                DamageLevel = 1,
                Description = "desc",
                Publisher = "Cambridge Press",
                LocalLibrary = localLibrary1,
                Nationality = nationality2,
	            Language = language1,
				Categories = new List<Category>() { category5 },
                ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/419CMC5SR1L._SX308_BO1,204,203,200_.jpg"
            };

            var book5 = new Book()
            {
                Name = "Romeo i Julija",
                NumOfPages = "155",
                Author = author2,
                YearOfIssue = year3,
                ISBN = "1505259568",
                DamageLevel = 1,
                Description = "desc",
                Publisher = "Cambridge Press",
                LocalLibrary = localLibrary1,
                Nationality = nationality2,
	            Language = language1,
				Categories = new List<Category>() { category5 },
                ImageUrl = "https://static.enotes.com/images/covers%2Fromeo-and-juliet.jpg"
            };

            var book6 = new Book()
            {
                Name = "Romeo i Julija",
                NumOfPages = "155",
                Author = author2,
                YearOfIssue = year3,
                ISBN = "1505259568",
                // only property different from book5:
                DamageLevel = 3,
                Description = "desc",
                Publisher = "Cambridge Press",
                LocalLibrary = localLibrary1,
                Nationality = nationality2,
	            Language = language1,
				Categories = new List<Category>() { category5 },
                ImageUrl = "https://static.enotes.com/images/covers%2Fromeo-and-juliet.jpg"
            };

            context.Categories.Add(category1);
	        context.Categories.Add(category2);
            context.Categories.Add(category3);
            context.Categories.Add(category4);
            context.Categories.Add(category5);
            context.Categories.Add(category6);

            context.Nationalities.Add(nationality1);
	        context.Nationalities.Add(nationality2);
	        context.Nationalities.Add(nationality3);
            context.Nationalities.Add(nationality4);
            context.Nationalities.Add(nationality5);
            context.Nationalities.Add(nationality6);

            context.LocalLibraries.Add(localLibrary1);

	        context.Books.Add(book1);
	        context.Books.Add(book2);
	        context.Books.Add(book3);
            context.Books.Add(book4);
            context.Books.Add(book5);
            context.Books.Add(book6);

            base.Seed(context);
        }
    }
}
