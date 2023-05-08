using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReqDTO
{
	public class UpdateDefaultPermissionsReqDTO
	{
		public int RoleId { get; set; }
		public List<DefaultPermissionModel> DefaultPermissionList { get; set; }
	}

	public class DefaultPermissionModel
	{
		public int PermissionId { get; set; }
		public string PermissionName { get; set; }
		public string PermissionCode { get; set; }
		public bool IsDefault { get; set; } = false;
	}
}
