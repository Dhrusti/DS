using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DTOs.ReqDTOs;
using DTOs.ResDTOs;
using Helper.CommonHelper;
using Helpers.CommonModels;

namespace BusinessLayer
{
	public class AuthBLL
	{
		private readonly CommonRepo _commonRepo;
		private readonly AuthRepo _authRepo;
		public AuthBLL(CommonRepo commonRepo, AuthRepo authRepo)
		{
			_commonRepo = commonRepo;
			_authRepo = authRepo;
		}
		public async Task<CommonResponse> LoginAsync(LogInReqDTO request)
		{
			CommonResponse response = new CommonResponse();
			LogInResDTO logInResDTO = new LogInResDTO();
			try
			{
				if (request.UserName == "Dhrusti" && request.Password == "123")
				{

					response = getSuccessLoginResponse(logInResDTO);

					response.Status = true;
					response.StatusCode = HttpStatusCode.OK;
					response.Message = CommonConstants.Logged_In_Successfully;
					response.Data = logInResDTO;
				}
				else
				{
					response.StatusCode = HttpStatusCode.NotFound;
					response.Message = CommonConstants.Wrong_Credentials;
				}
			}
			catch { throw; }
			return response;
		}


		private CommonResponse getSuccessLoginResponse(LogInResDTO logInResDTO)
		{
			CommonResponse response = new CommonResponse();
			var token = _authRepo.CreateJWT(logInResDTO.Id, false);
			_authRepo.CreateRefreshToken();

			if (token != null)
			{
				//UserDetail.AccessCategory = _commonRepo.getAllRoleList().Where(x=>x.Id == Convert.ToInt32(UserDetail.Role)).Select(x=>x.RoleName).FirstOrDefault() ?? "IFA";
				//logInResDTO.UserDetail = UserDetail;
				logInResDTO.Token = token.Token;
				logInResDTO.RefreshToken = token.RefreshToken;

				response.Status = true;
				response.StatusCode = HttpStatusCode.OK;
				response.Message = "LogIn Sucessfully.";
			}
			else
			{
				response.Message = "Token not Generated";
			}
			return response;
		}
	}
}
