namespace MedicalBillingManagementWebAPI.ViewModels.ReqViewModel
{
	public class GetDetailUserListReqViewModel
	{
		public int RoleId { get; set; }
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
		public bool? OrderBy { get; set; }
		public string? GlobalSearch { get; set; }
	}
}
