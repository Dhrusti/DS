using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValidationDemoApi.Helper;

namespace ValidationDemoApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class NumberWordController : ControllerBase
    {
        private readonly NumberToWords _nwc;
        public NumberWordController(NumberToWords nwc)
        {
           _nwc = nwc;
        }

        [HttpPost("NumbertoWordCon")]
        public IActionResult NumbertoWordCon(string numberw)
        {
            var res = _nwc.NumbertoWordCon(numberw);
            return Ok(res);
        }
    }
}
