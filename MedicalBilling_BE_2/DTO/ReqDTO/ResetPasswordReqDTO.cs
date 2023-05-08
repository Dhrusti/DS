using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReqDTO
{
	public class ResetPasswordReqDTO
	{
		public int UserId { get; set; }
		public string Email { get; set; } = null!;
		public string NewPassword { get; set; }

		[Compare("NewPassword")]
		public string ConfirmPassword { get; set; }
	}
}
