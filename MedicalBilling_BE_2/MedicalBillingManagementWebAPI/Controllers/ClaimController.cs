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
    public class ClaimController : ControllerBase
    {
        private readonly IClaim _claim;

        public ClaimController(IClaim claim)
        { 
            _claim = claim;
        }

        [HttpPost("GetAllClaim")]
        public CommonResponse GetAllClaim(GetAllClaimReqViewModel getAllClaimReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _claim.GetAllClaim(getAllClaimReqViewModel.Adapt<GetAllClaimReqDTO>());
                GetAllClaimResDTO model = commonResponse.Data;
                commonResponse.Data = model.Adapt<GetAllClaimResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        //[HttpPost("AddClaim")]

        //public CommonResponse AddClaim(AddClaimReqViewModel addClaimReqViewModel)
        //{ 
        //    CommonResponse commonResponse = new CommonResponse();
        //    try
        //    {
        //        commonResponse = _claim.AddClaim(addClaimReqViewModel.Adapt<AddClaimReqDTO>());
        //        AddClaimResDTO model = commonResponse.Data;
        //        commonResponse.Data = model.Adapt<AddClaimResDTO>();
              
        //    }
        //    catch (Exception) { throw; }
        //    return commonResponse;
            
        //}
       
    }
}
