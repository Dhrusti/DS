using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReqDTO
{
	public class UpdateUserReqDTO
	{
        public int UserId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime DOB { get; set; }
		public string Mobile { get; set; }
		public string Email { get; set; }
		public bool IsActive { get; set; }
	}
}
