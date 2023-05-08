using DTO.ResDTO;

namespace MedicalBillingManagementWebAPI.ViewModels.ResViewModel
{
    public class GetDefaultPermissionsResViewModel
    {
		public List<GetDefaultPermission> GetDefaultPermissionList { get; set; }
		public int TotalCount { get; set; }
	}
	public class GetDefaultPermission
	{
		public int PermissionId { get; set; }
		public string PermissionName { get; set; }
		public string PermissionCode { get; set; }
		public bool IsDefault { get; set; }
	}
}
