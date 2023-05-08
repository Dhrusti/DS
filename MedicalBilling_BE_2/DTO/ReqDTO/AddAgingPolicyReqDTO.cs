using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReqDTO
{
	public class AddAgingPolicyReqDTO
	{

		public int OrganizationId { get; set; }

		public int CompanyId { get; set; }

		public int DepartmentId { get; set; }

		public int PayerId { get; set; }

		public int PatientId { get; set; }

		public string? PolicyCode { get; set; }

	}
}
