using DTO.ReqDTO;
using DTO.ResDTO;
using Helper.Models;
using Mapster;
using MedicalBillingManagementWebAPI.ViewModels.ReqViewModel;
using MedicalBillingManagementWebAPI.ViewModels.ResViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;

namespace MedicalBillingManagementWebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUser _user;

		public UserController(IUser user)
		{
			_user = user;
		}

		[HttpPost("GetDetailUserList")]
		public CommonResponse GetDetailUserList(GetDetailUserListReqViewModel getDetailUserListReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _user.GetDetailUserList(getDetailUserListReqViewModel.Adapt<GetDetailUserListReqDTO>());
				GetDetailUserListResDTO model = commonResponse.Data;
				commonResponse.Data = model.Adapt<GetDetailUserListResViewModel>();
			}
			catch { throw; }
			return commonResponse;
		}

		[HttpPost("GetUserDetailById")]
		public CommonResponse GetUserDetailById(GetUserDetailByIdReqViewModel getUserDetailByIdReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _user.GetUserDetailById(getUserDetailByIdReqViewModel.Adapt<GetUserDetailByIdReqDTO>());
				GetUserDetailByIdResDTO model = commonResponse.Data;
				commonResponse.Data = model.Adapt<GetUserDetailByIdResViewModel>();
			}
			catch { throw; }
			return commonResponse;
		}

		[HttpPost("AddUser")]
		public CommonResponse AddUser(AddUserReqViewModel addUserReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _user.AddUser(addUserReqViewModel.Adapt<AddUserReqDTO>());
			}
			catch { throw; }
			return commonResponse;
		}

		[HttpPost("UpdateUser")]
		public CommonResponse UpdateUser(UpdateUserReqViewModel updateUserReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _user.UpdateUser(updateUserReqViewModel.Adapt<UpdateUserReqDTO>());
			}
			catch { throw; }
			return commonResponse;
		}

		[HttpPost("DeleteUser")]
		public CommonResponse DeleteUser(DeleteUserReqViewModel deleteUserReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _user.DeleteUser(deleteUserReqViewModel.Adapt<DeleteUserReqDTO>());
			}
			catch { throw; }
			return commonResponse;
		}
	}
}
