using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Beeble.Domain.DTOs;
using Beeble.Domain.Repositories;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Beeble.Api.Controllers
{
    [RoutePrefix("api/export")]
    public class ExcelExportController : ApiController
    {
        [HttpGet]
        [Route("books")]
        public HttpResponseMessage ExportToExcel(int libraryId)
        {
            var repo = new BooksRepository();
            var books = repo.BookExportObject(libraryId);

            var booksTable = new DataTable();

            booksTable.Columns.Add("Naslov");
            booksTable.Columns.Add("Autor");
            booksTable.Columns.Add("Broj posuđivanja");
            booksTable.Columns.Add("Broj rezervacija");

            foreach (var book in books)
            {
                booksTable.Rows.Add(
                    book.Name,
                    book.Author,
                    book.BorrowCount,
                    book.ReserveCount
                    );
            }


            using (var excel = new ExcelPackage())
            {
                var worksheet = excel.Workbook.Worksheets.Add("Knjige");

                using (var rng = worksheet.Cells["A1:D1"])
                {
                    rng.Style.Font.Bold = true;
                    rng.Style.Font.Size = 13;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    rng.Style.Fill.BackgroundColor.SetColor(Color.YellowGreen);
                    rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    rng.Style.Font.Color.SetColor(Color.Black);
                }

                worksheet.Cells["A1"].LoadFromDataTable(booksTable, true);
                worksheet.Column(1).AutoFit();
                worksheet.Column(2).AutoFit();
                worksheet.Column(3).AutoFit();
                worksheet.Column(4).AutoFit();

                using (var rng = worksheet.Cells["A2:D250"])
                {
                    rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    rng.Style.Font.Color.SetColor(Color.Black);
                }

                var result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(excel.GetAsByteArray())
                };
                result.Content.Headers.ContentDisposition =
                    new System.Net.Http.Headers.ContentDispositionHeaderValue("inline")
                    {
                        FileName = "Knjige_Beeble_Export.xlsx"
                    };
                result.Content.Headers.ContentType =
                    new MediaTypeHeaderValue("application/octet-stream");

                return result;
            }

            

        }
    }
}
