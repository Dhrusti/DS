using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResDTO
{
	public class GetFileCategoryHistoryByIdResDTO
	{
		public string FileCategoryName { get; set; } = null!;

		public int OrganizationId { get; set; }

		public int CompanyId { get; set; }

		public int DepartmentId { get; set; }

		public string FileFormatPath { get; set; } = null!;
	}
}
