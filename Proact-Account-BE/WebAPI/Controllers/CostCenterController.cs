using Azure.Core;
using Azure;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helpers.CommonModels;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;
using WebAPI.ViewModels.ReqViewModels;
using WebAPI.ViewModels.ResViewModels;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CostCenterController : ControllerBase
	{
		private readonly ICostCenter _iCostCenter;
        public CostCenterController(ICostCenter iCostCenter)
        {
			_iCostCenter = iCostCenter;
        }

		[HttpPost("GetAllCostCenterAsync")]
		public async Task<CommonResponse> GetAllCostCenter()
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = await _iCostCenter.GetAllCostCenterAsync();
			}
			catch { throw; }
			return commonResponse;
		}

		[HttpPost("GetAllCostCenterByIdAsync")]
		public async Task<CommonResponse> GetAllCostCenterByIdAsync(GetAllCostCenterByIdReqViewModel request)
		{
			CommonResponse response = new CommonResponse();
			try
			{
				response = await _iCostCenter.GetAllCostCenterByIdAsync(request.Adapt<GetAllCostCenterByIdReqDTO>());
				GetAllCostCenterByIdResDTO getAllCostCenterByIdResDTO = response.Data;
				response.Data = getAllCostCenterByIdResDTO.Adapt<GetAllCostCenterByIdResViewModel>();
			}
			catch { throw; }
			return response;
		}

		[HttpPost("AddCostCenterAsync")]
		public async Task<CommonResponse> AddCostCenterAsync(AddCostCenterReqViewModel request)
		{
			CommonResponse response = new CommonResponse();
			try
			{
				response = await _iCostCenter.AddCostCenterAsync(request.Adapt<AddCostCenterReqDTO>());
				AddCostCenterResDTO addCostCenterResDTO = response.Data;
				response.Data = addCostCenterResDTO.Adapt<AddCostCenterResViewModel>();
			}
			catch { throw; }
			return response;
		}

		[HttpPost("UpdateCostCenterAsync")]
		public async Task<CommonResponse> UpdateCostCenterAsync(UpdateCostCenterReqViewModel request)
		{
			CommonResponse response = new CommonResponse();
			try
			{
				response = await _iCostCenter.UpdateCostCenterAsync(request.Adapt<UpdateCostCenterReqDTO>());
				UpdateCostCenterResDTO updateCostCenterResDTO = response.Data;
				response.Data = updateCostCenterResDTO.Adapt<UpdateCostCenterResViewModel>();
			}
			catch { throw; }
			return response;
		}

		[HttpPost("DeleteCostCenterAsync")]
		public async Task<CommonResponse> DeleteCostCenterAsync(DeleteCostCenterReqViewmodel request)
		{
			CommonResponse response = new CommonResponse();
			try
			{
				response = await _iCostCenter.DeleteCostCenterAsync(request.Adapt<DeleteCostCenterReqDTO>());
				DeleteCostCenterResDTO deleteCostCenterResDTO = response.Data;
				response.Data = deleteCostCenterResDTO.Adapt<DeleteCostCenterResViewModel>();
			}
			catch { throw; }
			return response;
		}
	}
}
