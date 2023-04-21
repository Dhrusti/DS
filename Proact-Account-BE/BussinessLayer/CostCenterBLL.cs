using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;
using DataLayer;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Net;
using Helpers.CommonModels;
using Helpers.CommonHelpers;
using DTO.ReqDTO;
using DTO.ResDTO;

namespace BussinessLayer
{
	public class CostCenterBLL
	{
		private readonly IMongoCollection<CostCenterMst> _costCenterCollection;
		private readonly IOptions<DatabaseSettings> _dbSettings;
		public CostCenterBLL(IOptions<DatabaseSettings> dbSettings)
		{
			_dbSettings = dbSettings;
			var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
			var mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
			_costCenterCollection = mongoDatabase.GetCollection<CostCenterMst>(dbSettings.Value.CostCenterCollectionName);
		}

		public async Task<CommonResponse> GetAllCostCenterAsync()
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				var costCenterData = await _costCenterCollection.Find(x => x.IsActive == true && x.IsDelete == false).ToListAsync();
				if (costCenterData.Count > 0)
				{
					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Data = costCenterData;
					commonResponse.Message = CommonConstant.Data_Found_Successfully;
				}
				else
				{
					commonResponse.StatusCode = HttpStatusCode.BadRequest;
					commonResponse.Message = CommonConstant.Data_Not_Found;
				}
			}
			catch { throw; }
			return commonResponse;
		}

		public async Task<CommonResponse> GetAllCostCenterByIdAsync(GetAllCostCenterByIdReqDTO request)
		{
			CommonResponse response = new CommonResponse();
			GetAllCostCenterByIdResDTO getAllCostCenterByIdResDTO = new GetAllCostCenterByIdResDTO();
			try
			{
				var costCenterData = await _costCenterCollection.Find(x => x.Id == request.Id).FirstOrDefaultAsync();
				if (costCenterData != null)
				{
					getAllCostCenterByIdResDTO.Id = costCenterData.Id;
					getAllCostCenterByIdResDTO.CostCenterMstName = costCenterData.CostCenterMstName;

					response.Data = getAllCostCenterByIdResDTO;
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

		public async Task<CommonResponse> AddCostCenterAsync(AddCostCenterReqDTO request)
		{
			CommonResponse response = new CommonResponse();
			AddCostCenterResDTO addCostCenterResDTO = new AddCostCenterResDTO();
			try
			{
				var costCenter = await _costCenterCollection.Find(x => x.CostCenterMstName.ToLower() == request.CostCenterMstName.ToLower()).FirstOrDefaultAsync();
				if (costCenter == null)
				{
					CostCenterMst costCenterMst = new CostCenterMst();
					costCenterMst.CostCenterMstName = request.CostCenterMstName;
					costCenterMst.IsActive = true;
					costCenterMst.IsDelete = false;
					costCenterMst.CreatedBy = 1;
					costCenterMst.UpdatedBy = 1;
					costCenterMst.CreatedDate = DateTime.Now; // Insert Current Time
					costCenterMst.UpdatedDate = DateTime.Now;
					await _costCenterCollection.InsertOneAsync(costCenterMst);

					response.Data = addCostCenterResDTO;
					response.Message = CommonConstant.Data_Saved_Successfully;
					response.Status = true;
					response.StatusCode = System.Net.HttpStatusCode.OK;
				}
				else
				{
					response.Message = CommonConstant.Data_Already_Exist;
					response.StatusCode = HttpStatusCode.NotFound;
				}
				addCostCenterResDTO.CostCenterMstName = request.CostCenterMstName;
			}
			catch { throw; }
			return response;
		}

		public async Task<CommonResponse> UpdateCostCenterAsync(UpdateCostCenterReqDTO request)
		{
			CommonResponse response = new CommonResponse();
			UpdateCostCenterResDTO updateCostCenterResDTO = new UpdateCostCenterResDTO();
			try
			{
				var costCenter = await _costCenterCollection.Find(x => x.Id == request.Id).FirstOrDefaultAsync();
				if (costCenter != null)
				{
					var costCenterName = await _costCenterCollection.Find(x => x.CostCenterMstName == request.CostCenterMstName).ToListAsync();
					if (costCenterName.Count <= 0)
					{
						CostCenterMst costCenterMst = new CostCenterMst();
						costCenterMst.Id = request.Id;
						costCenterMst.CostCenterMstName = request.CostCenterMstName;
						costCenterMst.IsActive = true;
						costCenterMst.IsDelete = false;
						costCenterMst.CreatedBy = 1;
						costCenterMst.UpdatedBy = 1;
						costCenterMst.CreatedDate = DateTime.Now; // Insert Current Time
						costCenterMst.UpdatedDate = DateTime.Now;
						await _costCenterCollection.ReplaceOneAsync(x => x.Id == request.Id, costCenterMst);

						updateCostCenterResDTO.Id = costCenterMst.Id;
						updateCostCenterResDTO.CostCenterMstName = costCenterMst.CostCenterMstName;

						response.Data = updateCostCenterResDTO;
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
			catch { throw; }
			return response;
		}

		public async Task<CommonResponse> DeleteCostCenterAsync(DeleteCostCenterReqDTO request)
		{
			CommonResponse response = new CommonResponse();
			DeleteCostCenterResDTO deleteCostCenterResDTO = new DeleteCostCenterResDTO();
			try
			{
				var costCenter = await _costCenterCollection.Find(x => x.Id == request.Id).FirstOrDefaultAsync();
				if (costCenter != null)
				{
					CostCenterMst costCenterMst = new CostCenterMst();
					costCenterMst.Id = request.Id;
					costCenterMst.IsActive = true;
					costCenterMst.IsDelete = true;
					costCenterMst.CreatedBy = 1;
					costCenterMst.UpdatedBy = 1;
					costCenterMst.CreatedDate = DateTime.Now; // Insert Current Time
					costCenterMst.UpdatedDate = DateTime.Now;
					await _costCenterCollection.ReplaceOneAsync(x => x.Id == request.Id, costCenterMst);

					deleteCostCenterResDTO.Id = costCenterMst.Id;

					response.Data = deleteCostCenterResDTO;
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
