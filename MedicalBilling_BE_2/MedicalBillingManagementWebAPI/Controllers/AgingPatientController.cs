using DTO.ReqDTO;
using DTO.ResDTO;
using Helper.Models;
using Mapster;
using MedicalBillingManagementWebAPI.ViewModels.ReqViewModel;
using MedicalBillingManagementWebAPI.ViewModels.ResViewModel;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;

namespace MedicalBillingManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgingPatientController : ControllerBase
    {
        private readonly IAgingPatient _iAgingPatient;

        public AgingPatientController(IAgingPatient iAgingPatient)
        {
            _iAgingPatient = iAgingPatient;
        }

        [HttpPost("AddAgingPatient")]
        public CommonResponse AddAgingPatient(AddAgingPatientReqViewModel addAgingPatientReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iAgingPatient.AddAgingPatient(addAgingPatientReqViewModel.Adapt<AddAgingPatientReqDTO>());
                AddAgingPatientResDTO model = commonResponse.Data;
                commonResponse.Data = model.Adapt<AddAgingPatientResViewModel>();
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;

        }
    }
}
