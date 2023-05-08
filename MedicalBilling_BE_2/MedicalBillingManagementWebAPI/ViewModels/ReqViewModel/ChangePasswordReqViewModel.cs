using System.ComponentModel.DataAnnotations;

namespace MedicalBillingManagementWebAPI.ViewModels.ReqViewModel
{
	public class ChangePasswordReqViewModel
	{
		public int UserId { get; set; }
		public string OldPassword { get; set; }
		[Required]
		[StringLength(50,MinimumLength = 2 , ErrorMessage = "Minimum 2 characters required !")]
		public string NewPassword { get; set; }
		[Required]
		[Compare("NewPassword")]
		public string ConfirmPassword { get; set; }
	}
}
