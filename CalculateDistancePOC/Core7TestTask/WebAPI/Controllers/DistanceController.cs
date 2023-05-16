using DTO.ReqDTO;
using Helper;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;
using WebAPI.ViewModels.ReqViewModels;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DistanceController : ControllerBase
	{
		private readonly IDistance _idistance;
		public DistanceController(IDistance idistance)
		{
			_idistance = idistance;
		}

		[HttpPost("GetDistanceByZipCodes")]
		public CommonResponse GetDistanceByZipCodes(GetDistanceByZipCodesReqViewModel getDistanceByZipCodesReqViewModel)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				commonResponse = _idistance.GetDistanceByZipCodes(getDistanceByZipCodesReqViewModel.Adapt<GetDistanceByZipCodesReqDTO>());
			}
			catch { throw; }
			return commonResponse;
		}

	}
}
