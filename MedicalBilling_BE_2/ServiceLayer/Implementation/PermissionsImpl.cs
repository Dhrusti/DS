using BussinessLayer;
using DTO.ReqDTO;
using Helper.Models;
using ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Implementation
{
    public class PermissionsImpl : IPermissions
    {
        private readonly PermissionBLL _permissionBLL;

        public PermissionsImpl(PermissionBLL permissionBLL)
        {
            _permissionBLL = permissionBLL;
        }

        public CommonResponse GetDefaultPermissions(GetDefaultPermissionsReqDTO getDefaultPermissionsReqDTO)
        {
            return _permissionBLL.GetDefaultPermissions(getDefaultPermissionsReqDTO);
        }

		public CommonResponse UpdateDefaultPermissions(UpdateDefaultPermissionsReqDTO updateDefaultPermissionsReqDTO)
        {
            return _permissionBLL.UpdateDefaultPermissions(updateDefaultPermissionsReqDTO);
        }

	}
}
