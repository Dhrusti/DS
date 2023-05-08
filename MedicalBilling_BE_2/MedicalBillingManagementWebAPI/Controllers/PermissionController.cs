using DTO.ReqDTO;
using DTO.ResDTO;
using Helper.Models;
using Mapster;
using MedicalBillingManagementWebAPI.ViewModels.ReqViewModel;
using MedicalBillingManagementWebAPI.ViewModels.ResViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;
using System.Security;

namespace MedicalBillingManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissions _permissions;
        public PermissionController(IPermissions permissions)
        {
            _permissions = permissions;
        }

        [HttpPost("GetDefaultPermissions")]
        public CommonResponse GetDefaultPermissions(GetDefaultPermissionsReqViewModel getDefaultPermissionsReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _permissions.GetDefaultPermissions(getDefaultPermissionsReqViewModel.Adapt<GetDefaultPermissionsReqDTO>());
            }
            catch { throw; }
            return commonResponse;
        }

        [HttpPost("UpdateDefaultPermissions")]
		public CommonResponse UpdateDefaultPermissions(UpdateDefaultPermissionsReqViewModel updateDefaultPermissionsReqViewModel)
        {
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _permissions.UpdateDefaultPermissions(updateDefaultPermissionsReqViewModel.Adapt<UpdateDefaultPermissionsReqDTO>());
			}
			catch { throw; }
			return commonResponse;
		}

	}
}
