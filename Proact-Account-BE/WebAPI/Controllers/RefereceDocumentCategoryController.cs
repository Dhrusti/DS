using DTO.ReqDTO;
using DTO.ResDTO;
using Helpers.CommonModels;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;
using WebAPI.ViewModels.ReqViewModels;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RefereceDocumentCategoryController : ControllerBase
	{
		private readonly IRefereceDocumentCategory _iIRefereceDocumentCategory;
		public RefereceDocumentCategoryController(IRefereceDocumentCategory iIRefereceDocumentCategory)
		{
			_iIRefereceDocumentCategory = iIRefereceDocumentCategory;
		}

		[HttpPost("AddReferenceDocumentCategoryAsync")]
		public async Task<CommonResponse> AddReferenceDocumentCategoryAsync(AddRefereceDocumentCategoryReqViewModel request)
		{
			CommonResponse response = new CommonResponse();
			try
			{
				response = await _iIRefereceDocumentCategory.AddReferenceDocumentCategoryAsync(request.Adapt<AddRefereceDocumentCategoryReqDTO>());
				AddRefereceDocumentCategoryResDTO addRefereceDocumentCategoryResDTO = response.Data;
				response.Data = request;
			}
			catch { throw; }
			return response;
		}
	}
}
