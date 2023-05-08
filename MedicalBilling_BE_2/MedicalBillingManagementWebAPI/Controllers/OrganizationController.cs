using DocumentFormat.OpenXml.ExtendedProperties;
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
	public class OrganizationController : ControllerBase
	{
		private readonly IOrganization _organization;

		public OrganizationController(IOrganization organization)
		{
			_organization = organization;
		}

		[HttpPost("AddOrganization")]
		public CommonResponse AddOrganization(AddOrganizationReqViewModel addOrganizationReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _organization.AddOrganization(addOrganizationReqViewModel.Adapt<AddOrganizationReqDTO>());
			}
			catch (Exception) { throw; }
			return commonResponse;
		}

		[HttpPost("GetDetailOrganizationList")]
		public CommonResponse GetDetailOrganizationList(GetDetailOrganizationListReqViewModel getDetailOrganizationListReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _organization.GetDetailOrganizationList(getDetailOrganizationListReqViewModel.Adapt<GetDetailOrganizationListReqDTO>());
				GetDetailOrganizationListResDTO data = commonResponse.Data;
				commonResponse.Data = data.Adapt<GetDetailOrganizationListResViewModel>();
			}
			catch (Exception) { throw; }
			return commonResponse;
		}

		[HttpPost("GetOrganizationDetailById")]
		public CommonResponse GetOrganizationDetailById(GetOrganizationDetailByIdReqViewModel getOrganizationDetailByIdReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _organization.GetOrganizationDetailById(getOrganizationDetailByIdReqViewModel.Adapt<GetOrganizationDetailByIdReqDTO>());
				GetOrganizationDetailByIdResDTO data = commonResponse.Data;
				commonResponse.Data = data.Adapt<GetOrganizationDetailByIdResViewModel>();
			}
			catch (Exception) { throw; }
			return commonResponse;
		}

		[HttpPost("UpdateOrganization")]
		public CommonResponse UpdateOrganization(UpdateOrganizationReqViewModel updateOrganizationReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _organization.UpdateOrganization(updateOrganizationReqViewModel.Adapt<UpdateOrganizationReqDTO>());
			}
			catch { throw; }
			return commonResponse;
		}

		[HttpPost("DeleteOrganization")]
		public CommonResponse DeleteOrganization(DeleteOrganizationReqViewModel deleteOrganizationReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _organization.DeleteOrganization(deleteOrganizationReqViewModel.Adapt<DeleteOrganizationReqDTO>());
			}
			catch { throw; }
			return commonResponse;
		}

		[HttpPost("GetOrganizationList")]
		public CommonResponse GetOrganizationList()
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _organization.GetOrganizationList();
				List<GetOrganizationListResDTO> getOrganizationListResDTO = commonResponse.Data;
				commonResponse.Data = getOrganizationListResDTO.Adapt<List<GetOrganizationListResViewModel>>();
			}
			catch { throw; }
			return commonResponse;
		}

	}
}
