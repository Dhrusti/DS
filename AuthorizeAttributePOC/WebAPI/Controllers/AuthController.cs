﻿using DTO.ReqDTO;
using DTO.ResDTO;
using Helper.CommonModels;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer;
using WebAPI.ViewModels.ReqViewModel;
using WebAPI.ViewModels.ResViewModel;

namespace WebAPI.Controllers
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
        [HttpPost("Login")]
        public async Task<CommonResponse> Login(LoginReqViewModel request)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                response = await _iAuth.Login(request.Adapt<LoginReqDTO>());
                LoginResDTO res = response.Data;
                response.Data = res.Adapt<LoginResViewModel>();
            }
            catch { throw; }
            return response;
        }
    }
}
