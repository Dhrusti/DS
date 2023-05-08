using DataLayer.Entities;

namespace Helper
{
	public class CommonRepo
	{
		private readonly MedicalBillingDbContext _dbContext;
		private readonly AuthRepo _authRepo;
		private readonly CommonHelper _commonhelper;
		public CommonRepo(MedicalBillingDbContext dbContext, AuthRepo authRepo, CommonHelper commonhelper)
		{
			_dbContext = dbContext;
			_authRepo = authRepo;
			_commonhelper = commonhelper;
		}

		public IQueryable<ClientMst> getCLientList(bool IsDeleted = false, bool IsActive = true)
		{
			return _dbContext.ClientMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
		}

		public IQueryable<PhysicianMst> getAllPhysician(bool IsDeleted = false, bool IsActive = true)
		{
			return _dbContext.PhysicianMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
		}
		public IQueryable<PhysicianMst> getAllPhysicianList()
		{
			return _dbContext.PhysicianMsts.AsQueryable();
		}

		public IQueryable<CallTypeMst> getAllCallType(bool? IsDeleted = false, bool? IsActive = true)
		{
			return _dbContext.CallTypeMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
		}
		public IQueryable<ExtensionMst> getAllExtension(bool IsDeleted = false, bool IsActive = true)
		{
			return _dbContext.ExtensionMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
		}

		public IQueryable<PhysicianMst> getApptDoctor(bool IsDeleted = false, bool IsActive = true)
		{
			return _dbContext.PhysicianMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
		}
		public IQueryable<AppointmentMst> getAllAppointment(bool? IsDeleted = false, bool? IsActive = true)
		{
			return _dbContext.AppointmentMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
		}
		//public IQueryable<AppointmentMst> getAllAppointmentList()
		//{
		//    return _dbContext.AppointmentMsts.AsQueryable();
		//}

		public IQueryable<PatientEmailMst> getallpatientnEmail(bool IsDeleted = false, bool IsActive = true)
		{
			return _dbContext.PatientEmailMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
		}

		public IQueryable<NotificationMst> getAllNotification(bool IsDeleted = false, bool IsActive = true)
		{
			return _dbContext.NotificationMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
		}
		public IQueryable<UserMst> getAllUsers(bool? IsDeleted = false, bool? IsActive = true)
		{
			return _dbContext.UserMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
		}

		public IQueryable<RoleMst> getAllRoles(bool? IsDeleted = false, bool? IsActive = true)
		{
			return _dbContext.RoleMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
		}

		public IQueryable<DefaultPermission> getAllDefaultPermissions(bool? IsDeleted = false, bool? IsActive = true)
		{
			return _dbContext.DefaultPermissions.Where(x => x.IsDelete == IsDeleted && x.IsActive == IsActive).AsQueryable();
		}

		public IQueryable<PermissionMst> getAllPermissions(bool? IsDeleted = false, bool? IsActive = true)
		{
			return _dbContext.PermissionMsts.Where(x => x.IsDelete == IsDeleted && x.IsActive == IsActive).AsQueryable();
		}

		public IQueryable<UserPermission> getAllUserPermissions(bool? IsDeleted = false, bool? IsActive = true)
		{
			return _dbContext.UserPermissions.Where(x => x.IsDelete == IsDeleted && x.IsActive == IsActive).AsQueryable();
		}

		public IQueryable<RemarkMst> getAllRemarks(bool? IsDeleted = false, bool? IsActive = true)
		{
			return _dbContext.RemarkMsts.Where(x => x.IsDeleted == IsDeleted && x.IsActive == IsActive).AsQueryable();
		}

		public IQueryable<OrganizationMst> getOrganizationList(bool? IsDeleted = false, bool? IsActive = null)
		{
			return _dbContext.OrganizationMsts.Where(x => x.IsDelete == IsDeleted && (x.IsActive == IsActive || IsActive == null)).AsQueryable();
		}

		public int GetLoggedInRoleId()
		{
			int UserId = _commonhelper.GetLoggedInUserId();
			var UserDetail = getAllUsers().FirstOrDefault(x => x.Id == UserId);
			int RoleId = UserDetail != null && UserDetail.Role != null ? UserDetail.Role.Value : 0;
			return RoleId;
		}
		public bool HasPermission(int PermissionId)
		{
			bool hasPermission = false;
			int UserId = _commonhelper.GetLoggedInUserId();
			int RoleId = GetLoggedInRoleId();
			var DefaultPermissionIdList = getAllDefaultPermissions().Where(x => x.RoleId == RoleId).Select(x => x.PermissionId).ToList();
			var UserPermissionIdList = getAllUserPermissions().Where(x => x.UserId == UserId).Select(x => x.PermissionId).ToList();

			hasPermission = DefaultPermissionIdList.Contains(PermissionId) ? true : false;
			if (!hasPermission)
				hasPermission = UserPermissionIdList.Contains(PermissionId) ? true : false;

			if (RoleId == CommonConstant.Super_Admin || UserId == 0)
			{
				hasPermission = true;
			}
			return hasPermission;
		}
		public IQueryable<CompanyMst> getAllCompany(bool? IsDeleted = false, bool? IsActive = null)
		{
			return _dbContext.CompanyMsts.Where(x => x.IsDelete == IsDeleted && (x.IsActive == IsActive || IsActive == null)).AsQueryable();
		}
		public IQueryable<DepartmentMst> getAllDepartment(bool? IsDeleted = false, bool? IsActive = null)
		{
			return _dbContext.DepartmentMsts.Where(x => x.IsDelete == IsDeleted && (x.IsActive == IsActive || IsActive == null)).AsQueryable();
		}
		public IQueryable<PayerMst> getAllPayer(bool? IsDeleted = false, bool? IsActive = null)
		{
			return _dbContext.PayerMsts.Where(x => x.IsDelete == IsDeleted && (x.IsActive == IsActive || IsActive == null)).AsQueryable();
		}
		public IQueryable<ClaimMst> getClaimList(bool? IsDeleted = false, bool? IsActive = null)
		{
			return _dbContext.ClaimMsts.Where(x => x.IsDelete == IsDeleted && (x.IsActive == IsActive || IsActive == null)).AsQueryable();
		}
		public IQueryable<PatientMst> getPatientMsts(bool? IsDeleted = false, bool? IsActive = null)
		{
			return _dbContext.PatientMsts.Where(x => x.IsDelete == IsDeleted && (x.IsActive == IsActive || IsActive == null)).AsQueryable();
		}
		public IQueryable<LinkMst> linkList(bool? IsDeleted = false, bool? IsActive = true)
		{
			return _dbContext.LinkMsts.Where(x => x.IsDelete == IsDeleted && x.IsActive == IsActive).AsQueryable();
		}
		public IQueryable<ClaimStatusMst> getClaimStatusList(bool? IsDeleted = false, bool? IsActive = null)
		{
			return _dbContext.ClaimStatusMsts.Where(x => x.IsDelete == IsDeleted && (x.IsActive == IsActive || IsActive == null)).AsQueryable();
		}

		public IQueryable<FileCategoryHistoryMst> getFileCategoryHistoryList(bool? IsDeleted = false, bool? IsActive = null)
		{
			return _dbContext.FileCategoryHistoryMsts.Where(x => x.IsDelete == IsDeleted && (x.IsActive == IsActive || IsActive == null)).AsQueryable();
		}
		public IQueryable<FileDataMst> getFileDataList(bool? IsDeleted = false, bool? IsActive = true)
		{
			return _dbContext.FileDataMsts.Where(x => x.IsDelete == IsDeleted && x.IsActive == IsActive).AsQueryable();
		}

		public IQueryable<FileHistoryMst> getFileHistoryList(bool? IsDeleted = false, bool? IsActive = true)
		{
			return _dbContext.FileHistoryMsts.Where(x => x.IsDelete == IsDeleted && x.IsActive == IsActive).AsQueryable();
		}

		public IQueryable<PolicyMst> getPolicyList(bool? IsDeleted = false, bool? IsActive = true)
		{
			return _dbContext.PolicyMsts.Where(x => x.IsDelete == IsDeleted && x.IsActive == IsActive).AsQueryable();
		}
		public IQueryable<ServiceMst> getServiceList(bool? IsDeleted = false, bool? IsActive = true)
		{
			return _dbContext.ServiceMsts.Where(x => x.IsDelete == IsDeleted && x.IsActive == IsActive).AsQueryable();
		}
	}
}
