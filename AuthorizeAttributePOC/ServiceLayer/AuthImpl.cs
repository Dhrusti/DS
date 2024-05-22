using BussinessLayer;
using DTO.ReqDTO;
using Helper.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class AuthImpl : IAuth
    {
        private readonly AuthBLL _authBLL;
        public AuthImpl(AuthBLL authBLL)
        {
            _authBLL = authBLL;
        }
        public async Task<CommonResponse> Login(LoginReqDTO request) => await _authBLL.Login(request);
    }
    public interface IAuth
    {
        public Task<CommonResponse> Login(LoginReqDTO request);
    }
}
