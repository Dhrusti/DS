using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DTO.ReqDTO
{
	public class FileCategoryHistoryReqDTO
	{
		public string FileCategoryName { get; set; }

		public int OrganizationId { get; set; }

		public int CompanyId { get; set; }

		public int DepartmentId { get; set; }

		public dynamic File { get; set; } 
	}
}
