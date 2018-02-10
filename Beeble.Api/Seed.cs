﻿using System.Linq;
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

                    _userManager.Create(adminUser, "123456nova");
                    _userManager.AddToRole(adminUser.Id, "Admin");

                    var regularUser = new OnlineUser()
                    {
                        Name = "Josip",
                        LastName = "Svalina",
                        Address = "Kralja Tomislava 54",
                        Oib = "95874125896",
                        City = "Split",
                        PhoneNumber = "095 4224 247",
                        Email = "josip@dump.hr",
                        UserName = "josip@dump.hr",
                        LocalLibraryMembers = new List<LocalLibraryMember>()
                    };

                    var regularUser2 = new OnlineUser()
                    {
                        Email = "stipe@dump.hr",
                        UserName = "slelas",
                        LocalLibraryMembers = new List<LocalLibraryMember>()
                    };

                    _userManager.Create(regularUser, "123456nova");
                    _userManager.AddToRole(regularUser.Id, "User");

                    _userManager.Create(regularUser2, "123456nova");
                    _userManager.AddToRole(regularUser2.Id, "User");

                    #region Category seed
                    var category1 = new Category() { Name = "Roman" };
                    var category2 = new Category() { Name = "Biografija" };
                    var category3 = new Category() { Name = "Autobiografija" };
                    var category4 = new Category() { Name = "Ep" };
                    var category5 = new Category() { Name = "Drama" };
                    var category6 = new Category() { Name = "Komedija" };
                    var category7 = new Category() { Name = "Kuharica" };
                    var category8 = new Category() { Name = "Programiranje" };
                    var category9 = new Category() { Name = "Dizajn" };
                    #endregion

                    #region Language seed
                    var language1 = new Language() { Name = "Hrvatski" };
                    var language2 = new Language() { Name = "Engleski" };
                    var language3 = new Language() { Name = "Francuski" };
                    var language4 = new Language() { Name = "Spanjolski" };
                    var language5 = new Language() { Name = "Talijanski" };
                    var language6 = new Language() { Name = "Kineski" };
                    #endregion

                    #region Nationality seed
                    var nationality1 = new Nationality() { Name = "Hrvatska" };
                    var nationality2 = new Nationality() { Name = "Engleska" };
                    var nationality3 = new Nationality() { Name = "USA" };
                    var nationality4 = new Nationality() { Name = "France" };
                    var nationality5 = new Nationality() { Name = "Germany" };
                    var nationality6 = new Nationality() { Name = "Ireland" };
                    #endregion

                    #region Year seed
                    var year1 = new YearOfIssue() { Year = "2001" };
                    var year2 = new YearOfIssue() { Year = "2015" };
                    var year3 = new YearOfIssue() { Year = "2017" };
                    var year4 = new YearOfIssue() { Year = "1931" };
                    #endregion;

                    #region Author seed
                    var author1 = new Author() { Name = "Walter Isaacson" };
                    var author2 = new Author() { Name = "Shakespeare" };
                    var author3 = new Author() { Name = "Mato Lovrak" };
                    var author4 = new Author() { Name = "Jamie Oliver" };
                    var author5 = new Author() { Name = "Adrija Nović" };
                    var author6 = new Author() { Name = "Josipa Katalonić" };
                    var author7 = new Author() { Name = "Ivan Buržan" };
                    var author8 = new Author() { Name = "Rob Miles" };
                    var author9 = new Author() { Name = "John Allwork" };
                    var author10 = new Author() { Name = "Joseph Albahari & Ben Albahari" };
                    var author11 = new Author() { Name = "Milan Gocić" };
                    var author12 = new Author() { Name = "Mark J. Price" };
                    var author13 = new Author() { Name = "Robin Landa" };
                    var author14 = new Author() { Name = "Charlotte & Peter Fiell" };
                    var author15 = new Author() { Name = "Feđa Vukić" };
                    var author16 = new Author() { Name = "Robert Bringhurst" };
                    var author17 = new Author() { Name = "Gavin Ambrose" };
                    var author18 = new Author() { Name = "Alina Wheeler" };
                    var author19 = new Author() { Name = "Jonathan Swift" };
                    var author20 = new Author() { Name = "Simon Sinek" };
                    #endregion

                    #region Library seed
                    var localLibrary1 = new LocalLibrary()
                    {
                        Address = "Matice hrvatske 12, Split",
                        BookLendLimit = 4,
                        DefaultLendDuration = 48,
                        GuestBorrowPrice = 20,
                        IBAN = "456",
                        Members = null,
                        MembershipPrice = 20,
                        Name = "Marko Marulic",
                        OIB = "2456",
                        OpenHours = "8-16",
                        ReservationDuration = 48,
                        Email = "test@test.hr",
                        Number = "0215678456",
                        YearEnrolled = 2016
                    };

                    var localLibrary2 = new LocalLibrary()
                    {
                        Address = "Kralja Zvonimira 105, Solin",
                        BookLendLimit = 5,
                        DefaultLendDuration = 48,
                        GuestBorrowPrice = 20,
                        IBAN = "456",
                        Members = null,
                        MembershipPrice = 20,
                        Name = "GK Šibenik",
                        OIB = "2456",
                        OpenHours = "8-16",
                        ReservationDuration = 48,
                        Email = "test@test.hr",
                        Number = "0215678456",
                        YearEnrolled = 2016
                    };

                    var localLibrary3 = new LocalLibrary()
                    {
                        Address = "Stjepana Radića 11b, Zadar",
                        BookLendLimit = 5,
                        DefaultLendDuration = 48,
                        GuestBorrowPrice = 20,
                        IBAN = "456",
                        Members = null,
                        MembershipPrice = 20,
                        Name = "GK Zadar",
                        OIB = "2456",
                        OpenHours = "8-16",
                        ReservationDuration = 72,
                        Email = "test@test.hr",
                        Number = "0215678456",
                        Administrators = new List<OnlineUser>() { adminUser },
                        YearEnrolled = 2016
                    };

                    var localLibrary4 = new LocalLibrary()
                    {
                        Address = "Trg palih za domovinu 1, Kaštel Štafilić",
                        BookLendLimit = 5,
                        DefaultLendDuration = 48,
                        GuestBorrowPrice = 20,
                        IBAN = "456",
                        Members = null,
                        MembershipPrice = 20,
                        Name = "Knjižnica Vodice",
                        OIB = "2456",
                        OpenHours = "8-16",
                        ReservationDuration = 48,
                        Email = "test@test.hr",
                        Number = "0215678456",
                        YearEnrolled = 2016
                    };
                    #endregion

                    #region Miscellaneous books seed
                    var book1 = new Book()
                    {
                        Name = "Steve Jobs",
                        NumOfPages = "577",
                        Author = author1,
                        YearOfIssue = year1,
                        ISBN = "1451648537",
                        Id = 1111111111,
                        DamageLevel = 2,
                        Description = "desc",
                        Publisher = " Simon & Schuster",
                        LocalLibrary = localLibrary1,
                        Language = language1,
                        Nationality = nationality3,
                        Categories = new List<Category>() { category2 },
                        ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/81VStYnDGrL.jpg",
                        LateReturnFee = 0.5
                    };

                    var book2 = new Book()
                    {
                        Id = 2222222222,
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
                        ImageUrl = "https://mojtv.hr//images/ff6b3c8b-ba33-4050-9f1f-34d3a35ffbd5.jpg",
                        LateReturnFee = 1.5
                    };

                    var book3 = new Book()
                    {
                        Id = 1111111112,
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
                        ImageUrl = "https://static.enotes.com/images/covers%2Fhamlet.jpg",
                        LateReturnFee = 2.5
                    };

                    var book4 = new Book()
                    {
                        Id = 1111111113,
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
                        ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/419CMC5SR1L._SX308_BO1,204,203,200_.jpg",
                        LateReturnFee = 2.5
                    };

                    var book5 = new Book()
                    {
                        Id = 1111111114,
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
                        Id = 1111111115,
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
                        Id = 1111111116,
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
                        Id = 1111111117,
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
                        Id = 1111111118,
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

                    var book44 = new Book()
                    {
                        Name = "Gulliverova putovanja",
                        NumOfPages = "115",
                        Author = author19,
                        YearOfIssue = year2,
                        ISBN = "0230217867",
                        DamageLevel = 2,
                        Description = "Gulliverova putovanja zanimljiv je pustolovni roman književnika Jonathana Swifta. Swift je u svom djelu iznio svoje osobno mišljenje o Engleskoj i njezinoj politici.",
                        Publisher = "Cambridge Press",
                        LocalLibrary = localLibrary1,
                        Nationality = nationality2,
                        Language = language1,
                        Categories = new List<Category>() { category1 },
                        ImageUrl = "https://www.superknjizara.hr/photo/knjige/m_100054563.jpg",
                        IsBorrowed = false,
                        IsReserved = false,
                        BarcodeNumber = "9789537160357"
                    };

                    var book45 = new Book()
                    {
                        Name = "Gulliverova putovanja",
                        NumOfPages = "115",
                        Author = author19,
                        YearOfIssue = year2,
                        ISBN = "0230217867",
                        DamageLevel = 2,
                        Description = "Gulliverova putovanja zanimljiv je pustolovni roman književnika Jonathana Swifta. Swift je u svom djelu iznio svoje osobno mišljenje o Engleskoj i njezinoj politici.",
                        Publisher = "Cambridge Press",
                        LocalLibrary = localLibrary2,
                        Nationality = nationality2,
                        Language = language1,
                        Categories = new List<Category>() { category1 },
                        ImageUrl = "https://www.superknjizara.hr/photo/knjige/m_100054563.jpg",
                        IsBorrowed = false,
                        IsReserved = false,
                        BarcodeNumber = "9789537160350"
                    };

                    var book46 = new Book()
                    {
                        Name = "Gulliverova putovanja",
                        NumOfPages = "115",
                        Author = author19,
                        YearOfIssue = year2,
                        ISBN = "0230217867",
                        DamageLevel = 2,
                        Description = "Gulliverova putovanja zanimljiv je pustolovni roman književnika Jonathana Swifta. Swift je u svom djelu iznio svoje osobno mišljenje o Engleskoj i njezinoj politici.",
                        Publisher = "Cambridge Press",
                        LocalLibrary = localLibrary3,
                        Nationality = nationality2,
                        Language = language1,
                        Categories = new List<Category>() { category1 },
                        ImageUrl = "https://www.superknjizara.hr/photo/knjige/m_100054563.jpg",
                        IsBorrowed = false,
                        IsReserved = false,
                        BarcodeNumber = "9789537160351"
                    };

                    var book47 = new Book()
                    {
                        Name = "Leaders Eat Last",
                        NumOfPages = "115",
                        Author = author20,
                        YearOfIssue = year2,
                        ISBN = "0230217867",
                        DamageLevel = 2,
                        Description = "Zašto neki timovi uspijevaju, an neki ne? Zašto vjerno slijedimo neke vođe, a drugima okrećemo leđa čim naiđe bolja prilika? Simon Sinek u ovom bestselleru otkriva odgovore na ova, ali i mnoga druga pitanja.",
                        Publisher = "Cambridge Press",
                        LocalLibrary = localLibrary3,
                        Nationality = nationality2,
                        Language = language1,
                        Categories = new List<Category>() { category1 },
                        ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/51ejdeSXFjL.jpg",
                        IsBorrowed = true,
                        IsReserved = false,
                        BarcodeNumber = "9780670923175"
                    };
                    #endregion Miscellaneous books seed

                    #region Cooking books seed
                    var book10 = new Book()
                    {
                        Name = "Super hrana",
                        NumOfPages = "115",
                        Author = author4,
                        YearOfIssue = year2,
                        ISBN = "0230217867",
                        DamageLevel = 2,
                        Description = "Odaberete li samo poneku ideju iz ove knjige, počet ćete drukčije razmišljati o hrani i njezinoj moći da izravno utječe na vas, i tjelesno i mentalno, što će vas nadahnuti da pozitivno promijenite ne samo način na koji jedete, nego i, nadam se, prehrambene navike osoba koje vas okružuju. Hranu treba cijeniti, dijeliti je i u njoj uživati, a zdrava i hranjiva jela trebaju biti šarolika, ukusna i, što je najvažnije, zabavna.",
                        Publisher = "Profil",
                        LocalLibrary = localLibrary2,
                        Nationality = nationality2,
                        Language = language2,
                        Categories = new List<Category>() { category7 },
                        ImageUrl = "http://mozaik-knjiga.hr/wp-content/uploads/2017/10/Superhrana-za-svaki-dan-J.Oliver-234x300.jpg",
                        IsBorrowed = false,
                        IsReserved = false,
                        Keyword = "kuhanje",
                        BarcodeNumber = "12345678901"
                    }; // Super hrana by Jamie Oliver

                    var book11 = new Book()
                    {
                        Name = "Velika hrvatska kuharica",
                        NumOfPages = "75",
                        Author = author5,
                        YearOfIssue = year3,
                        ISBN = "0230217867",
                        DamageLevel = 2,
                        Description = "Velika hrvatska kuharica donosi vam mnoštvo tradicionalnih hrvatskih recepata za predjela, glavna jela, priloge i salate te deserte. Ovo je kuharica za sve koji uživaju u ukusnoj domaćoj hrani spravljenoj na način naših starih – jednostavno i od namirnica koje se mogu naći u najbližoj trgovini ili na tržnici.",
                        Publisher = "Profil",
                        LocalLibrary = localLibrary2,
                        Nationality = nationality1,
                        Language = language1,
                        Categories = new List<Category>() { category7 },
                        ImageUrl = "https://i.imgur.com/xAwESKP.jpg",
                        IsBorrowed = false,
                        IsReserved = false,
                        Keyword = "kuhanje"
                    }; // Velika hrvatska kuharica

                    var book12 = new Book()
                    {
                        Name = "Domaća zimnica",
                        NumOfPages = "82",
                        Author = author6,
                        YearOfIssue = year3,
                        ISBN = "02302456788",
                        DamageLevel = 2,
                        Description = "Od proljeća do jeseni, u vrtu i na tržnici, nalazi se obilje svježega voća i povrća po povoljnim cijenama. Ništa ne prija bolje od poslastica koje od njih sami napravite, kao što su, primjerice, ukusan pekmez od malina, ulja aromatizirana začinskim biljem ili sočan kiseli kupus bez ikakvih dodataka.",
                        Publisher = "Profil",
                        LocalLibrary = localLibrary2,
                        Nationality = nationality2,
                        Language = language1,
                        Categories = new List<Category>() { category7 },
                        ImageUrl = "http://mozaik-knjiga.hr/wp-content/uploads/2016/04/domaca-zimnica-235x300.jpg",
                        IsBorrowed = false,
                        IsReserved = false,
                        Keyword = "kuhanje"
                    }; // Domaca zimnica

                    #region Array of cooking books by the same author
                    var book13 = new Book()
                    {
                        Name = "Juhe i složenci",
                        NumOfPages = "85",
                        Author = author7,
                        YearOfIssue = year3,
                        ISBN = "02302456788",
                        DamageLevel = 2,
                        Description = "Za predjelo ili kao lagani ručak juhe i složenci su kao stvoreni. A knjiga Juhe i složenci nudi Vam velik izbor recepata, od omiljenih obiteljskih jela kao što su pileća juha ili minestrone do egzotičnih, orijentalnih jela, kao što je japanska miso juha.",
                        Publisher = "Mozaik",
                        LocalLibrary = localLibrary2,
                        Nationality = nationality1,
                        Language = language1,
                        Categories = new List<Category>() { category7 },
                        ImageUrl = "http://mozaik-knjiga.hr/wp-content/uploads/2017/07/Juhe-i-slozenci_CRO_RD-226x300.jpg",
                        IsBorrowed = false,
                        IsReserved = false,
                        Keyword = "kuhanje"
                    };

                    var book14 = new Book()
                    {
                        Name = "Predjela i grickalice",
                        NumOfPages = "85",
                        Author = author7,
                        YearOfIssue = year3,
                        ISBN = "02302456788",
                        DamageLevel = 2,
                        Description = "U ovoj ćete knjižici naći recepte za mnoga ukusna i maštovita jela. Neka su tipična, poput pizze pečene u tavi, salate caprese, ili kolača od krumpira, neka su domišljate inačice klasičnih tema, poput rižinih popečaka s dimljenim sirom i korom od lješnjaka, a neka ukusne novine, poput kruha s rajčicama sušenim na suncu i origanom. Jedna su vrlo lagana, poput salate od pira sa škampima, a druga zasitnija, primjerice nabujak s gorgonzolom.",
                        Publisher = "Mozaik",
                        LocalLibrary = localLibrary2,
                        Nationality = nationality1,
                        Language = language1,
                        Categories = new List<Category>() { category7 },
                        ImageUrl = "http://mozaik-knjiga.hr/wp-content/uploads/archive/images/Barilla_-_predjela_i_grickalice-231x300.jpg",
                        IsBorrowed = false,
                        IsReserved = false,
                        Keyword = "kuhanje"
                    };

                    var book15 = new Book()
                    {
                        Name = "Riba",
                        NumOfPages = "85",
                        Author = author7,
                        YearOfIssue = year3,
                        ISBN = "02302456788",
                        DamageLevel = 2,
                        Description = "U ovoj ćete knjižici naći recepte za najpoznatije talijanske recepte ali i mnoge ukusne i maštovite inačice jela od kojih će vam rasti zazubice. Ta su jela vrlo lagana, ali istinski posebnoga okusa.Primjerice, božanstveni brancin u acqua pazza ili ukusne dagnje na buzaru.",
                        Publisher = "Mozaik",
                        LocalLibrary = localLibrary2,
                        Nationality = nationality1,
                        Language = language1,
                        Categories = new List<Category>() { category7 },
                        ImageUrl = "http://mozaik-knjiga.hr/wp-content/uploads/archive/images/Barilla_-_riba-230x300.jpg",
                        IsBorrowed = false,
                        IsReserved = false,
                        Keyword = "kuhanje"
                    };


                    var book16 = new Book()
                    {
                        Name = "Meso",
                        NumOfPages = "60",
                        Author = author7,
                        YearOfIssue = year3,
                        ISBN = "02302456788",
                        DamageLevel = 2,
                        Description = "U ovoj ćete knjižici naći recepte za najpoznatije talijanske recepte, maštovite inačice klasičnih jela, kao i za ukusna nova jela od kojih će vam rasti zazubice. Mnoga njih vrlo su jednostavna i mogu se pripremiti za manje od sata, poput janjećih rebara na roštilju.",
                        Publisher = "Mozaik",
                        LocalLibrary = localLibrary2,
                        Nationality = nationality1,
                        Language = language1,
                        Categories = new List<Category>() { category7 },
                        ImageUrl = "http://mozaik-knjiga.hr/wp-content/uploads/archive/images/Barilla_-_meso-233x300.jpg",
                        IsBorrowed = false,
                        IsReserved = false,
                        Keyword = "kuhanje"
                    };

                    var book17 = new Book()
                    {
                        Name = "Deserti",
                        NumOfPages = "60",
                        Author = author7,
                        YearOfIssue = year3,
                        ISBN = "02302456788",
                        DamageLevel = 2,
                        Description = "Gotovo svi deserti u ovoj knjizi najbolje su od svake regije, deserti koji su s godinama prerasli u poslastice cijeloga talijanskog naroda – poput cannola – remek-djela sicilijanskoga umijeća izrade slatkoga tijesta",
                        Publisher = "Mozaik",
                        LocalLibrary = localLibrary2,
                        Nationality = nationality1,
                        Language = language1,
                        Categories = new List<Category>() { category7 },
                        ImageUrl = "http://mozaik-knjiga.hr/wp-content/uploads/archive/images/Barilla_-_deserti-230x300.jpg",
                        IsBorrowed = false,
                        IsReserved = false,
                        Keyword = "kuhanje"
                    };
                    #endregion Array of cooking books by the same author

                    #region Books by Jamie Oliver
                    var book18 = new Book()
                    {
                        Name = "Jamie kod kuće",
                        NumOfPages = "60",
                        Author = author4,
                        YearOfIssue = year2,
                        ISBN = "02302456788",
                        DamageLevel = 2,
                        Description = "Neizmjerno sam uživao pišući ovu knjigu. U biti se radi o kuharici podijeljenoj na četiri godišnja doba, kako biste lakše stekli predodžbu o tome kad je što spremno za uporabu. Svako godišnje doba sadrži hrpu mini poglavlja koja se temelje na različitim sastojcima",
                        Publisher = "Mozaik",
                        LocalLibrary = localLibrary2,
                        Nationality = nationality1,
                        Language = language1,
                        Categories = new List<Category>() { category7 },
                        ImageUrl = "https://i.imgur.com/vcmjYnE.jpg",
                        IsBorrowed = false,
                        IsReserved = false,
                        Keyword = "kuhanje"
                    };

                    var book19 = new Book()
                    {
                        Name = "Super hrana",
                        NumOfPages = "115",
                        Author = author4,
                        YearOfIssue = year2,
                        ISBN = "0230217867",
                        DamageLevel = 2,
                        Description = "Odaberete li samo poneku ideju iz ove knjige, počet ćete drukčije razmišljati o hrani i njezinoj moći da izravno utječe na vas, i tjelesno i mentalno, što će vas nadahnuti da pozitivno promijenite ne samo način na koji jedete, nego i, nadam se, prehrambene navike osoba koje vas okružuju. Hranu treba cijeniti, dijeliti je i u njoj uživati, a zdrava i hranjiva jela trebaju biti šarolika, ukusna i, što je najvažnije, zabavna.",
                        Publisher = "Profil",
                        LocalLibrary = localLibrary1,
                        Nationality = nationality2,
                        Language = language2,
                        Categories = new List<Category>() { category7 },
                        ImageUrl = "http://mozaik-knjiga.hr/wp-content/uploads/2017/10/Superhrana-za-svaki-dan-J.Oliver-234x300.jpg",
                        IsBorrowed = false,
                        IsReserved = false,
                        Keyword = "kuhanje"
                    }; // Super hrana by Jamie Oliver, ll1

                    var book20 = new Book()
                    {
                        Name = "Super hrana",
                        NumOfPages = "115",
                        Author = author4,
                        YearOfIssue = year2,
                        ISBN = "0230217867",
                        DamageLevel = 2,
                        Description = "Odaberete li samo poneku ideju iz ove knjige, počet ćete drukčije razmišljati o hrani i njezinoj moći da izravno utječe na vas, i tjelesno i mentalno, što će vas nadahnuti da pozitivno promijenite ne samo način na koji jedete, nego i, nadam se, prehrambene navike osoba koje vas okružuju. Hranu treba cijeniti, dijeliti je i u njoj uživati, a zdrava i hranjiva jela trebaju biti šarolika, ukusna i, što je najvažnije, zabavna.",
                        Publisher = "Profil",
                        LocalLibrary = localLibrary3,
                        Nationality = nationality2,
                        Language = language2,
                        Categories = new List<Category>() { category7 },
                        ImageUrl = "http://mozaik-knjiga.hr/wp-content/uploads/2017/10/Superhrana-za-svaki-dan-J.Oliver-234x300.jpg",
                        IsBorrowed = false,
                        IsReserved = false,
                        Keyword = "kuhanje"
                    }; // Super hrana by Jamie Oliver

                    var book21 = new Book()
                    {
                        Name = "Super hrana",
                        NumOfPages = "115",
                        Author = author4,
                        YearOfIssue = year2,
                        ISBN = "0230217867",
                        DamageLevel = 2,
                        Description = "Odaberete li samo poneku ideju iz ove knjige, počet ćete drukčije razmišljati o hrani i njezinoj moći da izravno utječe na vas, i tjelesno i mentalno, što će vas nadahnuti da pozitivno promijenite ne samo način na koji jedete, nego i, nadam se, prehrambene navike osoba koje vas okružuju. Hranu treba cijeniti, dijeliti je i u njoj uživati, a zdrava i hranjiva jela trebaju biti šarolika, ukusna i, što je najvažnije, zabavna.",
                        Publisher = "Profil",
                        LocalLibrary = localLibrary4,
                        Nationality = nationality2,
                        Language = language2,
                        Categories = new List<Category>() { category7 },
                        ImageUrl = "http://mozaik-knjiga.hr/wp-content/uploads/2017/10/Superhrana-za-svaki-dan-J.Oliver-234x300.jpg",
                        IsBorrowed = false,
                        IsReserved = false,
                        Keyword = "kuhanje"
                    }; // Super hrana by Jamie Oliver
                    #endregion Books by Jamie Oliver
                    #endregion Cooking books

                    #region Programming books
                    var book22 = new Book()
                    {
                        Name = "C# 7.0 za programere",
                        NumOfPages = "960",
                        Author = author10,
                        YearOfIssue = year3,
                        ISBN = "0230217869",
                        DamageLevel = 2,
                        Description = "U ovoj knjizi naći ćete odgovor na svako pitanje koje se odnosi na C# 7.0 ili .NET CLR. Organizacija knjige zasniva se na konceptima i primjerima upotrebe, a peto izdanje sadrži ažurirane materijale o istovremenosti, višenitnom radu i paralelnom programiranju - uključujući i detaljno razmatranje asinkronih funkcija jezika C# 7.0.",
                        Publisher = "Profil",
                        LocalLibrary = localLibrary1,
                        Nationality = nationality2,
                        Language = language2,
                        Categories = new List<Category>() { category8 },
                        ImageUrl = "https://covers.oreillystatic.com/images/0636920083634/lrg.jpg",
                        IsBorrowed = false,
                        IsReserved = false,
                        Keyword = "c#"
                    };
                    var book23 = new Book()
                    {
                        Name = "C# osnove programiranja",
                        NumOfPages = "512",
                        Author = author8,
                        YearOfIssue = year2,
                        ISBN = "0230217869",
                        DamageLevel = 2,
                        Description = "Ova lako čitljiva knjiga omogućava da učite onako kako to vama najviše odgovara, pri čemu stječete veštine za građenje jedinstvenih i korisnih programa. Microsoft je potpuno preradio priručnik o programiranju za početnike, u koji su ugrađena sva saznanja o tome kako današnji početnici uče i zašto su neke druge knjige manje uspješne.",
                        Publisher = "Profil",
                        LocalLibrary = localLibrary2,
                        Nationality = nationality2,
                        Language = language2,
                        Categories = new List<Category>() { category8 },
                        ImageUrl = "http://www.mikroknjiga.rs/slike2/86-7991/978-86-7991-395-1_k1_1.jpg",
                        IsBorrowed = false,
                        IsReserved = false,
                        Keyword = "c#"
                    };
                    var book24 = new Book()
                    {
                        Name = "C# osnove programiranja",
                        NumOfPages = "512",
                        Author = author8,
                        YearOfIssue = year2,
                        ISBN = "0230217869",
                        DamageLevel = 2,
                        Description = "Ova lako čitljiva knjiga omogućava da učite onako kako to vama najviše odgovara, pri čemu stječete veštine za građenje jedinstvenih i korisnih programa. Microsoft je potpuno preradio priručnik o programiranju za početnike, u koji su ugrađena sva saznanja o tome kako današnji početnici uče i zašto su neke druge knjige manje uspješne.",
                        Publisher = "Profil",
                        LocalLibrary = localLibrary3,
                        Nationality = nationality2,
                        Language = language2,
                        Categories = new List<Category>() { category8 },
                        ImageUrl = "http://www.mikroknjiga.rs/slike2/86-7991/978-86-7991-395-1_k1_1.jpg",
                        IsBorrowed = false,
                        IsReserved = false,
                        Keyword = "c#"
                    };
                    var book25 = new Book()
                    {
                        Name = "C# osnove programiranja",
                        NumOfPages = "512",
                        Author = author8,
                        YearOfIssue = year2,
                        ISBN = "0230217869",
                        DamageLevel = 2,
                        Description = "Ova lako čitljiva knjiga omogućava da učite onako kako to vama najviše odgovara, pri čemu stječete veštine za građenje jedinstvenih i korisnih programa. Microsoft je potpuno preradio priručnik o programiranju za početnike, u koji su ugrađena sva saznanja o tome kako današnji početnici uče i zašto su neke druge knjige manje uspješne.",
                        Publisher = "Profil",
                        LocalLibrary = localLibrary4,
                        Nationality = nationality2,
                        Language = language2,
                        Categories = new List<Category>() { category8 },
                        ImageUrl = "http://www.mikroknjiga.rs/slike2/86-7991/978-86-7991-395-1_k1_1.jpg",
                        IsBorrowed = false,
                        IsReserved = false,
                        Keyword = "c#"
                    };
                    var book26 = new Book()
                    {
                        Name = "C# osnove programiranja",
                        NumOfPages = "512",
                        Author = author8,
                        YearOfIssue = year2,
                        ISBN = "0230217869",
                        DamageLevel = 2,
                        Description = "Ova lako čitljiva knjiga omogućava da učite onako kako to vama najviše odgovara, pri čemu stječete veštine za građenje jedinstvenih i korisnih programa. Microsoft je potpuno preradio priručnik o programiranju za početnike, u koji su ugrađena sva saznanja o tome kako današnji početnici uče i zašto su neke druge knjige manje uspješne.",
                        Publisher = "Profil",
                        LocalLibrary = localLibrary4,
                        Nationality = nationality2,
                        Language = language2,
                        Categories = new List<Category>() { category8 },
                        ImageUrl = "http://www.mikroknjiga.rs/slike2/86-7991/978-86-7991-395-1_k1_1.jpg",
                        IsBorrowed = false,
                        IsReserved = false,
                        Keyword = "c#"
                    };
                    var book27 = new Book()
                    {
                        Name = "C# programiranje za Windows i Android",
                        NumOfPages = "256",
                        Author = author9,
                        YearOfIssue = year2,
                        ISBN = "0230217869",
                        DamageLevel = 2,
                        Description = "Ova knjiga je najprodavanija u okviru softverskih knjiga Visual Studio C#, a namijenjena je inženjerima i entuzijastima koji se žele upoznati sa jezikom C# i razvojnim okruženjem.",
                        Publisher = "Mozaik",
                        LocalLibrary = localLibrary4,
                        Nationality = nationality2,
                        Language = language2,
                        Categories = new List<Category>() { category8 },
                        ImageUrl = "https://www.korisnaknjiga.com/photo/books0083/p082458c0.jpg",
                        IsBorrowed = false,
                        IsReserved = false,
                        Keyword = "c#"
                    };
                    var book28 = new Book()
                    {
                        Name = "C# 7.0 za programere",
                        NumOfPages = "256",
                        Author = author10,
                        YearOfIssue = year3,
                        ISBN = "0230217869",
                        DamageLevel = 2,
                        Description = "U ovoj knjizi naći ćete odgovor na svako pitanje koje se odnosi na C# 7.0 ili .NET CLR. Organizacija knjige zasniva se na konceptima i primjerima upotrebe, a peto izdanje sadrži ažurirane materijale o istovremenosti, višenitnom radu i paralelnom programiranju - uključujući i detaljno razmatranje asinkronih funkcija jezika C# 7.0.",
                        Publisher = "Mozaik",
                        LocalLibrary = localLibrary2,
                        Nationality = nationality2,
                        Language = language2,
                        Categories = new List<Category>() { category8 },
                        ImageUrl = "https://www.korisnaknjiga.com/photo/books0083/p082458c0.jpg",
                        IsBorrowed = false,
                        IsReserved = false,
                        Keyword = "c#"
                    };
                    var book29 = new Book()
                    {
                        Name = "C# 7.0 za programere",
                        NumOfPages = "960",
                        Author = author10,
                        YearOfIssue = year3,
                        ISBN = "0230217869",
                        DamageLevel = 2,
                        Description = "U ovoj knjizi naći ćete odgovor na svako pitanje koje se odnosi na C# 7.0 ili .NET CLR. Organizacija knjige zasniva se na konceptima i primjerima upotrebe, a peto izdanje sadrži ažurirane materijale o istovremenosti, višenitnom radu i paralelnom programiranju - uključujući i detaljno razmatranje asinkronih funkcija jezika C# 7.0.",
                        Publisher = "Profil",
                        LocalLibrary = localLibrary3,
                        Nationality = nationality2,
                        Language = language2,
                        Categories = new List<Category>() { category8 },
                        ImageUrl = "https://covers.oreillystatic.com/images/0636920083634/lrg.jpg",
                        IsBorrowed = false,
                        IsReserved = false,
                        Keyword = "c#"
                    };
                    var book30 = new Book()
                    {
                        Name = "C# 7.0 za programere",
                        NumOfPages = "960",
                        Author = author10,
                        YearOfIssue = year3,
                        ISBN = "0230217869",
                        DamageLevel = 2,
                        Description = "U ovoj knjizi naći ćete odgovor na svako pitanje koje se odnosi na C# 7.0 ili .NET CLR. Organizacija knjige zasniva se na konceptima i primjerima upotrebe, a peto izdanje sadrži ažurirane materijale o istovremenosti, višenitnom radu i paralelnom programiranju - uključujući i detaljno razmatranje asinkronih funkcija jezika C# 7.0.",
                        Publisher = "Profil",
                        LocalLibrary = localLibrary4,
                        Nationality = nationality2,
                        Language = language2,
                        Categories = new List<Category>() { category8 },
                        ImageUrl = "https://covers.oreillystatic.com/images/0636920083634/lrg.jpg",
                        IsBorrowed = false,
                        IsReserved = false,
                        Keyword = "c#"
                    };
                    var book31 = new Book()
                    {
                        Name = "Programski jezik C#: pitanja i odgovori",
                        NumOfPages = "960",
                        Author = author11,
                        YearOfIssue = year2,
                        ISBN = "0230217869",
                        DamageLevel = 2,
                        Description = "Knjiga sadrži rješenja brojnih problema s kojima se svakodnevno susrećemo i dobar je oslonac za početak programerske karijere ili utvrđivanje zaboravljenih znanja. Podeljena je na tri dijela.",
                        Publisher = "Profil",
                        LocalLibrary = localLibrary4,
                        Nationality = nationality1,
                        Language = language2,
                        Categories = new List<Category>() { category8 },
                        ImageUrl = "http://www.kombib.rs/slike/knjige/programski_jezik_c_sharp-pitanja-odgovori.jpg",
                        IsBorrowed = false,
                        IsReserved = false,
                        Keyword = "c#"
                    };
                    var book32 = new Book()
                    {
                        Name = "C# 6 i .NET Core 1.0",
                        NumOfPages = "720",
                        Author = author12,
                        YearOfIssue = year3,
                        ISBN = "0230217810",
                        DamageLevel = 2,
                        Description = "Kreirajte moćne aplikacije za različite platforme koristeći C# 6, .NET Core 1.0, ASP.NET Core 1.0 i Visual Studio 2015.",
                        Publisher = "Algoritam",
                        LocalLibrary = localLibrary4,
                        Nationality = nationality1,
                        Language = language2,
                        Categories = new List<Category>() { category8 },
                        ImageUrl = "http://www.mikroknjiga.rs/slike2/86-7310/978-86-7310-507-9_k1_1.jpg",
                        IsBorrowed = false,
                        IsReserved = false,
                        Keyword = "c#"
                    };
                    #endregion Programming books

                    #region Design books
                    var book33 = new Book()
                    {
                        Name = "Rješenja za grafički dizajn",
                        NumOfPages = "250",
                        Author = author13,
                        YearOfIssue = year3,
                        ISBN = "0230217869",
                        DamageLevel = 2,
                        Description = "U jednoj od najpopularnijih knjiga za grafički dizajn, Robin Landa predstavlja znanje i trikove koje je sakupio za vrijeme svoje karijere kao dizajner.",
                        Publisher = "Profil",
                        LocalLibrary = localLibrary1,
                        Nationality = nationality2,
                        Language = language1,
                        Categories = new List<Category>() { category9, category2 },
                        ImageUrl = "https://images.gr-assets.com/books/1347790482l/7758047.jpg",
                        IsBorrowed = false,
                        IsReserved = false,
                        Keyword = "dizajn"
                    };

                    var book34 = new Book()
                    {
                        Name = "Dizajn danas!",
                        NumOfPages = "250",
                        Author = author14,
                        YearOfIssue = year2,
                        ISBN = "0230217869",
                        DamageLevel = 2,
                        Description = "Knjiga \"Dizajn danas!\" daje iscrpan pregled suvremene dizajnerske prakse, a istodobno poziva na stvaranje primjenjivijeg koncepta svih vrsta proizvodnog dizajna. Osim što očarava izgledom i pruža obilje informacija, knjiga \"Dizajn danas!\" predstavlja i najnovije radove 90 vodećih svjetskih dizajnera",
                        Publisher = "Algoritam",
                        LocalLibrary = localLibrary1,
                        Nationality = nationality2,
                        Language = language1,
                        Categories = new List<Category>() { category9 },
                        ImageUrl = "http://verbum.hr/images/artikli/velike/5518.jpg",
                        IsBorrowed = false,
                        IsReserved = false,
                        Keyword = "dizajn"
                    };


                    var book35 = new Book()
                    {
                        Name = "Dizajn danas!",
                        NumOfPages = "250",
                        Author = author14,
                        YearOfIssue = year2,
                        ISBN = "0230217869",
                        DamageLevel = 2,
                        Description = "Knjiga \"Dizajn danas!\" daje iscrpan pregled suvremene dizajnerske prakse, a istodobno poziva na stvaranje primjenjivijeg koncepta svih vrsta proizvodnog dizajna. Osim što očarava izgledom i pruža obilje informacija, knjiga \"Dizajn danas!\" predstavlja i najnovije radove 90 vodećih svjetskih dizajnera",
                        Publisher = "Algoritam",
                        LocalLibrary = localLibrary2,
                        Nationality = nationality2,
                        Language = language1,
                        Categories = new List<Category>() { category9 },
                        ImageUrl = "http://verbum.hr/images/artikli/velike/5518.jpg",
                        IsBorrowed = false,
                        IsReserved = false,
                        Keyword = "dizajn"
                    };

                    var book36 = new Book()
                    {
                        Name = "Dizajn danas!",
                        NumOfPages = "250",
                        Author = author14,
                        YearOfIssue = year2,
                        ISBN = "0230217869",
                        DamageLevel = 2,
                        Description = "Knjiga \"Dizajn danas!\" daje iscrpan pregled suvremene dizajnerske prakse, a istodobno poziva na stvaranje primjenjivijeg koncepta svih vrsta proizvodnog dizajna. Osim što očarava izgledom i pruža obilje informacija, knjiga \"Dizajn danas!\" predstavlja i najnovije radove 90 vodećih svjetskih dizajnera",
                        Publisher = "Algoritam",
                        LocalLibrary = localLibrary3,
                        Nationality = nationality2,
                        Language = language1,
                        Categories = new List<Category>() { category9 },
                        ImageUrl = "http://verbum.hr/images/artikli/velike/5518.jpg",
                        IsBorrowed = false,
                        IsReserved = false,
                        Keyword = "dizajn"
                    };

                    var book37 = new Book()
                    {
                        Name = "Dizajn danas!",
                        NumOfPages = "250",
                        Author = author14,
                        YearOfIssue = year2,
                        ISBN = "0230217869",
                        DamageLevel = 2,
                        Description = "Knjiga \"Dizajn danas!\" daje iscrpan pregled suvremene dizajnerske prakse, a istodobno poziva na stvaranje primjenjivijeg koncepta svih vrsta proizvodnog dizajna. Osim što očarava izgledom i pruža obilje informacija, knjiga \"Dizajn danas!\" predstavlja i najnovije radove 90 vodećih svjetskih dizajnera",
                        Publisher = "Algoritam",
                        LocalLibrary = localLibrary4,
                        Nationality = nationality2,
                        Language = language1,
                        Categories = new List<Category>() { category9 },
                        ImageUrl = "http://verbum.hr/images/artikli/velike/5518.jpg",
                        IsBorrowed = false,
                        IsReserved = false,
                        Keyword = "dizajn"
                    };

                    var book38 = new Book()
                    {
                        Name = "Teorija i povijest dizajna",
                        NumOfPages = "250",
                        Author = author15,
                        YearOfIssue = year2,
                        ISBN = "0230217869",
                        DamageLevel = 2,
                        Description = "Raspon tekstova u ovoj kritičkoj antologiji nije samo vremenski nego i tematski raširen, od osamnaestog stoljeća i fascinacije uporabnim u umjetnosti sve do suvremenog interdisciplinarnog propitivanja materijalnosti objekta.",
                        Publisher = "Algoritam",
                        LocalLibrary = localLibrary1,
                        Nationality = nationality1,
                        Language = language1,
                        Categories = new List<Category>() { category9 },
                        ImageUrl = "http://www.kgz.hr/UserDocsImages//Novi%20naslovi/2012/teorija_povijest_dizajna.jpg",
                        IsBorrowed = false,
                        IsReserved = false,
                        Keyword = "dizajn"
                    };

                    var book39 = new Book()
                    {
                        Name = "Dizajn za početnike",
                        NumOfPages = "250",
                        Author = author15,
                        YearOfIssue = year3,
                        ISBN = "0230217869",
                        DamageLevel = 2,
                        Description = "Feđa Vukić u ovom djelu iznosi sve što trebate znati o dizajnu kako biste započeli sa svojom karijerom.",
                        Publisher = "Mozaik",
                        LocalLibrary = localLibrary1,
                        Nationality = nationality1,
                        Language = language2,
                        Categories = new List<Category>() { category9 },
                        ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/51GJpUx4uoL._SX367_BO1,204,203,200_.jpg",
                        IsBorrowed = false,
                        IsReserved = false,
                        Keyword = "dizajn"
                    };

                    var book40 = new Book()
                    {
                        Name = "Elementi tipografskog stila",
                        NumOfPages = "250",
                        Author = author16,
                        YearOfIssue = year3,
                        ISBN = "0230217869",
                        DamageLevel = 2,
                        Description = "Autor ove knjige jedan je od najboljih tipografa na području cijele Kanade, a i šire.",
                        Publisher = "Algoritam",
                        LocalLibrary = localLibrary1,
                        Nationality = nationality1,
                        Language = language2,
                        Categories = new List<Category>() { category9 },
                        ImageUrl = "https://i.imgur.com/yN2PLV1.jpg",
                        IsBorrowed = false,
                        IsReserved = false,
                        Keyword = "dizajn"
                    };


                    var book41 = new Book()
                    {
                        Name = "Temelji kreativnog dizajna",
                        NumOfPages = "250",
                        Author = author17,
                        YearOfIssue = year3,
                        ISBN = "0230217869",
                        DamageLevel = 2,
                        Description = "U ovoj knjizi Gary Ambrose upoznaje čitatelje s temeljima današnjeg kreativnog dizajna, poput formata, tipografije, printa...",
                        Publisher = "Algoritam",
                        LocalLibrary = localLibrary1,
                        Nationality = nationality1,
                        Language = language2,
                        Categories = new List<Category>() { category9 },
                        ImageUrl = "https://images.gr-assets.com/books/1312019743l/6330726.jpg",
                        IsBorrowed = false,
                        IsReserved = false,
                        Keyword = "dizajn"
                    };

                    var book42 = new Book()
                    {
                        Name = "Dizajniranje brand identiteta",
                        NumOfPages = "250",
                        Author = author18,
                        YearOfIssue = year3,
                        ISBN = "0230217869",
                        DamageLevel = 2,
                        Description = "U dizajn identita za proizvod i tvrtku ulazi mnogo rada na dizajnu, no i puno pripreme. Alina Wheeler upravo Vas vodi kroz pripremu za jedan od najvećih izazova svakog dizajnera.",
                        Publisher = "Profil",
                        LocalLibrary = localLibrary1,
                        Nationality = nationality2,
                        Language = language2,
                        Categories = new List<Category>() { category9 },
                        ImageUrl = "https://media.licdn.com/mpr/mpr/AAEAAQAAAAAAAAuaAAAAJGU0NmJlM2RjLWUzMmYtNDkwZS1iZmRiLTg5YTc4ZDEyMjIwZg.jpg",
                        IsBorrowed = false,
                        IsReserved = false,
                        Keyword = "dizajn"
                    };

                    var book43 = new Book()
                    {
                        Name = "Industrijalni dizajn od A do Ž",
                        NumOfPages = "225",
                        Author = author14,
                        YearOfIssue = year2,
                        ISBN = "0230217869",
                        DamageLevel = 2,
                        Description = "Ako Vas imalo zanima dizajn Vaše četkice za zube, perilice za rublje ili telefona, knjiga Industrijalni dizajn bit će Vam neizmjerno zanimljiva.",
                        Publisher = "Mozaik",
                        LocalLibrary = localLibrary1,
                        Nationality = nationality2,
                        Language = language1,
                        Categories = new List<Category>() { category9 },
                        ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/514SxRAt20L._SX374_BO1,204,203,200_.jpg",
                        IsBorrowed = false,
                        IsReserved = true,
                        Keyword = "dizajn"
                    };
                    #endregion

                    #region Library member seed
                    var localLibraryMember1 = new LocalLibraryMember()
                    {
                        Name = "Josip",
                        LastName = "Svalina",
                        Email = "josip@dump.hr",
                        PhoneNumber = "095 4224 247",
                        Oib = "95874125896",
                        Address = "Kralja Tomislava 54",
                        LocalLibrary = localLibrary2,
                        OnlineUser = regularUser,
                        BarcodeNumber = "12345678902",
                        MembershipExpiryDate = new DateTime(2020, 5, 1)
                    };

                    var localLibraryMember2 = new LocalLibraryMember()
                    {
                        Name = "Mate",
                        LastName = "Matic",
                        Email = "josip@dump.hr",
                        PhoneNumber = "095 4224 247",
                        Oib = "95874125896",
                        Address = "Kralja Tomislava 54",
                        LocalLibrary = localLibrary3,
                        OnlineUser = regularUser,
                        BarcodeNumber = "12345678901",
                        MembershipExpiryDate = new DateTime(2025, 5, 1)
                    };

                    var localLibraryMember4 = new LocalLibraryMember()
                    {
                        Name = "Ante",
                        LastName = "Antic",
                        Email = "josip@dump.hr",
                        PhoneNumber = "095 4224 247",
                        Oib = "95874125896",
                        Address = "Kralja Tomislava 54",
                        LocalLibrary = localLibrary3,
                        //OnlineUser = regularUser,
                        BarcodeNumber = "12345678901",
                        MembershipExpiryDate = new DateTime(2025, 5, 1)
                    };

                    var localLibraryMember5 = new LocalLibraryMember()
                    {
                        Name = "Stipe",
                        LastName = "Lelas",
                        Email = "josip@dump.hr",
                        PhoneNumber = "095 4224 247",
                        Oib = "95874125896",
                        Address = "Kralja Tomislava 54",
                        LocalLibrary = localLibrary3,
                        //OnlineUser = regularUser,
                        BarcodeNumber = "12345678901",
                        MembershipExpiryDate = new DateTime(2025, 5, 1)
                    };

                    var localLibraryMember6 = new LocalLibraryMember()
                    {
                        Name = "Zvonimir",
                        LastName = "Delas",
                        Email = "josip@dump.hr",
                        PhoneNumber = "095 4224 247",
                        Oib = "95874125896",
                        Address = "Kralja Tomislava 54",
                        LocalLibrary = localLibrary3,
                        //OnlineUser = regularUser,
                        BarcodeNumber = "12345678901",
                        MembershipExpiryDate = new DateTime(2025, 5, 1)
                    };

                    var localLibraryMember3 = new LocalLibraryMember()
                    {
                        LocalLibrary = localLibrary2,
                        OnlineUser = null,
                        BarcodeNumber = "12345678903",
                        MembershipExpiryDate = new DateTime(2025, 5, 1)
                    };
                    #endregion

                    #region Borrowed and reserved seed
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
                        ReturnDeadline = new DateTime(2018, 3, 30),
                        Books = new List<Book> { book4, book19 },
                    };

                    var batchOfBorrowedBooks4 = new BatchOfBorrowedBooks()
                    {
                        LibraryMember = localLibraryMember2,
                        PickupDate = new DateTime(2018, 3, 2),
                        ReturnDeadline = new DateTime(2018, 3, 30),
                        Books = new List<Book> { book4, book19, book47 },
                    };

                    var reservation1 = new Reservation()
                    {
                        LibraryMember = localLibraryMember2,
                        PickupDeadline = new DateTime(2016, 1, 2),
                        Book = book2
                    };

                    var reservation2 = new Reservation()
                    {
                        LibraryMember = localLibraryMember2,
                        PickupDeadline = new DateTime(2018, 1, 2),
                        Book = book7
                    };
                    #endregion

                    #region Reserved for stats seed
                    var listOfAllReservedBooks = new List<ReservedBooksAll>()
                    {
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 1, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 1, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 1, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 1, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 1, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 1, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 1, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 1, 25)
                        },
                                                new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 1, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 1, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 1, 25)
                        },
                                                new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 1, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 1, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 1, 25)
                        },
                                                new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 1, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 1, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 1, 25)
                        },
                                                new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 1, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 1, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 1, 25)
                        },

                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 3, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 3, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 3, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 3, 25)
                        },
                                                new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 3, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 3, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 3, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 3, 25)
                        },
                                                new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 3, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 3, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 3, 25)
                        },
                                                new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 3, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 3, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 3, 25)
                        },
                                                new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 3, 25)
                        },

                                                new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 3, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 3, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 3, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 3, 25)
                        },
                                                new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 3, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 3, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 3, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 3, 25)
                        },
                                                new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 3, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 3, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 3, 25)
                        },
                                                new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 3, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 3, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 3, 25)
                        },
                                                new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 3, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 3, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 3, 25)
                        },
                                                new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 3, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 3, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 3, 25)
                        },

                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 3, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 3, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 3, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 3, 25)
                        },
                                                new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 3, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 3, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 3, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 3, 25)
                        },
                                                new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 3, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 3, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 3, 25)
                        },
                                                new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 3, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 3, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 3, 25)
                        },


                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                                                new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                                                new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                                                new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                                                new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },

                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                                                new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                                                new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                                                new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                                                new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },

                                                new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                                                new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                                                new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                                                new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                                                new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                                                new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },

                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                                                new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                                                new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                                                new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                        new ReservedBooksAll()
                        {
                            LocalLibrary = localLibrary1,
                            TimeStamp = new DateTime(2017, 4, 25)
                        },
                    };
                    #endregion

                    var random = new Random();

                    var listOfAllBorrowedBooks = new List<BorrowedBooksAll>();

                    for (int i = 0; i < 12; i++)
                    {
                        var numberOfBooksInMonth = random.Next(55, 250);

                        for (int j = 0; j < numberOfBooksInMonth; j++)
                        {
                            listOfAllReservedBooks.Add(
                                new ReservedBooksAll()
                                {
                                    LocalLibrary = localLibrary1,
                                    TimeStamp = new DateTime(2017, 12, 31).AddMonths(-i)
                                });

                            listOfAllBorrowedBooks.Add(
                                new BorrowedBooksAll()
                                {
                                    LocalLibrary = localLibrary1,
                                    TimeStamp = new DateTime(2017, 12, 31).AddMonths(-i)
                                });
                        }
                    }

//                    var listOfAllBorrowedBooks = new List<BorrowedBooksAll>()
//                    {
//                        new BorrowedBooksAll()
//                        {
//                            LocalLibrary = localLibrary1,
//                            TimeStamp = new DateTime(2018,1,29)
//                        },
//new BorrowedBooksAll()
//                        {
//                            LocalLibrary = localLibrary1,
//                            TimeStamp = DateTime.Now
//                        }
//                    };

                    for (int i = 0; i < 7; i++)
                    {

                        var numberOfBooksOnDay = random.Next(7, 25);

                        for (int j = 0; j < numberOfBooksOnDay; j++)
                        {
                            listOfAllBorrowedBooks.Add(
                                new BorrowedBooksAll()
                                {
                                    LocalLibrary = localLibrary1,
                                    TimeStamp = DateTime.Now.AddDays(-i).AddYears(-1),
                                });

                            listOfAllBorrowedBooks.Add(
                                new BorrowedBooksAll()
                                {
                                    LocalLibrary = localLibrary1,
                                    TimeStamp = DateTime.Now.AddDays(-i),
                                });
                        }
                    }

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
                    context.Books.Add(book10);
                    context.Books.Add(book11);
                    context.Books.Add(book12);
                    context.Books.Add(book13);
                    context.Books.Add(book14);
                    context.Books.Add(book15);
                    context.Books.Add(book16);
                    context.Books.Add(book17);
                    context.Books.Add(book18);
                    context.Books.Add(book19);
                    context.Books.Add(book20);
                    context.Books.Add(book21);
                    context.Books.Add(book22);
                    context.Books.Add(book23);
                    context.Books.Add(book24);
                    context.Books.Add(book25);
                    context.Books.Add(book26);
                    context.Books.Add(book27);
                    context.Books.Add(book28);
                    context.Books.Add(book29);
                    context.Books.Add(book30);
                    context.Books.Add(book31);
                    context.Books.Add(book32);
                    context.Books.Add(book33);
                    context.Books.Add(book34);
                    context.Books.Add(book35);
                    context.Books.Add(book36);
                    context.Books.Add(book37);
                    context.Books.Add(book38);
                    context.Books.Add(book39);
                    context.Books.Add(book40);
                    context.Books.Add(book41);
                    context.Books.Add(book42);
                    context.Books.Add(book43);
                    context.Books.Add(book44);
                    context.Books.Add(book45);
                    context.Books.Add(book46);
                    context.Books.Add(book47);

                    context.BatchesOfBorrowedBooks.Add(batchOfBorrowedBooks1);
                    context.BatchesOfBorrowedBooks.Add(batchOfBorrowedBooks2);
                    context.BatchesOfBorrowedBooks.Add(batchOfBorrowedBooks3);
                    context.BatchesOfBorrowedBooks.Add(batchOfBorrowedBooks4);

                    context.Reservations.Add(reservation1);
                    //context.Reservations.Add(reservation2);

                    context.LocalLibraryMembers.Add(localLibraryMember1);
                    context.LocalLibraryMembers.Add(localLibraryMember2);
                    context.LocalLibraryMembers.Add(localLibraryMember3);
                    context.LocalLibraryMembers.Add(localLibraryMember4);
                    context.LocalLibraryMembers.Add(localLibraryMember5);
                    context.LocalLibraryMembers.Add(localLibraryMember6);

                    context.BorrowedBooksAll.AddRange(listOfAllBorrowedBooks);
                    context.ReservedBooksAll.AddRange(listOfAllReservedBooks);

                    context.SaveChanges();
                }

            }
        }
    }
}