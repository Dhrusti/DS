namespace MedicalBillingManagementWebAPI.ViewModels.ResViewModel
{
    public class GetAllClaimStatusResViewModel
    {
        public int TotalCount { get; set; }

        public List<ClaimStatusList> ClaimStatusLists { get; set; }


    }
    public class ClaimStatusList
    {
        public int ClaimStatusId { get; set; }
        public int OrganizationId { get; set; }

        public int CompanyId { get; set; }

        public int DepartmentId { get; set; }

        public string ClaimStatusName { get; set; } = null!;

        public bool IsActive { get; set; }
    }
}
