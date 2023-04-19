using DataLayer;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.IService
{
    public interface IUser
    {
        public Task<ResponseDTO> UserLoginAsync(LoginRequestDTO loginRequestDTO);
        public Task<ResponseDTO> GenerateTokenFromRefreshTokenAsync(RefreshTokenRequestDTO refreshTokenRequestDTO);
    }
}
