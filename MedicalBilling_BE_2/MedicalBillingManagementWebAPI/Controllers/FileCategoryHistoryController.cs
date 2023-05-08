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
	public class FileCategoryHistoryController : ControllerBase
	{
		private readonly IFileCategoryHistory _iFileCategoryHistory;
		public FileCategoryHistoryController(IFileCategoryHistory iFileCategoryHistory)
		{
			_iFileCategoryHistory = iFileCategoryHistory;
		}

		[HttpPost("UploadFileCategoryHistory")]
		public CommonResponse UploadFileCategoryHistory([FromForm] FileCategoryHistoryReqViewModel fileCategoryHistoryReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				//commonResponse = _iFileCategoryHistory.UploadFileCategoryHistory(fileCategoryHistoryReqViewModel);
				commonResponse = _iFileCategoryHistory.UploadFileCategoryHistory(fileCategoryHistoryReqViewModel.Adapt<FileCategoryHistoryReqDTO>());
			}
			catch { throw; }
			return commonResponse;
		}

		[HttpPost("GetAllFileCategoryHistory")]
		public CommonResponse GetAllFileCategoryHistory()
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _iFileCategoryHistory.GetAllFileCategoryHistory();
				List<GetAllFileCategoryHistoryResDTO> getAllFileCategoryHistoryResDTO = commonResponse.Data;
				commonResponse.Data = getAllFileCategoryHistoryResDTO.Adapt<List<GetAllFileCategoryHistoryResViewModel>>();
			}
			catch { throw; }
			return commonResponse;
		}

		[HttpPost("GetFileCategoryHistoryById")]
		public CommonResponse GetFileCategoryHistoryById(GetFileCategoryHistoryByIdReqViewModel getAllFileCategoryHistoryReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _iFileCategoryHistory.GetFileCategoryHistoryById(getAllFileCategoryHistoryReqViewModel.Adapt<GetFileCategoryHistoryByIdReqDTO>());
				GetFileCategoryHistoryByIdResDTO getAllFileCategoryHistoryResDTO = commonResponse.Data;
				commonResponse.Data = getAllFileCategoryHistoryResDTO.Adapt<GetFileCategoryHistoryByIdResViewModel>();
			}
			catch { throw; }
			return commonResponse;
		}

		[HttpPost("UpdateFileCategoryHistory")]
		public CommonResponse UpdateFileCategoryHistory([FromForm]UpdateFileCategoryHistoryReqViewModel updateFileCategoryHistoryReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _iFileCategoryHistory.UpdateFileCategoryHistory(updateFileCategoryHistoryReqViewModel.Adapt<UpdateFileCategoryHistoryReqDTO>());
				UpdateFileCategoryHistoryResDTO updateFileCategoryHistoryResDTO = commonResponse.Data;
				commonResponse.Data = updateFileCategoryHistoryResDTO.Adapt<UpdateFileCategoryHistoryResViewModel>();
			}
			catch { throw; }
			return commonResponse;
		}

		[HttpPost("DeleteFileCategoryHistory")]
		public CommonResponse DeleteFileCategoryHistory(DeleteFileCategoryHistoryReqViewModel deleteFileCategoryHistoryReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _iFileCategoryHistory.DeleteFileCategoryHistory(deleteFileCategoryHistoryReqViewModel.Adapt<DeleteFileCategoryHistoryReqDTO>());
				DeleteFileCategoryHistoryResDTO deleteFileCategoryHistoryResDTO = commonResponse.Data;
				commonResponse.Data = deleteFileCategoryHistoryResDTO.Adapt<DeleteFileCategoryHistoryResViewModel>();
			}
			catch { throw; }
			return commonResponse;
		}
	}
}
