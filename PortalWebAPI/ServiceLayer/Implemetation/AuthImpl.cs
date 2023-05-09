using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;
using DTOs.ReqDTOs;
using Helpers.CommonModels;
using ServiceLayer.Interface;

namespace ServiceLayer.Implemetation
{
	public class AuthImpl : IAuth
	{
		private readonly AuthBLL _authBLL;
		public AuthImpl(AuthBLL authBLL)
		{
			_authBLL = authBLL;
		}

		public async Task<CommonResponse> LoginAsync(LogInReqDTO request)
		{
			return await _authBLL.LoginAsync(request);
		}
	}
}
