using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;
using DTO.ReqDTO;
using Helpers.CommonModels;

namespace ServiceLayer.Interface
{
	public interface IVoucherName
	{
		public Task<CommonResponse> GetAllVoucherNameAsync();

		public Task<CommonResponse> GetVoucherNameByIdAsync(GetVoucherNameByIdReqDTO request);

		public Task<CommonResponse> AddVoucherNameAsync(AddVoucherNameReqDTO request);

		public Task<CommonResponse> UpdateVoucherNameAsync(UpdateVoucherNameReqDTO request);

		public Task<CommonResponse> DeleteVoucherNameAsync(DeleteVoucherNameReqDTO request);
	}
}
