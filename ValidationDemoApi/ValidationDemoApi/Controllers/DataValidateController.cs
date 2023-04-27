

using Microsoft.AspNetCore.Mvc;

using ValidationDemoApi.Helper;
using ValidationDemoApi.Models;

namespace ValidationDemoApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class DataValidateController : ControllerBase
    {
        private readonly Validate _validate;
        public DataValidateController(Validate validate)
        {
            _validate = validate;
        }
       
        [HttpPost("ValidateEmail")]
        public IActionResult Email([FromBody]string Email)
        {
          var res = _validate.Email(Email);
          return Ok(res);
        }

        [HttpPost("ValidatePinCode")]
        public IActionResult PinCode([FromForm]int? PinCode)
        {
            var res = _validate.PinCode(PinCode);
            return Ok(res);
        }

        [HttpPost("ValidateLinkedIn")]
        public IActionResult LinkedIn([FromBody] string LinkedInEmail)
        {
            var res = _validate.LinkedIn(LinkedInEmail);
            return Ok(res);
        }

        [HttpPost("ValidateWebsite")]
        public IActionResult Website([FromBody]string Website)
        {
            var res = _validate.Website(Website);
            return Ok(res);
        }

    }
}
