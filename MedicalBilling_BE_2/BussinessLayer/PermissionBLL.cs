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
	public class PermissionBLL
	{
		private readonly CommonHelper _commonHelper;
		private readonly CommonRepo _commonRepo;
		private readonly MedicalBillingDbContext _dbContext;
		public PermissionBLL(CommonRepo commonRepo, CommonHelper commonHelper, MedicalBillingDbContext dbContext)
		{
			_commonHelper = commonHelper;
			_commonRepo = commonRepo;
			_dbContext = dbContext;
		}

		public CommonResponse GetDefaultPermissions(GetDefaultPermissionsReqDTO getDefaultPermissionsReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				if (_commonRepo.HasPermission(CommonConstant.Default_Permission_View))
				{
					int PageNumber = getDefaultPermissionsReqDTO.PageNumber == 0 ? _commonHelper.PageNumber : getDefaultPermissionsReqDTO.PageNumber;
					int PageSize = getDefaultPermissionsReqDTO.PageSize == 0 ? _commonHelper.PageSize : getDefaultPermissionsReqDTO.PageSize;
					bool OrderBy = getDefaultPermissionsReqDTO.OrderBy == null ? _commonHelper.OrderBy : getDefaultPermissionsReqDTO.OrderBy.Value;
					bool IsDefault = false;

					GetDefaultPermissionsResDTO getDefaultPermissionsResDTO = new GetDefaultPermissionsResDTO();
					getDefaultPermissionsResDTO.GetDefaultPermissionList = new List<GetDefaultPermission>();

					var PermissionList = _commonRepo.getAllPermissions();
					if (!string.IsNullOrWhiteSpace(getDefaultPermissionsReqDTO.GlobalSearch))
					{
						PermissionList = PermissionList.Where(x => x.PermissionName.Contains(getDefaultPermissionsReqDTO.GlobalSearch));
					}
					PermissionList.ToList();

					var DefaultPermissionIdList = _commonRepo.getAllDefaultPermissions().Where(x => x.RoleId == getDefaultPermissionsReqDTO.RoleId).Select(x => x.PermissionId).ToList();

					GetDefaultPermission getDefaultPermission = new GetDefaultPermission();
					foreach (var permission in PermissionList)
					{
						IsDefault = DefaultPermissionIdList.Contains(permission.Id);
						getDefaultPermission = new GetDefaultPermission();
						getDefaultPermission.PermissionId = permission.Id;
						getDefaultPermission.PermissionName = permission.PermissionName;
						getDefaultPermission.PermissionCode = permission.PermissionCode;
						getDefaultPermission.IsDefault = IsDefault;

						getDefaultPermissionsResDTO.GetDefaultPermissionList.Add(getDefaultPermission);
					}

					getDefaultPermissionsResDTO.TotalCount = getDefaultPermissionsResDTO.GetDefaultPermissionList.Count();
					if (OrderBy)
						//getDefaultPermissionsResDTO.GetDefaultPermissionList = getDefaultPermissionsResDTO.GetDefaultPermissionList.OrderBy(x => x.PermissionName).Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();
						getDefaultPermissionsResDTO.GetDefaultPermissionList = getDefaultPermissionsResDTO.GetDefaultPermissionList.OrderBy(x => x.PermissionName).ToList();
					else
						//getDefaultPermissionsResDTO.GetDefaultPermissionList = getDefaultPermissionsResDTO.GetDefaultPermissionList.OrderByDescending(x => x.PermissionName).Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();
						getDefaultPermissionsResDTO.GetDefaultPermissionList = getDefaultPermissionsResDTO.GetDefaultPermissionList.OrderByDescending(x => x.PermissionName).ToList();

					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Message = "Success!";
					commonResponse.Data = getDefaultPermissionsResDTO;
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

		public CommonResponse UpdateDefaultPermissions(UpdateDefaultPermissionsReqDTO updateDefaultPermissionsReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				if (_commonRepo.HasPermission(CommonConstant.Default_Permission_Update))
				{
					bool TransactionStatus = false;
					using (TransactionScope transactionScope = new TransactionScope())
					{
						DateTime CurrentDateTime = _commonHelper.GetCurrentDateTime();
						int LoggedInUserId = _commonHelper.GetLoggedInUserId();

						var DefaultPermissionList = _commonRepo.getAllDefaultPermissions().Where(x => x.RoleId == updateDefaultPermissionsReqDTO.RoleId).ToList();
						List<DefaultPermission> DBDefaultPermissionList = new List<DefaultPermission>();

						foreach (var item in updateDefaultPermissionsReqDTO.DefaultPermissionList.Where(x => x.IsDefault).ToList())
						{
							DefaultPermission defaultPermission = new DefaultPermission();
							defaultPermission.RoleId = updateDefaultPermissionsReqDTO.RoleId;
							defaultPermission.PermissionId = item.PermissionId;
							defaultPermission.IsActive = true;
							defaultPermission.IsDelete = false;
							defaultPermission.CreatedBy = LoggedInUserId;
							defaultPermission.UpdatedBy = LoggedInUserId;
							defaultPermission.CreatedDate = CurrentDateTime;
							defaultPermission.UpdatedDate = CurrentDateTime;

							DBDefaultPermissionList.Add(defaultPermission);
						}

						_dbContext.DefaultPermissions.RemoveRange(DefaultPermissionList);
						_dbContext.DefaultPermissions.AddRange(DBDefaultPermissionList);
						_dbContext.SaveChanges();
						TransactionStatus = DBDefaultPermissionList.Where(x => x.PermissionId <= 0).Count() > 0 ? false : true;
						if (!TransactionStatus)
						{
							transactionScope.Dispose();
						}
						else
						{
							transactionScope.Complete();
							commonResponse.Status = true;
							commonResponse.StatusCode = HttpStatusCode.OK;
							commonResponse.Message = "Success!";
							commonResponse.Data = DBDefaultPermissionList;
						}
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

	}
}
