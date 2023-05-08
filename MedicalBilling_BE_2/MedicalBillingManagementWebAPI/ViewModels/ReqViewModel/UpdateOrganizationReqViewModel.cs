namespace MedicalBillingManagementWebAPI.ViewModels.ReqViewModel
{
    public class UpdateOrganizationReqViewModel
    {
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; } = null!;

        public string OrganizationDisplayName { get; set; } = null!;

        public string? Address { get; set; }

        public string? Phone { get; set; }

        public string? Mobile { get; set; }

        public string? Email { get; set; }

        public string? Website { get; set; }

        public string? FaxNo { get; set; }

        public string? ZipCode { get; set; }

        public string? Npi { get; set; }

        public string? Bcn { get; set; }

        public string? Sonarx { get; set; }

        public string? TaxId { get; set; }

        public bool IsActive { get; set; }
    }
}
