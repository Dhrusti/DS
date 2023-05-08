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
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartment _department;

        public DepartmentController(IDepartment department)
        { 
            _department = department;
        }
        [HttpPost("AddDepartment")]
        public CommonResponse AddDepartment(AddDepartmentReqViewModel addDepartmentReqViewModel)
        { 
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _department.AddDepartment(addDepartmentReqViewModel.Adapt<AddDepartmentReqDTO>());
                AddDepartmentResDTO model = commonResponse.Data;
                commonResponse.Data = model.Adapt<AddDepartmentResViewModel>();
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }
        [HttpPost("GetDetailDepartmentList")]
        public CommonResponse GetDetailDepartmentList(GetDetailDepartmentListReqViewModel getAllDepartmentReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _department.GetDetailDepartmentList(getAllDepartmentReqViewModel.Adapt<GetDetailDepartmentListReqDTO>());
                GetDetailDepartmentListResDTO model = commonResponse.Data;
                commonResponse.Data = model.Adapt<GetDetailDepartmentListResViewModel>();
                
            }
            catch(Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("GetDepartmentDetailsById")]
        public CommonResponse GetDepartmentDetailsById(GetDepartmentDetailsByIdReqViewModel getAllDepartmentbyidReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _department.GetDepartmentDetailsById(getAllDepartmentbyidReqViewModel.Adapt<GetDepartmentDetailsByIdReqDTO>());
                GetDepartmentDetailsByIdResDTO model = commonResponse.Data;
                commonResponse.Data = model.Adapt<GetDepartmentDetailsByIdResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("UpdateDepartment")]
        public CommonResponse UpdateDepartment(UpdateDepartmentReqViewModel updateDepartmentReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _department.UpdateDepartment(updateDepartmentReqViewModel.Adapt<UpdateDepartmentReqDTO>());

            }
            catch (Exception) { throw; }
            return commonResponse;
        }
        
        [HttpPost("DeleteDepartment")]
        public CommonResponse DeleteDepartment(DeleteDepartmentReqViewModel deleteDepartmentReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _department.DeleteDepartment(deleteDepartmentReqViewModel.Adapt<DeleteDepartmentReqDTO>());

            }
            catch (Exception) { throw; }
            return commonResponse;
        }

		[HttpPost("GetDepartmentList")]
		public CommonResponse GetDepartmentList()
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _department.GetDepartmentList();
				List<GetDepartmentListResDTO> getDepartmentListResDTO = commonResponse.Data;
				commonResponse.Data = getDepartmentListResDTO.Adapt<List<GetDepartmentListResViewModel>>();
			}
			catch { throw; }
			return commonResponse;
		}
	}
}
