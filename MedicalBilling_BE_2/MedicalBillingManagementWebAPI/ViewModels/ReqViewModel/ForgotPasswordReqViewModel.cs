using System.ComponentModel.DataAnnotations;

namespace MedicalBillingManagementWebAPI.ViewModels.ReqViewModel
{
	public class ForgotPasswordReqViewModel
	{
		[Required]
		public string Email { get; set; } = null!;
	}
}
