using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValidationDemoApi.Helper;

namespace ValidationDemoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DateController : ControllerBase
    {
        private readonly DateTemp _dateTemp;

        public DateController(DateTemp dateTemp)
        {
         _dateTemp = dateTemp;
        }
        [HttpGet]
        [Route("Dateconvert")]
        public string Dateconvert(string datedata)
        {

           
            string date = _dateTemp.DateConvert(datedata);

            return date;
        }
    }
}
