namespace MedicalBillingManagementWebAPI.ViewModels.ReqViewModel
{
	public class UpdateDefaultPermissionsReqViewModel
	{
		public int RoleId { get; set; }
		public List<DefaultPermissionModel> DefaultPermissionList { get; set; }
	}

	public class DefaultPermissionModel
	{
		public int PermissionId { get; set; }
		public string PermissionName { get; set; }
		public string PermissionCode { get; set; }
		public bool IsDefault { get; set; }
	}
}
