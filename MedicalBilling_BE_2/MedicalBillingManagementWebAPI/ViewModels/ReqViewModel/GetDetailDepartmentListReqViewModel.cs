namespace MedicalBillingManagementWebAPI.ViewModels.ReqViewModel
{
    public class GetDetailDepartmentListReqViewModel
    {
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
        public bool? OrdenBy { get; set; }
        public string GlobalSearch { get; set; }
    }
}
