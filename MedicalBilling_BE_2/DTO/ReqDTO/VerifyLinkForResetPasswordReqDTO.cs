using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReqDTO
{
	public class VerifyLinkForResetPasswordReqDTO
	{
		public string Email { get; set; } = null!;
		public string Link { get; set; }
	}
}
