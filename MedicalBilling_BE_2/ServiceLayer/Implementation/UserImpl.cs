using BussinessLayer;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper.Models;
using ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Implementation
{
	public class UserImpl : IUser
	{
		private readonly UserBLL _userBLL;

		public UserImpl(UserBLL userBLL)
		{
			_userBLL = userBLL;
		}

		public CommonResponse AddUser(AddUserReqDTO addUserReqDTO)
		{
			return _userBLL.AddUser(addUserReqDTO);
		}
		public CommonResponse GetDetailUserList(GetDetailUserListReqDTO getDetailUserListReqDTO)
		{
			return _userBLL.GetDetailUserList(getDetailUserListReqDTO);
		}
		public CommonResponse GetUserDetailById(GetUserDetailByIdReqDTO getUserDetailByIdReqDTO)
		{
			return _userBLL.GetUserDetailById(getUserDetailByIdReqDTO);
		}
		public CommonResponse UpdateUser(UpdateUserReqDTO updateUserReqDTO)
		{
			return _userBLL.UpdateUser(updateUserReqDTO);
		}
		public CommonResponse DeleteUser(DeleteUserReqDTO deleteUserReqDTO)
		{
			return _userBLL.DeleteUser(deleteUserReqDTO);
		}
	}
}
