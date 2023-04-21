using BussinessLayer;
using DTO.ReqDTO;
using Helpers.CommonModels;
using ServiceLayer.Interface;

namespace ServiceLayer.Implementation
{
    public class AuthImpl : IAuth
    {
        private readonly AuthBLL _authBLL;

        public AuthImpl(AuthBLL authBLL)
        {
            _authBLL = authBLL;
        }

        public async Task<CommonResponse> LoginAsync(LoginReqDTO request) => await _authBLL.LoginAsync(request);

        public async Task<string> GetEncryptionAsync(EncryptDecryptReqDTO request) => await _authBLL.GetEncryptionAsync(request);

        public async Task<string> GetDecryptionAsync(EncryptDecryptReqDTO request) => await _authBLL.GetDecryptionAsync(request);
    }
}
