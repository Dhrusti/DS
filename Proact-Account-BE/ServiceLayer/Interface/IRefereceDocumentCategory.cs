using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.ReqDTO;
using Helpers.CommonModels;

namespace ServiceLayer.Interface
{
	public interface IRefereceDocumentCategory
	{
		public Task<CommonResponse> AddReferenceDocumentCategoryAsync(AddRefereceDocumentCategoryReqDTO request);
	}
}
