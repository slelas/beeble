using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Web;
using FluentEmail;

namespace Beeble.Domain.MailService
{
    public class MailService
    {
        public static void SendBookReturnReminder(string emailAddress, string name, string libraryName, string bookName, DateTime? expiryDate)
        {
            var smtpClient = new SmtpClient();

            var email = new Email(
                client: smtpClient,
                emailAddress: "beeble.no-reply@outlook.com",
                name: "Obavijest o isticanju roka za vraćanje knjige"
            );

            var template = @"<p style='color: #444444; font-family: sans-serif;'>
                        Poštovani @Model.Name, <br/> <br/>
                        obavještavamo Vas da za @Model.RemainingDays dan (@Model.Date) istječe rok za vraćanje knjige @Model.BookName u @Model.LibraryName. Svaki dan kašnjenja se naplaćuje prema dogovorenoj tarifi.
                        <br/>
                        <br/>
                        Ovo je automatska poruka. Nemoj odgovarati na ovu e-poruku jer je adresa namijenjena samo slanju poruka.
                        <br/>
                        <br/>
                        Srdačan pozdrav,<br/>
                        Beeble tim
                        </p>";

            var model = new
            {
                Date = expiryDate.Value.ToShortDateString(),
                Name = name,
                LibraryName = libraryName,
                BookName = bookName ?? "(Pogledajte profil)",
                RemainingDays = (int)((expiryDate - DateTime.Now).Value.TotalDays)
            };

            email.To(emailAddress)
                .Subject("Obavijest o isticanju roka za vraćanje knjige")
                .UsingTemplate(template, model);

            email.Send();
        }

        public static void SendBookReservationReminder(string emailAddress, string name, string libraryName, string bookName, DateTime? expiryDate)
        {
            var smtpClient = new SmtpClient();

            var email = new Email(
                client: smtpClient,
                emailAddress: "beeble.no-reply@outlook.com",
                name: "Obavijest o isticanju rezervacije knjige"
            );

            var template = @"<p style='color: #444444; font-family: sans-serif;'>
                        Poštovani @Model.Name, <br/> <br/>
                        obavještavamo Vas da za @Model.RemainingHours sati (@Model.Date) istječe rezervacija za knjigu @Model.BookName u @Model.LibraryName.
                        <br/>
                        <br/>
                        Ovo je automatska poruka. Nemoj odgovarati na ovu e-poruku jer je adresa namijenjena samo slanju poruka.
                        <br/>
                        <br/>
                        Srdačan pozdrav,<br/>
                        Beeble tim
                        </p>";

            var model = new
            {
                Date = expiryDate.Value,
                Name = name,
                LibraryName = libraryName,
                BookName = bookName ?? "(Pogledajte profil)",
                RemainingHours = (int)((expiryDate - DateTime.Now).Value.TotalHours)
            };

            email.To(emailAddress)
                .Subject("Obavijest o isticanju rezervacije knjige")
                .UsingTemplate(template, model);

            email.Send();
        }


        public static void SendMembershipExpiryReminder(string emailAddress, string name, string libraryName, DateTime? expiryDate)
        {
            var smtpClient = new SmtpClient();

            var email = new Email(
                client: smtpClient,
                emailAddress: "beeble.no-reply@outlook.com",
                name: "Obavijest o isticanju članstva"
            );

            var template = @"<p style='color: #444444; font-family: sans-serif;'>
                        Poštovani @Model.Name, <br/> <br/>
                        obavještavamo Vas da za @Model.RemainingDays dana (@Model.Date) istječe vaše članstvo u @Model.LibraryName.
                        <br/>
                        <br/>
                        Ovo je automatska poruka. Nemoj odgovarati na ovu e-poruku jer je adresa namijenjena samo slanju poruka.
                        <br/>
                        <br/>
                        Srdačan pozdrav,<br/>
                        Beeble tim
                        </p>";

            var model = new
            {
                Date = expiryDate.Value.ToShortDateString(),
                Name = name,
                LibraryName = libraryName,
                RemainingDays = (int)((expiryDate - DateTime.Now).Value.TotalDays)
            };

            email.To(emailAddress)
                .Subject("Obavijest o isticanju članstva")
                .UsingTemplate(template, model);
            
            email.Send();


        }
    }
}