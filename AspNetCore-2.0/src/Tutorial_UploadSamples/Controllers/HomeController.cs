using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using System.Diagnostics;
using Tutorial_UploadSamples.Models;
using Tutorial_UploadSamples.Services;

namespace Tutorial_UploadSamples.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class HomeController : Controller
    {
        readonly IStreamFileUploadService _streamFileUploadService;

        public HomeController(IStreamFileUploadService streamFileUploadService)
        {
            _streamFileUploadService = streamFileUploadService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [ActionName("Index")]
        [HttpPost]
        public async Task<IActionResult> SaveFileToPhysicalFolder()
        {
            var boundary = HeaderUtilities.RemoveQuotes(
              MediaTypeHeaderValue.Parse(Request.ContentType).Boundary
             ).Value;

            var reader = new MultipartReader(boundary, Request.Body);

            var section = await reader.ReadNextSectionAsync();

            string response = string.Empty;
            try
            {
                if (await _streamFileUploadService.UploadFile(reader, section))
                {
                    ViewBag.Message = "File Upload Successful";
                }
                else
                {
                    ViewBag.Message = "File Upload Failed";
                }
            }
            catch (Exception ex)
            {
                //Log ex
                ViewBag.Message = "File Upload Failed";
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
