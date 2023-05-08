using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResDTO
{
	public class GetOrganizationListResDTO
	{
		public int Id { get; set; }
		public string OrganizationName { get; set; } = null!;
	}
}
