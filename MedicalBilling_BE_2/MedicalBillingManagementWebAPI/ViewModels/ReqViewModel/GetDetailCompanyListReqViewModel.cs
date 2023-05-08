namespace MedicalBillingManagementWebAPI.ViewModels.ReqViewModel
{
    public class GetDetailCompanyListReqViewModel
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; }

        public bool Orderby { get; set; }

        public string? GlobalSearch { get; set; }
    }
}
