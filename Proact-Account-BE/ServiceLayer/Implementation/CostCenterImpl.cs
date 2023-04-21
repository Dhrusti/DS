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
	public class CostCenterImpl : ICostCenter
	{
		private readonly CostCenterBLL _costCenterBLL;
        public CostCenterImpl(CostCenterBLL costCenterBLL)
        {
            _costCenterBLL = costCenterBLL;
        }

		public async Task<CommonResponse> GetAllCostCenterAsync() => await _costCenterBLL.GetAllCostCenterAsync();

		public async Task<CommonResponse> GetAllCostCenterByIdAsync(GetAllCostCenterByIdReqDTO request)=> await _costCenterBLL.GetAllCostCenterByIdAsync(request);
		
		public async Task<CommonResponse> AddCostCenterAsync(AddCostCenterReqDTO request) => await _costCenterBLL.AddCostCenterAsync(request);

		public async Task<CommonResponse> UpdateCostCenterAsync(UpdateCostCenterReqDTO request)	=> await _costCenterBLL.UpdateCostCenterAsync(request);

		public async Task<CommonResponse> DeleteCostCenterAsync(DeleteCostCenterReqDTO request) => await _costCenterBLL.DeleteCostCenterAsync(request);
	}
}
