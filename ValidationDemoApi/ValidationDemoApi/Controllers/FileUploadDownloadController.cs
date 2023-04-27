using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValidationDemoApi.Helper;
using ValidationDemoApi.Models;

namespace ValidationDemoApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class FileUploadDownloadController : ControllerBase
    {
        private readonly FileDownloadandUpload _fdu;

        public FileUploadDownloadController(FileDownloadandUpload fdu)
        {
            _fdu = fdu;

        }

        [HttpPost("FileUpload")]
        [Consumes("multipart/form-data")]
        public IActionResult FileUpload([FromForm]  string DocumentType , IFormFile UploadDocument)
        {
            var res = _fdu.UploadFile(DocumentType, UploadDocument);
            return Ok(res);
        }


        [HttpGet("download")]
        public IActionResult DownloadFile(string link)
        {
            var res = _fdu.DownloadFile(link);
            var content = res.Data.Item1;
            var contentType = "application/pdf";
            return File(content, contentType,link);
        }

    }
}
