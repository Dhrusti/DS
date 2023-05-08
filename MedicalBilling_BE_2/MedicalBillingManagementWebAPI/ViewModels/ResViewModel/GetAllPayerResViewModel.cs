namespace MedicalBillingManagementWebAPI.ViewModels.ResViewModel
{
    public class GetAllPayerResViewModel
    {
        public int Id { get; set; }

        public string PayerName { get; set; } = null!;

        public string? PayerCode { get; set; }
    }
}
