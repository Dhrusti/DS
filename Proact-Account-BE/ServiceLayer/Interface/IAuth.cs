using DTO.ReqDTO;
using Helpers.CommonModels;

namespace ServiceLayer.Interface
{
    public interface IAuth
    {
        public Task<CommonResponse> LoginAsync(LoginReqDTO request);
        public Task<string> GetEncryptionAsync(EncryptDecryptReqDTO request);
        public Task<string> GetDecryptionAsync(EncryptDecryptReqDTO request);
    }
}
