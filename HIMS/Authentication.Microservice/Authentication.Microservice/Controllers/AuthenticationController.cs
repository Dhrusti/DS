using DataLayer;
using DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.IService;

namespace Authentication.Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUser _iUser;
        public AuthenticationController(IUser iUser)
        {
            this._iUser = iUser;
        }

        [HttpPost]
        [Route("UserLoginAsync")]
        public async Task<ResponseDTO> UserLoginAsync(LoginRequestDTO loginRequestDTO)
        {
            return await _iUser.UserLoginAsync(loginRequestDTO);
        }

        [HttpPost]
        [Route("GenerateTokenFromRefreshTokenAsync")]
        public async Task<ResponseDTO> GenerateTokenFromRefreshTokenAsync(RefreshTokenRequestDTO refreshTokenRequestDTO)
        {
            return await _iUser.GenerateTokenFromRefreshTokenAsync(refreshTokenRequestDTO);
        }
    }
}
