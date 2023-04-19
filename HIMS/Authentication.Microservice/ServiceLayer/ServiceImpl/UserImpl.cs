using BusinessLayer;
using DataLayer;
using DTO;
using ServiceLayer.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceImpl
{
    public class UserImpl : IUser
    {
        private readonly AuthenticationBLL _authenticationBLL;
        public UserImpl(AuthenticationBLL authenticationBLL)
        {
            _authenticationBLL = authenticationBLL;
        }

        public async Task<ResponseDTO> UserLoginAsync(LoginRequestDTO loginRequestDTO)
        {
            return await _authenticationBLL.UserLoginAsync(loginRequestDTO);
        }
        public async Task<ResponseDTO> GenerateTokenFromRefreshTokenAsync(RefreshTokenRequestDTO refreshTokenRequestDTO)
        {
            return await _authenticationBLL.GenerateTokenFromRefreshTokenAsync(refreshTokenRequestDTO);
        }
    }
}
