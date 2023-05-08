namespace MedicalBillingManagementWebAPI.ViewModels.ReqViewModel
{
	public class CheckResetPasswordLinkReqViewModel
	{
		public string Id { get; set; }

		public string Link { get; set; }
		public string SecurityCode { get; set; }
	}
}
