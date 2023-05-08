namespace MedicalBillingManagementWebAPI.ViewModels.ReqViewModel
{
	public class FileCategoryHistoryReqViewModel
	{
		public string FileCategoryName { get; set; }

		public int OrganizationId { get; set; }

		public int CompanyId { get; set; }

		public int DepartmentId { get; set; }

		public IFormFile File { get; set; }
	}
}
