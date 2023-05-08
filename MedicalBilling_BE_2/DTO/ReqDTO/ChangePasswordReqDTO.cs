using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReqDTO
{
	public class ChangePasswordReqDTO
	{
		public int UserId { get; set; }
		public string OldPassword { get; set; }
		[Required]
		[StringLength(50, MinimumLength = 2, ErrorMessage = "Minimum 2 characters required !")]
		public string NewPassword { get; set; }
		[Required]
		[Compare("NewPassword")]
		public string ConfirmPassword { get; set; }
	}
}
