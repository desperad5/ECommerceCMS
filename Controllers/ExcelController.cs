using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using OfficeOpenXml;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ECommerceCMS.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("EnableCors")]
    [Authorize]
    public class ExcelController : Controller
    {

        private readonly IHostingEnvironment _hostingEnvironment;
        private string fileName { get; set; }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> DownloadExcel(int type)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage excel = new ExcelPackage())
            {
                var worksheet = excel.Workbook.Worksheets.Add("Markalar");

                var headerRow = new List<string[]>() { new string[] { "Marka Adı", "Web Sitesi Url'i" } };

                // Determine the header range (e.g. A1:D1)
                string headerRange = "A1:" + Char.ConvertFromUtf32(headerRow[0].Length + 64) + "1";

                // Popular header row data
                worksheet.Cells[headerRange].LoadFromArrays(headerRow);

                FileInfo excelFile = new FileInfo(@"C:\Users\e_sez\OneDrive\Desktop\markalar.xlsx");
                excel.SaveAs(excelFile);

                return File(excel.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

                ////string wwwrootPath = _hostingEnvironment.WebRootPath;
                //string wwwrootPath = @"C:\Users\e_sez\OneDrive\Desktop";
                //fileName = @"markalar.xlsx";
                //FileInfo file = new FileInfo(Path.Combine(wwwrootPath, fileName));

                //IFileProvider provider = new PhysicalFileProvider(wwwrootPath);
                //IFileInfo fileInfo = provider.GetFileInfo(fileName);
                //var readStream = fileInfo.CreateReadStream();
                //var mimeType = "application/vnd.ms-excel";
                //return File(readStream, mimeType, fileName);

                //using (var stream = new MemoryStream(excel.GetAsByteArray()))
                //{
                //    stream.Position = 0;
                //    return File(stream, "application/vnd.ms-excel", "mytestfile.xls");
                //}
            }
        }
    }
}
