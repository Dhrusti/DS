using DTO.ReqDTO;
using Helper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface
{
    public interface IPermissions
    {
        public CommonResponse GetDefaultPermissions(GetDefaultPermissionsReqDTO getDefaultPermissionsReqDTO);
        public CommonResponse UpdateDefaultPermissions(UpdateDefaultPermissionsReqDTO updateDefaultPermissionsReqDTO);

	}
}
