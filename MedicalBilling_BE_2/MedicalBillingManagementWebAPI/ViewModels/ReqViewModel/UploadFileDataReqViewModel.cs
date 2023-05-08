namespace MedicalBillingManagementWebAPI.ViewModels.ReqViewModel
{
	public class UploadFileDataReqViewModel
	{
		public int FileCategoryId { get; set; }
		public IFormFile File { get; set; }
	}
}
