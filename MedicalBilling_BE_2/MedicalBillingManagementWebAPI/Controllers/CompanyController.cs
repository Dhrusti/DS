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
    public class CompanyController : ControllerBase
    {
        private readonly ICompany _company;

        public CompanyController(ICompany company)
        {
            _company = company;
        }

        [HttpPost("AddCompany")]
        public CommonResponse AddCompany(AddComapnyReqViewModel addComapnyReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _company.AddCompany(addComapnyReqViewModel.Adapt<AddCompanyReqDTO>());
                AddCompanyResDTO model = commonResponse.Data;
                commonResponse.Data = model.Adapt<AddCompanyResViewModel>();
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
            
        }

        [HttpPost("GetDetailCompanyList")]
        public CommonResponse GetDetailCompanyList(GetDetailCompanyListReqViewModel getAllCompanyReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _company.GetDetailCompanyList(getAllCompanyReqViewModel.Adapt<GetDetailCompanyListReqDTO>());
                GetDetailCompanyListResDTO model = commonResponse.Data;
                commonResponse.Data = model.Adapt<GetDetailCompanyListResViewModel>();
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;

        }

        [HttpPost("GetCompanyDetailById")]
        public CommonResponse GetCompanyDetailById(GetCompanyDetailByIdReqViewModel getAllCompanyByIdReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _company.GetCompanyDetailById(getAllCompanyByIdReqViewModel.Adapt<GetCompanyDetailByIdReqDTO>());
                GetCompanyDetailByIdResDTO model = commonResponse.Data;
                commonResponse.Data = model.Adapt<GetCompanyDetailByIdResViewModel>();
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;

        }
        
        [HttpPost("UpdateCompany")]
        public CommonResponse UpdateCompany(UpdateCompanyReqViewModel updateCompanyReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _company.UpdateCompany(updateCompanyReqViewModel.Adapt<UpdateCompanyReqDTO>());
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;

        }

        [HttpPost("DeleteCompany")]
        public CommonResponse DeleteCompany(DeleteCompanyReqViewModel deleteCompanyReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _company.DeleteCompany(deleteCompanyReqViewModel.Adapt<DeleteCompanyReqDTO>());
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;

        }

		[HttpPost("GetCompanyList")]
		public CommonResponse GetCompanyList()
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _company.GetCompanyList();
				List<GetCompanyListResDTO> getCompanyListResDTO = commonResponse.Data;
				commonResponse.Data = getCompanyListResDTO.Adapt<List<GetCompanyListResViewModel>>();
			}
			catch { throw; }
			return commonResponse;
		}

	}
}
