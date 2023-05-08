using System.ComponentModel.DataAnnotations;

namespace MedicalBillingManagementWebAPI.ViewModels.ReqViewModel
{
	public class ResetPasswordReqViewModel
	{
		public int UserId { get; set; }
		public string Email { get; set; } = null!;
		public string NewPassword { get; set; }
		[Compare("NewPassword")]
		public string ConfirmPassword { get; set; }
	}
}
