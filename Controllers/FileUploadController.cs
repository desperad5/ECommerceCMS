using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

[Produces("application/json")]
[Authorize]
public class FileUploadController : Controller
{
    private IWebHostEnvironment _hostingEnvironment;

    public FileUploadController(IWebHostEnvironment hostingEnvironment){
        _hostingEnvironment = hostingEnvironment;
    }

    [HttpPost("FileUpload")]
	[DisableRequestSizeLimit]
    public ActionResult FileUpload()
    {
        try
			{
				var file = Request.Form.Files[0];
				string folderName = "Upload";
				string webRootPath = _hostingEnvironment.WebRootPath;
				string newPath = Path.Combine(webRootPath, folderName);
				if (!Directory.Exists(newPath))
				{
					Directory.CreateDirectory(newPath);
				}
				if (file.Length > 0)
				{
					string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
					string fullPath = Path.Combine(newPath, fileName);
					using (var stream = new FileStream(fullPath, FileMode.Create))
					{
						file.CopyTo(stream);
					}
				}
				return Json("Upload Successful.");
			}
			catch (System.Exception ex)
			{
				return Json("Upload Failed: " + ex.Message);
			}
    }
}