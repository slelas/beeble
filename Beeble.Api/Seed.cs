using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Web;
using Beeble.Data;
using Beeble.Data.Models;

namespace Beeble.Api
{
    public class Seed
    {
        public static void Execute()
        {
            using (var context = new AuthContext())
            {
                var store = new UserStore<OnlineUser>(context);
                var _userManager = new UserManager<OnlineUser>(new UserStore<OnlineUser>(context));

                if (!context.Users.Any())
                {
                    var adminUser = new OnlineUser()
                    {
                        Email = "zvonimird@dump.hr",
                        UserName = "zdelas",
                    };

                    _userManager.Create(adminUser, "123456");
                    _userManager.AddToRole(adminUser.Id, "Admin");

                    var regularUser = new OnlineUser()
                    {
                        Email = "josip@dump.hr",
                        UserName = "jsvalina",
                        LocalLibraryMembers = new List<LocalLibraryMember>()
                    };

	                var regularUser2 = new OnlineUser()
	                {
		                Email = "stipe@dump.hr",
		                UserName = "slelas",
                        LocalLibraryMembers = new List<LocalLibraryMember>()
                    };

					_userManager.Create(regularUser, "123456");
                    _userManager.AddToRole(regularUser.Id, "User");

	                _userManager.Create(regularUser2, "123456");
	                _userManager.AddToRole(regularUser2.Id, "User");

					// seed categories
					var category1 = new Category() { Name = "Roman" };
                    var category2 = new Category() { Name = "Biografija" };
                    var category3 = new Category() { Name = "Autobiografija" };
                    var category4 = new Category() { Name = "Ep" };
                    var category5 = new Category() { Name = "Drama" };
					var category6 = new Category() {Name = "Komedija"};

                    var language1 = new Language() {Name = "Hrvatski"};
					var language2 = new Language() {Name = "Engleski"};
					var language3 = new Language() {Name = "Francuski"};
					var language4 = new Language() {Name = "Spanjolski"};
					var language5 = new Language() {Name = "Talijanski"};
					var language6 = new Language() {Name = "Kineski"};

	                var nationality1 = new Nationality() {Name = "Croatia"};
	                var nationality2 = new Nationality() {Name = "England"};
	                var nationality3 = new Nationality() {Name = "USA"};
	                var nationality4 = new Nationality() {Name = "France"};
	                var nationality5 = new Nationality() {Name = "Germany"};
	                var nationality6 = new Nationality() {Name = "Ireland"};

					var year1 = new YearOfIssue(){Year = "2001"};
                    var year2 = new YearOfIssue() { Year = "2005" };
	                var year3 = new YearOfIssue() {Year = "2017"};
	                var year4 = new YearOfIssue() {Year = "1931"};

					var author1 = new Author(){Name = "Walter Isaacson"};
                    var author2 = new Author() { Name = "Shakespeare" };
					var author3 = new Author(){Name = "Mato Lovrak"};

					var localLibrary1 = new LocalLibrary()
					{
						Address = "Placeholder adresa",
						BookLendLimit = 4,
						DefaultLendDuration = 48,
						GuestBorrowPrice = 20,
						IBAN = "456",
						Members = null,
						MembershipPrice = 20,
						Name = "Marko Marulic",
						OIB = "2456",
						OpenHours = "2-2",
						ReservationDuration = 48,
						Email = "test@test.hr",
						Number = "0215678456"
					};

	                var localLibrary2 = new LocalLibrary()
	                {
		                Address = "Placeholder adresa 222",
		                BookLendLimit = 5,
		                DefaultLendDuration = 48,
		                GuestBorrowPrice = 20,
		                IBAN = "456",
		                Members = null,
		                MembershipPrice = 20,
		                Name = "GK Solin",
		                OIB = "2456",
		                OpenHours = "2-2",
		                ReservationDuration = 48,
		                Email = "test@test.hr",
		                Number = "0215678456"
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
						Nationality = nationality2,
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

	                var book7 = new Book()
	                {
		                Name = "Romeo i Julija",
		                NumOfPages = "155",
		                Author = author2,
		                YearOfIssue = year3,
		                ISBN = "1505259568",
		                DamageLevel = 3,
		                Description = "desc",
		                Publisher = "Cambridge Press",
						// only differing property from book6
		                LocalLibrary = localLibrary2,
		                Nationality = nationality2,
		                Language = language1,
		                Categories = new List<Category>() { category5 },
		                ImageUrl = "https://static.enotes.com/images/covers%2Fromeo-and-juliet.jpg"
	                };

	                var book8 = new Book()
	                {
		                Name = "Hamlet",
		                NumOfPages = "115",
		                Author = author2,
		                YearOfIssue = year2,
		                ISBN = "0230217867",
		                DamageLevel = 2,
		                Description = "desc",
		                Publisher = "Cambridge Press",
		                LocalLibrary = localLibrary2,
		                Nationality = nationality2,
		                Language = language1,
		                Categories = new List<Category>() { category5 },
		                ImageUrl = "https://static.enotes.com/images/covers%2Fhamlet.jpg",
						IsBorrowed = false,
                        IsReserved = false
	                };

	                var book9 = new Book()
	                {
		                Name = "Hamlet",
		                NumOfPages = "115",
		                Author = author2,
		                YearOfIssue = year2,
		                ISBN = "0230217867",
		                DamageLevel = 2,
		                Description = "desc",
		                Publisher = "Cambridge Press",
		                LocalLibrary = localLibrary2,
		                Nationality = nationality2,
		                Language = language1,
		                Categories = new List<Category>() { category5 },
		                ImageUrl = "https://static.enotes.com/images/covers%2Fhamlet.jpg",
		                IsBorrowed = false,
		                IsReserved = false
	                };

					var localLibraryMember1 = new LocalLibraryMember()
					{
						LocalLibrary = localLibrary1,
						OnlineUser = regularUser,
						Id = 4654654654,
						MembershipExpiryDate = new DateTime(2020,5,1)
					};

	                var localLibraryMember2 = new LocalLibraryMember()
	                {
						Name = "Mate",
						LastName = "Matic",
						Email = "test@gmail.hr",
						PhoneNumber = "0915478522",
						Oib = "12345",
						Address = "adresa 123",
		                LocalLibrary = localLibrary2,
		                OnlineUser = regularUser2,
		                Id = 9999999999,
						MembershipExpiryDate = new DateTime(2025, 5, 1)
	                };

                    var localLibraryMember3 = new LocalLibraryMember()
                    {
                        LocalLibrary = localLibrary2,
                        OnlineUser = null,
                        Id = 1234554321,
                        MembershipExpiryDate = new DateTime(2025, 5, 1)
                    };

					var batchOfBorrowedBooks1 = new BatchOfBorrowedBooks()
					{
						LibraryMember = localLibraryMember1,
						PickupDate = new DateTime(2018, 1, 2),
						ReturnDeadline = new DateTime(2018, 3, 2),
						Books = new List<Book> { book1, book2 },
					};

					var batchOfBorrowedBooks2 = new BatchOfBorrowedBooks()
					{
						LibraryMember = localLibraryMember1,
						PickupDate = new DateTime(2018, 1, 2),
						ReturnDeadline = new DateTime(2020, 5, 5),
						Books = new List<Book> { book3 },
					};

					var batchOfBorrowedBooks3 = new BatchOfBorrowedBooks()
					{
						LibraryMember = localLibraryMember2,
						PickupDate = new DateTime(2018, 1, 2),
						ReturnDeadline = new DateTime(2020, 5, 5),
						Books = new List<Book> { book4 },
					};

					var reservation1 = new Reservation()
					{
						LibraryMember = localLibraryMember1,
						PickupDeadline = new DateTime(2016, 1, 2),
						Book = book2
					};

					var reservation2 = new Reservation()
					{
						LibraryMember = localLibraryMember1,
						PickupDeadline = new DateTime(2018, 1, 2),
						Book = book7
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
	                context.LocalLibraries.Add(localLibrary2);

					context.Books.Add(book1);
					context.Books.Add(book2);
					context.Books.Add(book3);
					context.Books.Add(book4);
					context.Books.Add(book5);
					context.Books.Add(book6);
	                context.Books.Add(book7);
	                context.Books.Add(book8);
	                context.Books.Add(book9);

					context.BatchesOfBorrowedBooks.Add(batchOfBorrowedBooks1);
					context.BatchesOfBorrowedBooks.Add(batchOfBorrowedBooks2);
					context.BatchesOfBorrowedBooks.Add(batchOfBorrowedBooks3);

					context.Reservations.Add(reservation1);
					context.Reservations.Add(reservation2);

					context.LocalLibraryMembers.Add(localLibraryMember1);
                    context.LocalLibraryMembers.Add(localLibraryMember2);
                    context.LocalLibraryMembers.Add(localLibraryMember3);

                    context.SaveChanges();
                }

            }
        }
    }
}