using DTO.ReqDTO;
using DTO.ResDTO;
using Helpers.CommonHelpers;
using Helpers.CommonModels;
using Mapster;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;
using System.Security.Claims;
using WebAPI.ViewModels.ReqViewModels;
using WebAPI.ViewModels.ResViewModels;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IAuth _auth;
        private readonly IConfiguration _configuration;
        public AuthController(IAuth auth, IConfiguration configuration)
        {
            _auth = auth;
            _configuration = configuration;
        }

        /// <summary>
        /// Checks for user authorized or not
        /// </summary>
        /// <param name="request">UserName</param>
        /// <param name="request">Password</param>
        /// <param name="request">Remember Me</param>
        /// <returns>200 if success</returns>
        /// <returns>404 if not found</returns>
        /// <returns>500 if failed</returns>
        [HttpPost("LoginAsync")]
        public async Task<CommonResponse> LoginAsync(LoginReqViewModel request)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                response = await _auth.LoginAsync(request.Adapt<LoginReqDTO>());
                LoginResDTO Model = response.Data;

                if (response.Status && Convert.ToBoolean(_configuration.GetSection("CommonSwitches:AuthenticationEnable").Value))
                {
                    var claims = new List<Claim> { new Claim(ClaimTypes.Name, "Admin"), new Claim(ClaimTypes.Role, request.UserId) };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    var props = new AuthenticationProperties();

                    props.IsPersistent = request.RememberMe;

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props).Wait();
                }

                response.Data = Model.Adapt<LoginResViewModel>();
            }
            catch { throw; }
            return response;
        }

        /// <summary>
        /// Logout and clears the user session
        /// </summary>
        /// <returns>200 if success</returns>
        /// <returns>500 if failed</returns>
        [HttpPost("LogoutAsync")]
        public CommonResponse LogoutAsync()
        {
            CommonResponse response = new CommonResponse();
            try
            {
                if (Convert.ToBoolean(_configuration.GetSection("AuthenticationEnable").Value))
                {
                    HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
                }

                response.Status = true;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.Message = CommonConstant.User_LoggedOut_Successfully;
            }
            catch (Exception ex)
            {
                response.Data = ex.ToString();
            }

            return response;
        }

        [HttpPost("GetEncryptionAsync")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<string> GetEncryptionAsync(EncryptDecryptReqViewModel request)
        {
            return await _auth.GetEncryptionAsync(request.Adapt<EncryptDecryptReqDTO>());
        }

        [HttpPost("GetDecryptionAsync")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<string> GetDecryptionAsync(EncryptDecryptReqViewModel request)
        {
            string res = "";
            try
            {
                res = await _auth.GetDecryptionAsync(request.Adapt<EncryptDecryptReqDTO>());
            }
            catch { throw; }
            return res;
        }
    }
}
