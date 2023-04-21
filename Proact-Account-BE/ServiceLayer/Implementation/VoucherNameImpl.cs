using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using BussinessLayer;
using DataLayer.Entities;
using DTO.ReqDTO;
using Helpers.CommonModels;
using ServiceLayer.Interface;

namespace ServiceLayer.Implementation
{
	public class VoucherNameImpl : IVoucherName
	{
		private readonly VoucherNameBLL _voucherBLL;
		public VoucherNameImpl(VoucherNameBLL voucherBLL)
		{
			_voucherBLL = voucherBLL;
		}

		public async Task<CommonResponse> GetAllVoucherNameAsync() => await _voucherBLL.GetAllVoucherNameAsync();

		public async Task<CommonResponse> GetVoucherNameByIdAsync(GetVoucherNameByIdReqDTO request) => await _voucherBLL.GetVoucherNameByIdAsync(request);
		
		public async Task<CommonResponse> AddVoucherNameAsync(AddVoucherNameReqDTO request) => await _voucherBLL.AddVoucherNameAsync(request);

		public async Task<CommonResponse> UpdateVoucherNameAsync(UpdateVoucherNameReqDTO request) => await _voucherBLL.UpdateVoucherNameAsync(request);

		public async Task<CommonResponse> DeleteVoucherNameAsync(DeleteVoucherNameReqDTO request) => await _voucherBLL.DeleteVoucherNameAsync(request);
	}
}
