using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValidationDemoApi.Helper;
using ValidationDemoApi.Models;

namespace ValidationDemoApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly GenerateToken _generateToken;
        public TokenController(GenerateToken generateToken)
        {
            _generateToken = generateToken;
        }
        [HttpPost("Registration")]
        public IActionResult Registration([FromBody] UserModel model)
        {
            var res = _generateToken.Registration(model);
            return Ok(res);
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserModel model)
        {
            var res = _generateToken.Login(model);
            return Ok(res);

        }

        [HttpPost("RefreshTokenUser")]
        public IActionResult RefreshTokenUser([FromBody] RefrenceTokenModel model)
        {
            var res = _generateToken.RefreshTokenUser(model);
            return Ok(res);
        }
         
        //[HttpPost("TokenUser")]
        //public IActionResult RefreshToken()
        //{
        //    var res = _generateToken.RefreshToken();
        //    return Ok(res);
        //}



    }
}
