using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValidationDemoApi.Helper;
using Microsoft.AspNetCore.Http;



namespace ValidationDemoApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class SendSMSController : ControllerBase
    {
        private readonly SendSMSOTP _sendsms;
      
        public SendSMSController(SendSMSOTP sendsms)
        {
            _sendsms = sendsms;
           
        }

        [HttpPost("SendSMS")]
        public IActionResult SendSMS([FromBody] string Contact)
        {
            var res = _sendsms.SendSMS(Contact);
            return Ok(res);
        }
        [HttpPost("SendOTP")]
        public IActionResult SendOTP([FromBody] string Contact)
        {
            var res = _sendsms.SendOTP(Contact);
            return Ok(res);
        }

        [HttpPost("VerifyOTP")]
        public IActionResult VerifyOTP([FromBody] string OTP)
        {
            var res = _sendsms.VerifyOTP(OTP);
            return Ok(res);


        }
    }
}
