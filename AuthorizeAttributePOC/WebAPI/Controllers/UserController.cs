using DTO.ReqDTO;
using Helper.CommonModels;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer;
using WebAPI.ViewModels.ReqViewModel;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _iUser;
        public UserController(IUser iUser)
        {
            _iUser = iUser;
        }

        [HttpGet("GetAllUserDetailList")]
        public async Task<CommonResponse> GetAllUserDetailList()
        {
            CommonResponse response = new CommonResponse();
            try
            {
                response = await _iUser.GetAllUserDetailList();
            }
            catch { throw; }
            return response;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("AddEditUser")]
        public async Task<CommonResponse> AddEditUser(AddEditUserReqViewModel request)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                response = await _iUser.AddEditUser(request.Adapt<AddEditUserReqDTO>());
            }
            catch { throw; }
            return response;
        }
    }
}
