using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Helper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BussinessLayer
{
	public class UserBLL
	{
		private readonly CommonHelper _commonHelper;
		private readonly CommonRepo _commonRepo;
		private readonly MedicalBillingDbContext _dbContext;
		private readonly AuthBLL _authBLL;
		public UserBLL(CommonRepo commonRepo, CommonHelper commonHelper, MedicalBillingDbContext dbContext, AuthBLL authBLL)
		{
			_commonHelper = commonHelper;
			_commonRepo = commonRepo;
			_dbContext = dbContext;
			_authBLL = authBLL;
		}

		public CommonResponse GetDetailUserList(GetDetailUserListReqDTO getDetailUserListReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			GetDetailUserListResDTO getDetailUserListResDTO = new GetDetailUserListResDTO();
			try
			{
				if (_commonRepo.HasPermission(CommonConstant.Aging_User_View))
				{
					int PageNumber = getDetailUserListReqDTO.PageNumber == 0 ? _commonHelper.PageNumber : getDetailUserListReqDTO.PageNumber;
					int PageSize = getDetailUserListReqDTO.PageSize == 0 ? _commonHelper.PageSize : getDetailUserListReqDTO.PageSize;
					bool OrderBy = getDetailUserListReqDTO.OrderBy == null ? _commonHelper.OrderBy : getDetailUserListReqDTO.OrderBy.Value;

					var UserList = _commonRepo.getAllUsers();
					var RoleList = _commonRepo.getAllRoles().ToList();

					if (!string.IsNullOrWhiteSpace(getDetailUserListReqDTO.GlobalSearch))
					{
						UserList = UserList.Where(x => x.FirstName.Contains(getDetailUserListReqDTO.GlobalSearch) || x.LastName.Contains(getDetailUserListReqDTO.GlobalSearch) || x.UserName.Contains(getDetailUserListReqDTO.GlobalSearch));
					}
					if (getDetailUserListReqDTO.RoleId > 0)
					{
						UserList = UserList.Where(x => x.Role == getDetailUserListReqDTO.RoleId);
					}
					UserList.ToList();

					getDetailUserListResDTO.GetDetailUserList = new List<GetDetailUser>();
					GetDetailUser getDetailUser = new GetDetailUser();
					foreach (var item in UserList)
					{
						var RoleDetails = RoleList.FirstOrDefault(x => x.Id == item.Role);
						getDetailUser = new GetDetailUser();
						getDetailUser.UserId = item.Id;
						getDetailUser.FirstName = item.FirstName;
						getDetailUser.LastName = item.LastName;
						getDetailUser.UserName = item.UserName;
						getDetailUser.Dob = item.Dob;
						getDetailUser.MobileNo = item.MobileNo;
						getDetailUser.Email = item.Email;
						getDetailUser.IsActive = item.IsActive;
						getDetailUser.RoleId = RoleDetails != null ? RoleDetails.Id : 0;
						getDetailUser.RoleName = RoleDetails != null ? RoleDetails.RoleName : "";

						getDetailUserListResDTO.GetDetailUserList.Add(getDetailUser);
					}

					getDetailUserListResDTO.TotalCount = getDetailUserListResDTO.GetDetailUserList.Count();
					if (OrderBy)
						getDetailUserListResDTO.GetDetailUserList = getDetailUserListResDTO.GetDetailUserList.OrderBy(x => x.FirstName).Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();
					else
						getDetailUserListResDTO.GetDetailUserList = getDetailUserListResDTO.GetDetailUserList.OrderByDescending(x => x.FirstName).Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();

					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Message = "Success!";
					commonResponse.Data = getDetailUserListResDTO;
				}
				else
				{
					commonResponse.StatusCode = HttpStatusCode.Forbidden;
					commonResponse.Message = "You Don't Have Permission To Access This!";
				}

			}
			catch { throw; }
			return commonResponse;
		}
		public CommonResponse GetUserDetailById(GetUserDetailByIdReqDTO getUserDetailByIdReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			GetUserDetailByIdResDTO getUserDetailByIdResDTO = new GetUserDetailByIdResDTO();
			try
			{
				if (_commonRepo.HasPermission(CommonConstant.Aging_User_Update))
				{
					var RoleList = _commonRepo.getAllRoles().ToList();
					var UserDetail = _commonRepo.getAllUsers().FirstOrDefault(x => x.Id == getUserDetailByIdReqDTO.UserId);
					if (UserDetail != null)
					{
						var RoleDetail = RoleList.FirstOrDefault(x => x.Id == UserDetail.Role);
						getUserDetailByIdResDTO.UserId = UserDetail.Id;
						getUserDetailByIdResDTO.FirstName = UserDetail.FirstName;
						getUserDetailByIdResDTO.LastName = UserDetail.LastName;
						getUserDetailByIdResDTO.UserName = UserDetail.UserName;
						getUserDetailByIdResDTO.MobileNo = UserDetail.MobileNo;
						getUserDetailByIdResDTO.Email = UserDetail.Email;
						getUserDetailByIdResDTO.Dob = UserDetail.Dob;
						getUserDetailByIdResDTO.IsActive = UserDetail.IsActive;
						getUserDetailByIdResDTO.RoleId = RoleDetail != null ? UserDetail.Id : 0;
						getUserDetailByIdResDTO.RoleName = RoleDetail != null ? RoleDetail.RoleName : "";

						commonResponse.Status = true;
						commonResponse.StatusCode = HttpStatusCode.OK;
						commonResponse.Message = "Success!";
						commonResponse.Data = getUserDetailByIdResDTO;
					}
					else
					{
						commonResponse.Message = "User Not Found!";
						commonResponse.StatusCode = HttpStatusCode.NotFound;
					}
				}
				else
				{
					commonResponse.StatusCode = HttpStatusCode.Forbidden;
					commonResponse.Message = "You Don't Have Permission To Access This!";
				}
			}
			catch { throw; }
			return commonResponse;
		}
		public CommonResponse AddUser(AddUserReqDTO addUserReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				if (_commonRepo.HasPermission(CommonConstant.Aging_User_Add))
				{
					var UserList = _commonRepo.getAllUsers().ToList();
					bool duplicateCheck = UserList.FirstOrDefault(x => x.UserName == addUserReqDTO.UserName) != null ? true : false;
					if (!duplicateCheck)
					{
						int LoggedInUserId = _commonHelper.GetLoggedInUserId();

						using (TransactionScope transactionScope = new TransactionScope())
						{
							UserMst userMst = new UserMst();
							userMst.Role = addUserReqDTO.RoleId;
							userMst.FirstName = addUserReqDTO.FirstName;
							userMst.LastName = addUserReqDTO.LastName;
							userMst.UserName = addUserReqDTO.UserName;
							userMst.Password = addUserReqDTO.Password;
							userMst.Dob = addUserReqDTO.DOB;
							userMst.MobileNo = addUserReqDTO.Mobile;
							userMst.Email = addUserReqDTO.Email;
							userMst.IsActive = addUserReqDTO.IsActive;
							userMst.IsDeleted = false;
							userMst.CreatedBy = LoggedInUserId;
							userMst.UpdatedBy = LoggedInUserId;
							userMst.CreatedDate = _commonHelper.GetCurrentDateTime();
							userMst.UpdatedDate = _commonHelper.GetCurrentDateTime();

							_dbContext.UserMsts.Add(userMst);
							_dbContext.SaveChanges();

							var res = AddDefaultPermissionsForUser(userMst.Id);
							if (res.Status)
							{
								transactionScope.Complete();
								commonResponse.Status = true;
								commonResponse.StatusCode = HttpStatusCode.OK;
								commonResponse.Message = "User Added Successfully!";
								commonResponse.Data = userMst.Id;
							}
						}
					}
					else
					{
						commonResponse.Message = "User Name Already Exists!";
					}
				}
				else
				{
					commonResponse.StatusCode = HttpStatusCode.Forbidden;
					commonResponse.Message = "You Don't Have Permission To Access This!";
				}
			}
			catch { throw; }
			return commonResponse;
		}
		public CommonResponse UpdateUser(UpdateUserReqDTO updateUserReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				if (_commonRepo.HasPermission(CommonConstant.Aging_User_Update))
				{
					UserMst userMst = new UserMst();
					var UserDetail = _commonRepo.getAllUsers().FirstOrDefault(x => x.Id == updateUserReqDTO.UserId);
					if (UserDetail != null)
					{
						int LoggedInUserId = _commonHelper.GetLoggedInUserId();
						userMst = UserDetail;
						userMst.FirstName = updateUserReqDTO.FirstName;
						userMst.LastName = updateUserReqDTO.LastName;
						userMst.Dob = updateUserReqDTO.DOB;
						userMst.MobileNo = updateUserReqDTO.Mobile;
						userMst.Email = updateUserReqDTO.Email;
						userMst.IsActive = updateUserReqDTO.IsActive;
						userMst.UpdatedBy = LoggedInUserId;
						userMst.UpdatedDate = _commonHelper.GetCurrentDateTime();

						_dbContext.SaveChanges();

						commonResponse.Status = true;
						commonResponse.StatusCode = HttpStatusCode.OK;
						commonResponse.Message = "User Updated Successfully!";
						commonResponse.Data = userMst.Id;
					}
					else
					{
						commonResponse.Message = "User Not Found!";
					}
				}
				else
				{
					commonResponse.StatusCode = HttpStatusCode.Forbidden;
					commonResponse.Message = "You Don't Have Permission To Access This!";
				}
			}
			catch { throw; }
			return commonResponse;
		}

		public CommonResponse DeleteUser(DeleteUserReqDTO deleteUserReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				if (_commonRepo.HasPermission(CommonConstant.Aging_User_Delete))
				{
					UserMst userMst = new UserMst();
					var UserDetail = _commonRepo.getAllUsers().FirstOrDefault(x => x.Id == deleteUserReqDTO.UserId);
					if (UserDetail != null)
					{
						int LoggedInUserId = _commonHelper.GetLoggedInUserId();
						userMst = UserDetail;
						userMst.IsDeleted = true;
						userMst.UpdatedBy = LoggedInUserId;
						userMst.UpdatedDate = _commonHelper.GetCurrentDateTime();
						_dbContext.SaveChanges();

						commonResponse.Status = true;
						commonResponse.StatusCode = HttpStatusCode.OK;
						commonResponse.Message = "User Deleted Successfully!";
						commonResponse.Data = userMst.Id;
					}
					else
					{
						commonResponse.Message = "User Not Found!";
					}
				}
				else
				{
					commonResponse.StatusCode = HttpStatusCode.Forbidden;
					commonResponse.Message = "You Don't Have Permission To Access This!";
				}
			}
			catch { throw; }
			return commonResponse;
		}

		public CommonResponse AddDefaultPermissionsForUser(int UserId)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				int RoleId = _commonRepo.GetLoggedInRoleId();
				var UserPermissions = _authBLL.GetPermissionsByUserId(UserId).Where(x => x.HasPermission == true).ToList();
				DateTime CurrentDateTime = _commonHelper.GetCurrentDateTime();
				int LoggedInUserId = _commonHelper.GetLoggedInUserId();
				List<UserPermission> userPermissionList = new List<UserPermission>();
				UserPermission userPermission = new UserPermission();
				foreach (var item in UserPermissions)
				{
					userPermission = new UserPermission();
					userPermission.PermissionId = item.PermissionId;
					userPermission.UserId = UserId;
					userPermission.RoleId = RoleId;
					userPermission.IsActive = true;
					userPermission.IsDelete = false;
					userPermission.CreatedBy = LoggedInUserId;
					userPermission.UpdatedBy = LoggedInUserId;
					userPermission.CreatedDate = CurrentDateTime;
					userPermission.UpdatedDate = CurrentDateTime;

					userPermissionList.Add(userPermission);
				}

				_dbContext.UserPermissions.AddRange(userPermissionList);
				_dbContext.SaveChanges();
				commonResponse.Status = true;
				commonResponse.StatusCode = HttpStatusCode.OK;
				commonResponse.Message = "User Permissions Added Successfully!";
				commonResponse.Data = UserId;
			}
			catch { throw; }
			return commonResponse;
		}
	}
}
