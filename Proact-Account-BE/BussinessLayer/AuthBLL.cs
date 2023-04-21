using DTO.ReqDTO;
using DTO.ResDTO;
using Helpers.CommonHelpers;
using Helpers.CommonModels;

namespace BussinessLayer
{
    public class AuthBLL
    {
        private readonly CommonHelper _commonHelper;
        public AuthBLL(CommonHelper commonHelper)
        {
            _commonHelper = commonHelper;
        }

        public async Task<CommonResponse> LoginAsync(LoginReqDTO request)
        {
            CommonResponse response = new CommonResponse();
            LoginResDTO logInResDTO = new LoginResDTO();
            try
            {
                if (request.UserId == "Admin" && request.Password == "123")
                {
                    logInResDTO.UserDetail = new { request.UserId, request.Password };

                    response.Status = true;
                    response.StatusCode = System.Net.HttpStatusCode.OK;
                    response.Message = CommonConstant.User_LoggedIn_Successfully;
                    response.Data = logInResDTO;
                }
                else
                {
                    response.Status = false;
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    response.Message = CommonConstant.Username_Or_Password_Is_Incorrect;
                }
            }
            catch { throw; }
            return response;
        }

        public async Task<string> GetEncryptionAsync(EncryptDecryptReqDTO request)
        {
            var response = "";
            try
            {
                response = await _commonHelper.EncryptStringAsync(request.Text);
            }
            catch { throw; }
            return response;
        }

        public async Task<string> GetDecryptionAsync(EncryptDecryptReqDTO request)
        {
            string response = "";
            try
            {
                response = await _commonHelper.DecryptStringAsync(request.Text);
            }
            catch { throw; }
            return response;
        }
    }
}
