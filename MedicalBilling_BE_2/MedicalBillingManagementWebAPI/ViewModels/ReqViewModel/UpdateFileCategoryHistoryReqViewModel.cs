namespace MedicalBillingManagementWebAPI.ViewModels.ReqViewModel
{
	public class UpdateFileCategoryHistoryReqViewModel
	{
		public int Id { get; set; }

		public string FileCategoryName { get; set; } = null!;

		public int OrganizationId { get; set; }

		public int CompanyId { get; set; }

		public int DepartmentId { get; set; }

		public IFormFile File { get; set; } = null!;
	}
}
