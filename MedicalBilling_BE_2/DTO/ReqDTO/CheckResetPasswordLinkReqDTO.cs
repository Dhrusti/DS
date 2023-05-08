using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReqDTO
{
	public class CheckResetPasswordLinkReqDTO
	{
		public string Id { get; set; }
		public string Link { get; set; }
		public string SecurityCode { get; set; }
	}
}
