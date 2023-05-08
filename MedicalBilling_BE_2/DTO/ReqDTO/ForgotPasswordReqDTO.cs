using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReqDTO
{
	public class ForgotPasswordReqDTO
	{
		[Required]
		public string Email { get; set; } = null!;
	}
}
