using DataLayer.Entities;
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
	public class VoucherNameController : ControllerBase
	{
		private readonly IVoucherName _iVoucher;
		public VoucherNameController(IVoucherName iVoucher)
		{
			_iVoucher = iVoucher;
		}

		[HttpPost("GetAllVoucherNameAsync")]
		public async Task<CommonResponse> GetAllVoucherNameAsync()
		{
			CommonResponse response = new CommonResponse();
			try
			{
				response = await _iVoucher.GetAllVoucherNameAsync();
			}
			catch { throw; }
			return response;
		}

		[HttpPost("GetVoucherNameByIdAsync")]
		public async Task<CommonResponse> GetVoucherNameByIdAsync(GetVoucherNameByIdReqViewModel request)
		{
			CommonResponse response = new CommonResponse();
			try
			{
				response = await _iVoucher.GetVoucherNameByIdAsync(request.Adapt<GetVoucherNameByIdReqDTO>());
				GetVoucherNameByIdResDTO getVoucherByIdRes = response.Data;
				response.Data = getVoucherByIdRes.Adapt<GetVoucherNameByIdResViewModel>();
			}
			catch { throw; }
			return response;
		}

		[HttpPost("AddVoucherNameAsync")]
		public async Task<CommonResponse> AddVoucherNameAsync([FromBody] AddVoucherNameReqViewModel request)
		{
			CommonResponse response = new CommonResponse();
			try
			{
				response = await _iVoucher.AddVoucherNameAsync(request.Adapt<AddVoucherNameReqDTO>());
				AddVoucherNameResDTO addVoucherResDTO = response.Data;
				response.Data = addVoucherResDTO.Adapt<AddVoucherNameResViewModel>();
			}
			catch { throw; }
			return response;
		}

		[HttpPost("UpdateVoucherNameAsync")]
		public async Task<CommonResponse> UpdateVoucherNameAsync(UpdateVoucherNameReqViewModel request)
		{
			CommonResponse response = new CommonResponse();
			try
			{
				response = await _iVoucher.UpdateVoucherNameAsync(request.Adapt<UpdateVoucherNameReqDTO>());
				UpdateVoucherNameResDTO updateVoucherNameResDTO = response.Data;
				response.Data = updateVoucherNameResDTO.Adapt<UpdateVoucherNameResViewModel>();
			}
			catch { throw; }
			return response;
		}

		[HttpPost("DeleteVoucherNameAsync")]
		public async Task<CommonResponse> DeleteVoucherNameAsync(DeleteVoucherNameReqViewModel request)
		{
			CommonResponse response = new CommonResponse();
			try
			{
				response = await _iVoucher.DeleteVoucherNameAsync(request.Adapt<DeleteVoucherNameReqDTO>());
				DeleteVoucherNameResDTO deleteVoucherResDTO = response.Data;
				response.Data = deleteVoucherResDTO.Adapt<DeleteVoucherNameResViewModel>();
			}
			catch { throw; }
			return response;
		}
	}
}
