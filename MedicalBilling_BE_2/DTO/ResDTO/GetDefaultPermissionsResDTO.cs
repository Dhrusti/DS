using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResDTO
{
    public class GetDefaultPermissionsResDTO
    {
        public List<GetDefaultPermission> GetDefaultPermissionList { get; set; }
        public int TotalCount { get; set; }
    }

    public class GetDefaultPermission
    {
		public int PermissionId { get; set; }
		public string PermissionName { get; set; }
		public string PermissionCode { get; set; }
		public bool IsDefault { get; set; }
	}
}
