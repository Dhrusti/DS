namespace MedicalBillingManagementWebAPI.ViewModels.ReqViewModel
{
    public class AddPayerReqViewModel
    {
        public string PayerName { get; set; } = null!;

        public string? PayerCode { get; set; }

        public string? Componant { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }

        public string? Mobile { get; set; }

        public string? Email { get; set; }

        public string? Website { get; set; }

        public bool IsActive { get; set; }

    }
}
