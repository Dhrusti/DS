using DTO.ReqDTO;
using Helper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface
{
	public interface IUser
	{
		public CommonResponse AddUser(AddUserReqDTO addUserReqDTO);
		public CommonResponse GetDetailUserList(GetDetailUserListReqDTO getDetailUserListReqDTO);
		public CommonResponse GetUserDetailById(GetUserDetailByIdReqDTO getUserDetailByIdReqDTO);
		public CommonResponse UpdateUser(UpdateUserReqDTO updateUserReqDTO);
		public CommonResponse DeleteUser(DeleteUserReqDTO deleteUserReqDTO);
	}
}
