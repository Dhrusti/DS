using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReqDTO
{
	public class UpdateFileCategoryHistoryReqDTO
	{
		public int Id { get; set; }

		public string FileCategoryName { get; set; } = null!;

		public int OrganizationId { get; set; }

		public int CompanyId { get; set; }

		public int DepartmentId { get; set; }

		public dynamic File { get; set; } = null!;
	}
}
