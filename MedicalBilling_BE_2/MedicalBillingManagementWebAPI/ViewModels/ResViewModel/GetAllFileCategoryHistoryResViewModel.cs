namespace MedicalBillingManagementWebAPI.ViewModels.ResViewModel
{
	public class GetAllFileCategoryHistoryResViewModel
	{
		public string FileCategoryName { get; set; } = null!;

		public int OrganizationId { get; set; }

		public int CompanyId { get; set; }

		public int DepartmentId { get; set; }

		public string FileFormatPath { get; set; } = null!;
	}
}
