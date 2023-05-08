using DTO.ReqDTO;
using DTO.ResDTO;
using Helper.Models;
using Mapster;
using MedicalBillingManagementWebAPI.ViewModels.ReqViewModel;
using MedicalBillingManagementWebAPI.ViewModels.ResViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;

namespace MedicalBillingManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayerController : ControllerBase
    {
        private readonly IPayer _payer;

        public PayerController(IPayer payer)
        {
            _payer = payer;
        }

        [HttpPost("GetAllPayer")]
		[ApiExplorerSettings(IgnoreApi = true)]
		public CommonResponse GetAllPayer()
        { 
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _payer.GetAllPayer();
                List<GetAllPayerResDTO> model = commonResponse.Data;
                commonResponse.Data = model.Adapt<List<GetAllPayerResViewModel>>();
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        
        }
        [HttpPost("AddPayer")]
		[ApiExplorerSettings(IgnoreApi = true)]
		public CommonResponse AddPayer(AddPayerReqViewModel addPayerReqViewModel) 
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _payer.AddPayer(addPayerReqViewModel.Adapt<AddPayerReqDTO>());
            }
            catch(Exception) { throw; }
            return commonResponse;
        
        }
    }
}
