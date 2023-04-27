using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValidationDemoApi.Helper;

namespace ValidationDemoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinqMethodListController : ControllerBase
    {
        private readonly LinqAllListMethod _LAL;
        public LinqMethodListController(LinqAllListMethod LAL)
        {
            _LAL = LAL;
        }

        [HttpGet("SelectQueryList")]
        public IActionResult SelectQueryList()
        {
            var res = _LAL.SelectQueryList();
            return Ok(res);
        }

        [HttpGet("LinqdataList")]
        public IActionResult LinqdataList()
        {
            var res = _LAL.LinqdataList();
            return Ok(res);
        }

        [HttpGet("JoindataList")]
        public IActionResult JoindataList()
        {
            var res = _LAL.JoindataList();
            return Ok(res);
        }
    }
}
