using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.ReqDTO;
using Helpers.CommonModels;

namespace ServiceLayer.Interface
{
	public interface ICostCenter
	{
		public Task<CommonResponse> GetAllCostCenterAsync();

		public Task<CommonResponse> GetAllCostCenterByIdAsync(GetAllCostCenterByIdReqDTO request);

		public Task<CommonResponse> AddCostCenterAsync(AddCostCenterReqDTO request);
		public Task<CommonResponse> UpdateCostCenterAsync(UpdateCostCenterReqDTO request);
		public Task<CommonResponse> DeleteCostCenterAsync(DeleteCostCenterReqDTO request);
	}
}
