using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyBlazorApp.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UploadController : Controller
    {
        public IWebHostEnvironment HostingEnvironment { get; set; }

        public UploadController(IWebHostEnvironment hostingEnvironment)
        {
            HostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        public async Task<IActionResult> Save(IFormFile files) // must match SaveField
        {
            if (files != null)
            {
                try
                {
                    // save to wwwroot - Blazor Server only
                    var saveLocation = Path.Combine(HostingEnvironment.WebRootPath, files.FileName);
                    // save to project root - Blazor Server or WebAssembly
                    //var saveLocation = Path.Combine(HostingEnvironment.ContentRootPath, files.FileName);

                    using (var fileStream = new FileStream(saveLocation, FileMode.Create))
                    {
                        await files.CopyToAsync(fileStream);
                    }
                }
                catch
                {
                    Response.StatusCode = 500;
                    await Response.WriteAsync("Upload failed.");
                }
            }

            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult Remove(string files) // must match RemoveField
        {
            if (files != null)
            {
                try
                {
                    // delete from wwwroot - Blazor Server only
                    var fileLocation = Path.Combine(HostingEnvironment.WebRootPath, files);
                    // delete from project root - Blazor Server or WebAssembly
                    //var fileLocation = Path.Combine(HostingEnvironment.ContentRootPath, files);

                    if (System.IO.File.Exists(fileLocation))
                    {
                        System.IO.File.Delete(fileLocation);
                    }
                }
                catch
                {
                    Response.StatusCode = 500;
                    Response.WriteAsync("File deletion failed.");
                }
            }

            return new EmptyResult();
        }
    }
}