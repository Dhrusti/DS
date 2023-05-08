namespace MedicalBillingManagementWebAPI.ViewModels.ResViewModel
{
    public class GetAllClaimStatusByIdResViewModel
    {
        public int OrganizationId { get; set; }

        public int CompanyId { get; set; }

        public int DepartmentId { get; set; }

        public string ClaimStatusName { get; set; } = null!;

        public bool IsActive { get; set; }
    }
}
