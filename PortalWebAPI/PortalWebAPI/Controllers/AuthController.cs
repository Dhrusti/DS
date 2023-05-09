using Helpers.CommonModels;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;
using PortalWebAPI.ViewModels.ReqViewModels;
using DTOs.ReqDTOs;
using Mapster;
using DTOs.ResDTOs;
using PortalWebAPI.ViewModels.ResViewModels;
using Microsoft.AspNetCore.Authorization;

namespace PortalWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuth _iAuth;
        public AuthController(IAuth iAuth)
        {
            _iAuth = iAuth;
        }

		[AllowAnonymous]
		[HttpPost("LoginAsync")]
		public async Task<CommonResponse> LoginAsync(LogInReqViewModel request)
		{
			CommonResponse response = new CommonResponse();
			try
			{
				response = await _iAuth.LoginAsync(request.Adapt<LogInReqDTO>());
				LogInResDTO Model = response.Data;
				response.Data = Model.Adapt<LogInResViewModel>();
			}
			catch (Exception)
			{
				throw;
			}
			return response;
		}
	}
}
