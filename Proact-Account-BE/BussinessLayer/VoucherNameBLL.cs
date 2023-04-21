using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using Azure;
using DataLayer;
using DataLayer.Entities;
using DTO.ReqDTO;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using DTO.ResDTO;
using Helpers.CommonModels;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Helpers.CommonHelpers;

namespace BussinessLayer
{
	public class VoucherNameBLL
	{
		private readonly IMongoCollection<VoucherMst> _voucherCollection;
		private readonly IOptions<DatabaseSettings> _dbSettings;
		public VoucherNameBLL(IOptions<DatabaseSettings> dbSettings)
		{
			_dbSettings = dbSettings;
			var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
			var mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
			_voucherCollection = mongoDatabase.GetCollection<VoucherMst>(dbSettings.Value.VoucherCollectionName);
		}

		public async Task<CommonResponse> GetAllVoucherNameAsync()
		{
			CommonResponse response = new CommonResponse();
			try
			{
				var voucherData = await _voucherCollection.Find(x => x.IsActive == true && x.IsDelete == false).ToListAsync();
				if (voucherData.Count > 0)
				{
					response.Status = true;
					response.Data = voucherData;
					response.Message = CommonConstant.Data_Found_Successfully;
					response.StatusCode = HttpStatusCode.OK;
				}
				else
				{
					response.Message = CommonConstant.Data_Not_Found;
					response.StatusCode = HttpStatusCode.NotFound;
				}
			}
			catch { throw; }
			return response;
		}

		public async Task<CommonResponse> GetVoucherNameByIdAsync(GetVoucherNameByIdReqDTO request)
		{
			CommonResponse response = new CommonResponse();
			GetVoucherNameByIdResDTO getVoucherByIdResDTO = new GetVoucherNameByIdResDTO();
			try
			{
				var voucherData = await _voucherCollection.Find(x => x.Id == request.Id).FirstOrDefaultAsync();
				if (voucherData != null)
				{
					getVoucherByIdResDTO.Id = voucherData.Id;
					getVoucherByIdResDTO.VoucherName = voucherData.VoucherName;

					response.Data = getVoucherByIdResDTO;
					response.Status = true;
					response.Message = CommonConstant.Data_Found_Successfully;
					response.StatusCode = HttpStatusCode.OK;
				}
				else
				{
					response.Message = CommonConstant.Data_Not_Found;
					response.StatusCode = HttpStatusCode.NotFound;
				}
			}
			catch { throw; }
			return response;
		}

		public async Task<CommonResponse> AddVoucherNameAsync(AddVoucherNameReqDTO request)
		{
			CommonResponse response = new CommonResponse();
			AddVoucherNameResDTO addVoucherResDTO = new AddVoucherNameResDTO();
			try
			{
				var vouchers = await _voucherCollection.Find(x => x.VoucherName.ToLower() == request.VoucherName.ToLower()).FirstOrDefaultAsync();
				if (vouchers == null)
				{
					VoucherMst voucherMst = new VoucherMst();
					voucherMst.VoucherName = request.VoucherName;
					voucherMst.IsActive = true;
					voucherMst.IsDelete = false;
					voucherMst.CreatedBy = 1;
					voucherMst.UpdatedBy = 1;
					voucherMst.CreatedDate = DateTime.Now; // Insert Current Time
					voucherMst.UpdatedDate = DateTime.Now;
					await _voucherCollection.InsertOneAsync(voucherMst);

					response.Data = addVoucherResDTO;
					response.Message = CommonConstant.Data_Saved_Successfully;
					response.Status = true;
					response.StatusCode = System.Net.HttpStatusCode.OK;
				}
				else
				{
					response.Message = CommonConstant.Data_Already_Exist;
					response.StatusCode = HttpStatusCode.NotFound;
				}
				addVoucherResDTO.VoucherName = request.VoucherName;
			}
			catch { throw; }
			return response;
		}

		public async Task<CommonResponse> UpdateVoucherNameAsync(UpdateVoucherNameReqDTO request)
		{
			CommonResponse response = new CommonResponse();
			UpdateVoucherNameResDTO updateVoucherNameResDTO = new UpdateVoucherNameResDTO();
			try
			{
				if (!string.IsNullOrEmpty(request.VoucherName))
				{
					var voucher = await _voucherCollection.Find(x => x.Id == request.Id).FirstOrDefaultAsync();
					if (voucher != null)
					{
						var voucherName = await _voucherCollection.Find(x => x.VoucherName == request.VoucherName).ToListAsync();
						if (voucherName.Count <= 0)
						{
							VoucherMst voucherMst = new VoucherMst();
							voucherMst.Id = request.Id;
							voucherMst.VoucherName = request.VoucherName;
							voucherMst.IsActive = true;
							voucherMst.IsDelete = false;
							voucherMst.CreatedBy = 1;
							voucherMst.UpdatedBy = 1;
							voucherMst.CreatedDate = DateTime.Now; // Insert Current Time
							voucherMst.UpdatedDate = DateTime.Now;
							await _voucherCollection.ReplaceOneAsync(x => x.Id == request.Id, voucherMst);

							updateVoucherNameResDTO.VoucherName = voucherMst.VoucherName;

							response.Data = updateVoucherNameResDTO;
							response.Status = true;
							response.Message = CommonConstant.Data_Updated_Successfully;
							response.StatusCode = HttpStatusCode.OK;
						}
						else
						{
							response.Message = CommonConstant.Data_Already_Exist;
							response.StatusCode = HttpStatusCode.BadRequest;
						}
					}
					else
					{
						response.Message = CommonConstant.Data_Not_Found;
						response.StatusCode = HttpStatusCode.NotFound;
					}
				}
			}
			catch { throw; }
			return response;
		}

		public async Task<CommonResponse> DeleteVoucherNameAsync(DeleteVoucherNameReqDTO request)
		{
			CommonResponse response = new CommonResponse();
			DeleteVoucherNameResDTO deleteVoucherResDTO = new DeleteVoucherNameResDTO();
			try
			{
				var voucher = await _voucherCollection.Find(x => x.Id == request.Id).FirstOrDefaultAsync();
				if (voucher != null)
				{
					VoucherMst voucherMst = new VoucherMst();
					voucherMst.Id = request.Id;
					voucherMst.IsActive = true;
					voucherMst.IsDelete = true;
					voucherMst.CreatedBy = 1;
					voucherMst.UpdatedBy = 1;
					voucherMst.CreatedDate = DateTime.Now; // Insert Current Time
					voucherMst.UpdatedDate = DateTime.Now;
					await _voucherCollection.ReplaceOneAsync(x => x.Id == request.Id, voucherMst);

					deleteVoucherResDTO.Id = voucherMst.Id;

					response.Data = deleteVoucherResDTO;
					response.Status = true;
					response.Message = CommonConstant.Data_Deleted_Successfully;
					response.StatusCode = HttpStatusCode.OK;
				}
				else
				{
					response.Message = CommonConstant.Data_Not_Found;
					response.StatusCode = HttpStatusCode.NotFound;
				}
			}
			catch { throw; }
			return response;
		}
	}
}
