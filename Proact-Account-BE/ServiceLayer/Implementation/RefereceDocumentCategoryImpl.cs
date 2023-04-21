using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessLayer;
using DTO.ReqDTO;
using Helpers.CommonModels;
using ServiceLayer.Interface;

namespace ServiceLayer.Implementation
{
	public class RefereceDocumentCategoryImpl : IRefereceDocumentCategory
	{
		private readonly RefereceDocumentCategoryBLL _refereceDocumentCategoryBLL;
		public RefereceDocumentCategoryImpl(RefereceDocumentCategoryBLL refereceDocumentCategoryBLL)
		{
			_refereceDocumentCategoryBLL = refereceDocumentCategoryBLL;
		}

		public async Task<CommonResponse> AddReferenceDocumentCategoryAsync(AddRefereceDocumentCategoryReqDTO request) => await _refereceDocumentCategoryBLL.AddReferenceDocumentCategoryAsync(request);

	}
}
