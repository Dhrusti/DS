using Helpers.CommonModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpPost("TestAsync")]
        public async Task<CommonResponse> TestAsync()
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse.Data = "Test Done.";
                commonResponse.Status = true;
                commonResponse.StatusCode = HttpStatusCode.OK;
                commonResponse.Message = "Test Done Successfully!";
            }
            catch (Exception) { throw; }
            return commonResponse;
        }
    }
}
