using DTO.ReqDTO;
using Helper.Models;
using Mapster;
using MedicalBillingManagementWebAPI.ViewModels.ReqViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;

namespace MedicalBillingManagementWebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FileUploadController : ControllerBase
	{
		private readonly IFileUpload _fileUpload;

		public FileUploadController(IFileUpload fileUpload)
		{
			_fileUpload = fileUpload;
		}

		[HttpPost("UploadFileData")]
		public CommonResponse UploadFileData([FromForm] UploadFileDataReqViewModel uploadFileDataReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _fileUpload.UploadFileData(uploadFileDataReqViewModel.Adapt<UploadFileDataReqDTO>());
			}
			catch { throw; }
			return commonResponse;
		}

	}
}
