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
    public class ClaimStatusController : ControllerBase
    {
        private readonly IClaimStatus _claimStatus;

        public ClaimStatusController(IClaimStatus claimStatus)
        { 
            _claimStatus = claimStatus;
        }

        [HttpPost("AddClaimStatus")]
        public CommonResponse AddClaimStatus(AddClaimStatusReqViewModel addClaimStatusReqViewModel)
        { 
            CommonResponse commonResponse = new CommonResponse();
            try 
            {
                commonResponse = _claimStatus.AddClaimStatus(addClaimStatusReqViewModel.Adapt<AddClaimStatusReqDTO>());
            }
            catch (Exception) { throw; }
            return commonResponse;
        }
        [HttpPost("GetAllClaimStatus")]
        public CommonResponse GetAllClaimStatus(GetAllClaimStatusReqViewModel getAllClaimStatusViewModel)
        { 
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _claimStatus.GetAllClaimStatus(getAllClaimStatusViewModel.Adapt<GetAllClaimStatusReqDTO>());
                GetAllClaimStatusResDTO model = commonResponse.Data;
                commonResponse.Data = model.Adapt<GetAllClaimStatusResViewModel>();    


            }
            catch (Exception) { throw; }
            return commonResponse;
            
        }
        [HttpPost("GetAllClaimStatusById")]
        public CommonResponse GetAllClaimStatusById(GetAllClaimStatusByIdReqViewModel getAllClaimStatusByIdReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _claimStatus.GetAllClaimStatusById(getAllClaimStatusByIdReqViewModel.Adapt<GetAllClaimStatusByIdReqDTO>());
                GetAllClaimStatusByIdResDTO model = commonResponse.Data;
                commonResponse.Data = model.Adapt<GetAllClaimStatusByIdResViewModel>();

            }
            catch (Exception) { throw; }
            return commonResponse;
            
        }

        //[HttpPost("UpdateClaimStatus")]
        //public CommonResponse UpdateClaimStatus(UpdateClaimStatusReqViewModel updateClaimStatusReqviewModel)
        //{
        //    CommonResponse commonResponse = new CommonResponse();
        //    try
        //    {
        //        commonResponse = _claimStatus.UpdateClaimStatus(updateClaimStatusReqviewModel.Adapt<UpdateClaimStatusReqDTO>());
        //        UpdateClaimStatusResDTO model = commonResponse.Data;
        //        commonResponse.Data = model.Adapt<UpdateClaimStatusResViewModel>();
        //    }
        //    catch (Exception) { throw; }
        //    return commonResponse;
        //}

        //[HttpPost("DeleteClaimStatus")]
        //public CommonResponse DeleteClaimStatus(DeleteClaimStatusReqViewModel deleteClaimStatusReqViewModel)
        //{
        //    CommonResponse commonResponse = new CommonResponse();
        //    try
        //    {
        //        commonResponse = _claimStatus.DeleteClaimStatus(deleteClaimStatusReqViewModel.Adapt<DeleteClaimStatusReqDTO>());
        //        DeleteClaimStatusResDTO model = commonResponse.Data;
        //        commonResponse.Data = model.Adapt<DeleteClaimStatusResViewModel>();
        //    }
        //    catch(Exception) { throw; }
        //    return commonResponse;
        //}

    }
}
