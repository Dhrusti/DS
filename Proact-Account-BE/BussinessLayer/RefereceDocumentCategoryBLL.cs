using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helpers.CommonHelpers;
using Helpers.CommonModels;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BussinessLayer
{
	public class RefereceDocumentCategoryBLL
	{
		private readonly IMongoCollection<RefereceDocumentCategoryMst> _refereceDocumentCategoryMstCollection;
		private readonly IOptions<DatabaseSettings> _dbSettings;
		public RefereceDocumentCategoryBLL(IOptions<DatabaseSettings> dbSettings)
		{
			_dbSettings = dbSettings;
			var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
			var mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
			_refereceDocumentCategoryMstCollection = mongoDatabase.GetCollection<RefereceDocumentCategoryMst>(dbSettings.Value.RefereceDocumentCategoryName);
		}

		public async Task<CommonResponse> AddReferenceDocumentCategoryAsync(AddRefereceDocumentCategoryReqDTO request)
		{
			CommonResponse response = new CommonResponse();
			AddRefereceDocumentCategoryResDTO addRefereceDocumentCategoryRes = new AddRefereceDocumentCategoryResDTO();
			try
			{
				var referenceDocument = await _refereceDocumentCategoryMstCollection.Find(x => x.RefDocCatName.ToLower() == request.RefDocCatName.ToLower()).FirstOrDefaultAsync();
				if (referenceDocument == null)
				{
					RefereceDocumentCategoryMst refereceDocumentCategoryMst = new RefereceDocumentCategoryMst();
					refereceDocumentCategoryMst.RefDocCatName = request.RefDocCatName;
					refereceDocumentCategoryMst.IsActive = true;
					refereceDocumentCategoryMst.IsDeleted = false;
					refereceDocumentCategoryMst.CreatedBy = 1;
					refereceDocumentCategoryMst.UpdatedBy = 1;
					refereceDocumentCategoryMst.CreatedDate = DateTime.Now; // Insert Current Time
					refereceDocumentCategoryMst.UpdatedDate = DateTime.Now;
					await _refereceDocumentCategoryMstCollection.InsertOneAsync(refereceDocumentCategoryMst);

					response.Data = addRefereceDocumentCategoryRes;
					response.Message = CommonConstant.Data_Saved_Successfully;
					response.Status = true;
					response.StatusCode = System.Net.HttpStatusCode.OK;
				}
				else
				{
					response.Message = CommonConstant.Data_Already_Exist;
					response.StatusCode = HttpStatusCode.NotFound;
				}
				addRefereceDocumentCategoryRes.RefDocCatName = request.RefDocCatName;
			}
			catch { throw; }
			return response;
		}
	}
}
