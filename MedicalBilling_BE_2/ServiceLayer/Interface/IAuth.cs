using DTO.ReqDTO;
using DTO.ResDTO;
using Helper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface
{
    public interface IAuth
    {
        public Task<CommonResponse> LoginAsync(LogInReqDTO logInReqDTO);

        public CommonResponse GetRefreshToken(GetRefreshTokenReqDTO refreshTokenReqDTO);

        public string EncryptString(string plainText);

        public string DecryptString(string cipherText);

		public CommonResponse ForgotPassword(ForgotPasswordReqDTO forgotReqDTO);

		public CommonResponse ResetPassword(ResetPasswordReqDTO resetPasswordReqDTO);

		public CommonResponse CheckResetPasswordLink(CheckResetPasswordLinkReqDTO checkResetPasswordLinkReqDTO);
        public CommonResponse ChangePassword(ChangePasswordReqDTO changePasswordReqDTO);

	}
}
