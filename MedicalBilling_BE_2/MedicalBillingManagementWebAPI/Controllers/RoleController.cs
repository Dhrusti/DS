using DTO.ResDTO;
using Helper.Models;
using Mapster;
using MedicalBillingManagementWebAPI.ViewModels.ResViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;

namespace MedicalBillingManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRole _role;

        public RoleController(IRole role)
        {
            _role = role;
        }

        [HttpGet("GetRoles")]
        public CommonResponse GetRoles()
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _role.GetRoles();
                List<GetRolesResDTO> model = commonResponse.Data ?? new List<GetRolesResDTO>();
                commonResponse.Data = model.Adapt<List<GetRolesResViewModels>>();
            }
            catch { throw; }
            return commonResponse;
        }
    }
}
