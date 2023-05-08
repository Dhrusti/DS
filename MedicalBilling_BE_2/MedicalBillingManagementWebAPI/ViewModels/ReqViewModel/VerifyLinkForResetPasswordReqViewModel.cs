namespace MedicalBillingManagementWebAPI.ViewModels.ReqViewModel
{
	public class VerifyLinkForResetPasswordReqViewModel
	{
		public string Email { get; set; } = null!;
		public string Link { get; set; }
	}
}
