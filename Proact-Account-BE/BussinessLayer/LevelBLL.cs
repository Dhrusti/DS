using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helpers.CommonHelpers;
using Helpers.CommonModels;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BussinessLayer
{
    public class LevelBLL
    {
        private readonly IConfiguration _configuration;
        private readonly MongoClient _db;
        private readonly CommonHelper _commonHelper;
        private readonly IMongoCollection<FirstLevelMst> _firstCollection;
        private readonly IMongoCollection<AllLevelMst> _secondCollection;
        private readonly IMongoCollection<AllLevelMst> _thirdCollection;
        private readonly IMongoCollection<AllLevelMst> _forthCollection;
        private readonly IMongoCollection<AllLevelMst> _fifthCollection;
        public LevelBLL(IConfiguration configuration, CommonHelper commonHelper)
        {
            _configuration = configuration;
            _db = new MongoClient(_configuration["DatabaseConnection:ConnectionString"]);
            var MongoDataBase = _db.GetDatabase(_configuration["DatabaseConnection:DatabaseName"]);
            _firstCollection = MongoDataBase.GetCollection<FirstLevelMst>(_configuration["DatabaseConnection:CollectionNameFirst"]);
            _secondCollection = MongoDataBase.GetCollection<AllLevelMst>(_configuration["DatabaseConnection:CollectionNameSecond"]);
            _thirdCollection = MongoDataBase.GetCollection<AllLevelMst>(_configuration["DatabaseConnection:CollectionNameThird"]);
            _forthCollection = MongoDataBase.GetCollection<AllLevelMst>(_configuration["DatabaseConnection:CollectionNameForth"]);
            _fifthCollection = MongoDataBase.GetCollection<AllLevelMst>(_configuration["DatabaseConnection:CollectionNameFifth"]);
            _commonHelper = commonHelper;
        }

        public async Task<CommonResponse> AddLevelFirstAsync(AddLevelFirstReqDTO request)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                AddLevelFirstResDTO addLevelFirstResDTO = new AddLevelFirstResDTO();
                FirstLevelMst firstLevelMst = new FirstLevelMst();
                firstLevelMst.Code = request.Code;
                firstLevelMst.Name = request.Name;
                firstLevelMst.IsActive = true;
                firstLevelMst.IsDeleted = false;
                firstLevelMst.CreatedBy = 1;
                firstLevelMst.UpdatedBy = 1;
                firstLevelMst.CreatedDate = DateTime.Now; // Insert Current Time
                firstLevelMst.UpdatedDate = DateTime.Now;
                await _firstCollection.InsertOneAsync(firstLevelMst);

                addLevelFirstResDTO.LevelId = firstLevelMst.LevelFirstId;

                if (addLevelFirstResDTO != null)
                {
                    response.Data = addLevelFirstResDTO;
                    response.Message = CommonConstant.Data_Saved_Successfully;
                    response.Status = true;
                    response.StatusCode = System.Net.HttpStatusCode.OK;
                }

            }
            catch { throw; }
            return response;
        }
        public async Task<CommonResponse> AddAllLevelAsync(AddAllLevelReqDTO request)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                AddAllLevelResDTO addLevelsecondResDTO = new AddAllLevelResDTO();
                AllLevelMst addAllLevelMst = new AllLevelMst();
                if (request.LevelId == 2)
                {
                    addAllLevelMst.Code = request.Code;
                    addAllLevelMst.Name = request.Name;
                    addAllLevelMst.ParentLevelTypeId = request.ParentLevelTypeId;
                    //addAllLevelMst.IsFinalLevel  = request.IsFinalLevel;
                    //addAllLevelMst.CreditOrDebit = request.CreditOrDebit;
                    addAllLevelMst.IsActive = true;
                    addAllLevelMst.IsDeleted = false;
                    addAllLevelMst.CreatedBy = 1;
                    addAllLevelMst.UpdatedBy = 1;
                    addAllLevelMst.CreatedDate = DateTime.Now; // Insert Current Time
                    addAllLevelMst.UpdatedDate = DateTime.Now;
                    await _secondCollection.InsertOneAsync(addAllLevelMst);

                    addLevelsecondResDTO.LevelId = addAllLevelMst.id;

                    if (addLevelsecondResDTO != null)
                    {
                        response.Data = addLevelsecondResDTO;
                        response.Message = CommonConstant.Data_Saved_Successfully;
                        response.Status = true;
                        response.StatusCode = System.Net.HttpStatusCode.OK;
                    }
                }
                else if (request.LevelId == 3)
                {
                    addAllLevelMst.Code = request.Code;
                    addAllLevelMst.Name = request.Name;
                    addAllLevelMst.ParentLevelTypeId = request.ParentLevelTypeId;
                    addAllLevelMst.IsFinalLevel = request.IsFinalLevel;
                    addAllLevelMst.CreditOrDebit = request.CreditOrDebit;
                    addAllLevelMst.IsActive = true;
                    addAllLevelMst.IsDeleted = false;
                    addAllLevelMst.CreatedBy = 1;
                    addAllLevelMst.UpdatedBy = 1;
                    addAllLevelMst.CreatedDate = DateTime.Now; // Insert Current Time
                    addAllLevelMst.UpdatedDate = DateTime.Now;
                    await _thirdCollection.InsertOneAsync(addAllLevelMst);

                    addLevelsecondResDTO.LevelId = addAllLevelMst.id;

                    if (addLevelsecondResDTO != null)
                    {
                        response.Data = addLevelsecondResDTO;
                        response.Message = CommonConstant.Data_Saved_Successfully;
                        response.Status = true;
                        response.StatusCode = System.Net.HttpStatusCode.OK;
                    }
                }
                else if (request.LevelId == 4)
                {
                    addAllLevelMst.Code = request.Code;
                    addAllLevelMst.Name = request.Name;
                    addAllLevelMst.ParentLevelTypeId = request.ParentLevelTypeId;
                    addAllLevelMst.IsFinalLevel = request.IsFinalLevel;
                    addAllLevelMst.CreditOrDebit = request.CreditOrDebit;
                    addAllLevelMst.IsActive = true;
                    addAllLevelMst.IsDeleted = false;
                    addAllLevelMst.CreatedBy = 1;
                    addAllLevelMst.UpdatedBy = 1;
                    addAllLevelMst.CreatedDate = DateTime.Now; // Insert Current Time
                    addAllLevelMst.UpdatedDate = DateTime.Now;
                    await _forthCollection.InsertOneAsync(addAllLevelMst);

                    addLevelsecondResDTO.LevelId = addAllLevelMst.id;

                    if (addLevelsecondResDTO != null)
                    {
                        response.Data = addLevelsecondResDTO;
                        response.Message = CommonConstant.Data_Saved_Successfully;
                        response.Status = true;
                        response.StatusCode = System.Net.HttpStatusCode.OK;
                    }
                }
                else if (request.LevelId == 5)
                {
                    addAllLevelMst.Code = request.Code;
                    addAllLevelMst.Name = request.Name;
                    addAllLevelMst.ParentLevelTypeId = request.ParentLevelTypeId;
                    addAllLevelMst.IsFinalLevel = request.IsFinalLevel;
                    addAllLevelMst.CreditOrDebit = request.CreditOrDebit;
                    addAllLevelMst.IsActive = true;
                    addAllLevelMst.IsDeleted = false;
                    addAllLevelMst.CreatedBy = 1;
                    addAllLevelMst.UpdatedBy = 1;
                    addAllLevelMst.CreatedDate = DateTime.Now; // Insert Current Time
                    addAllLevelMst.UpdatedDate = DateTime.Now;
                    await _fifthCollection.InsertOneAsync(addAllLevelMst);

                    addLevelsecondResDTO.LevelId = addAllLevelMst.id;

                    if (addLevelsecondResDTO != null)
                    {
                        response.Data = addLevelsecondResDTO;
                        response.Message = CommonConstant.Data_Saved_Successfully;
                        response.Status = true;
                        response.StatusCode = System.Net.HttpStatusCode.OK;
                    }
                }
                else
                {
                    response.Message = CommonConstant.Please_Enter_Valid_Data;
                }
            }
            catch { throw; }
            return response;
        }
        public async Task<CommonResponse> GenerateTypeCodeAsync(CodeGenerateReqDTO codeGenerateReqDTO)
        {
            CodeGenerateResDTO codeGenerateResDTO = new CodeGenerateResDTO();
            CommonResponse commonResponse = new CommonResponse();
            try
            {

                var generatedCode = await _commonHelper.GenerateCode(codeGenerateReqDTO);
                if (generatedCode != null)
                {

                    codeGenerateResDTO.Code = generatedCode;

                    commonResponse.Data = codeGenerateResDTO;
                    commonResponse.Message = CommonConstant.Code_Generated_Successfully;
                    commonResponse.Status = true;
                    commonResponse.StatusCode = System.Net.HttpStatusCode.OK;
                }
                else
                {
                    commonResponse.Message = CommonConstant.Please_Enter_Valid_Data;
                }
            }
            catch { throw; }
            return commonResponse;
        }
        public async Task<CommonResponse> UpdateLevelTypeAsync(UpdateLevelReqDTO request)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                UpdateLevelResDTO updateLevelResDTO = new UpdateLevelResDTO();
                AllLevelMst addAllLevelMst = new AllLevelMst();

                if (request.LevelId == 2)
                {
                    var Data = await _secondCollection.Find(x => x.id == request.id).FirstOrDefaultAsync();
                    if (Data != null)
                    {
                        addAllLevelMst.Code = Data.Code;
                        addAllLevelMst.Name = request.Name;
                        addAllLevelMst.ParentLevelTypeId = Data.ParentLevelTypeId;
                        addAllLevelMst.IsFinalLevel = request.IsFinalLevel;
                        addAllLevelMst.CreditOrDebit = request.CreditOrDebit;
                        addAllLevelMst.IsActive = Data.IsActive;
                        addAllLevelMst.IsDeleted = Data.IsDeleted;
                        addAllLevelMst.CreatedBy = Data.CreatedBy;
                        addAllLevelMst.UpdatedBy = Data.UpdatedBy;
                        addAllLevelMst.CreatedDate = Data.CreatedDate;// Insert Current Time
                        addAllLevelMst.UpdatedDate = DateTime.Now;

                        await _secondCollection.ReplaceOneAsync(x => x.id == request.id, addAllLevelMst);
                        updateLevelResDTO.id = Data.id;

                    }

                    if (updateLevelResDTO != null)
                    {
                        response.Data = updateLevelResDTO;
                        response.Message = CommonConstant.Data_Updated_Successfully;
                        response.Status = true;
                        response.StatusCode = System.Net.HttpStatusCode.OK;
                    }
                    else
                    {
                        response.Message = CommonConstant.Please_Enter_Valid_Data;
                    }
                }
                else if (request.LevelId == 3)
                {
                    var Data = await _thirdCollection.Find(x => x.id == request.id).FirstOrDefaultAsync();
                    if (Data != null)
                    {
                        addAllLevelMst.Code = Data.Code;
                        addAllLevelMst.Name = request.Name;
                        addAllLevelMst.ParentLevelTypeId = request.ParentLevelTypeId;
                        addAllLevelMst.IsFinalLevel = request.IsFinalLevel;
                        addAllLevelMst.CreditOrDebit = request.CreditOrDebit;
                        addAllLevelMst.IsActive = Data.IsActive;
                        addAllLevelMst.IsDeleted = Data.IsDeleted;
                        addAllLevelMst.CreatedBy = Data.CreatedBy;
                        addAllLevelMst.UpdatedBy = Data.UpdatedBy;
                        addAllLevelMst.CreatedDate = Data.CreatedDate;// Insert Current Time
                        addAllLevelMst.UpdatedDate = DateTime.Now;

                        await _thirdCollection.ReplaceOneAsync(x => x.id == request.id, addAllLevelMst);
                        updateLevelResDTO.id = addAllLevelMst.id;

                    }

                    if (updateLevelResDTO != null)
                    {
                        response.Data = updateLevelResDTO;
                        response.Message = CommonConstant.Data_Updated_Successfully;
                        response.Status = true;
                        response.StatusCode = System.Net.HttpStatusCode.OK;
                    }
                    else
                    {
                        response.Message = CommonConstant.Please_Enter_Valid_Data;
                    }
                }
                else if (request.LevelId == 4)
                {
                    var Data = await _forthCollection.Find(x => x.id == request.id).FirstOrDefaultAsync();
                    if (Data != null)
                    {
                        addAllLevelMst.Code = Data.Code;
                        addAllLevelMst.Name = request.Name;
                        addAllLevelMst.ParentLevelTypeId = request.ParentLevelTypeId;
                        addAllLevelMst.IsFinalLevel = request.IsFinalLevel;
                        addAllLevelMst.CreditOrDebit = request.CreditOrDebit;
                        addAllLevelMst.IsActive = Data.IsActive;
                        addAllLevelMst.IsDeleted = Data.IsDeleted;
                        addAllLevelMst.CreatedBy = Data.CreatedBy;
                        addAllLevelMst.UpdatedBy = Data.UpdatedBy;
                        addAllLevelMst.CreatedDate = Data.CreatedDate;// Insert Current Time
                        addAllLevelMst.UpdatedDate = DateTime.Now;

                        await _forthCollection.ReplaceOneAsync(x => x.id == request.id, addAllLevelMst);
                        updateLevelResDTO.id = addAllLevelMst.id;

                    }

                    if (updateLevelResDTO != null)
                    {
                        response.Data = updateLevelResDTO;
                        response.Message = CommonConstant.Data_Updated_Successfully;
                        response.Status = true;
                        response.StatusCode = System.Net.HttpStatusCode.OK;
                    }
                    else
                    {
                        response.Message = CommonConstant.Please_Enter_Valid_Data;
                    }
                }
                else if (request.LevelId == 5)
                {
                    var Data = await _fifthCollection.Find(x => x.id == request.id).FirstOrDefaultAsync();
                    if (Data != null)
                    {
                        addAllLevelMst.Code = Data.Code;
                        addAllLevelMst.Name = request.Name;
                        addAllLevelMst.ParentLevelTypeId = request.ParentLevelTypeId;
                        addAllLevelMst.IsFinalLevel = request.IsFinalLevel;
                        addAllLevelMst.CreditOrDebit = request.CreditOrDebit;
                        addAllLevelMst.IsActive = Data.IsActive;
                        addAllLevelMst.IsDeleted = Data.IsDeleted;
                        addAllLevelMst.CreatedBy = Data.CreatedBy;
                        addAllLevelMst.UpdatedBy = Data.UpdatedBy;
                        addAllLevelMst.CreatedDate = Data.CreatedDate;// Insert Current Time
                        addAllLevelMst.UpdatedDate = DateTime.Now;

                        await _fifthCollection.ReplaceOneAsync(x => x.id == request.id, addAllLevelMst);
                        updateLevelResDTO.id = addAllLevelMst.id;

                    }

                    if (updateLevelResDTO != null)
                    {
                        response.Data = updateLevelResDTO;
                        response.Message = CommonConstant.Data_Updated_Successfully;
                        response.Status = true;
                        response.StatusCode = System.Net.HttpStatusCode.OK;
                    }
                    else
                    {
                        response.Message = CommonConstant.Please_Enter_Valid_Data;
                    }
                }
                else
                {
                    response.Message = CommonConstant.Please_Enter_Valid_Data;
                }
            }
            catch { throw; }
            return response;
        }
        public async Task<CommonResponse> DeleteLevelTypeAsync(DeleteLevelReqDTO request)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                UpdateLevelResDTO updateLevelResDTO = new UpdateLevelResDTO();
                AllLevelMst addAllLevelMst = new AllLevelMst();

                if (request.LevelId == 2)
                {
                    var Data = await _secondCollection.Find(x => x.id == request.id).FirstOrDefaultAsync();
                    if (Data != null)
                    {

                        var Filter = new BsonDocument()
                           .Add("IsDeleted", true)
                           .Add("UpdatedDate", DateTime.Now.ToString())
                           .Add("IsActive", false);

                        var updateDoc = new BsonDocument("$set", Filter);

                        var Result = await _secondCollection.UpdateOneAsync(x => x.id == request.id, updateDoc);
                        updateLevelResDTO.id = Data.id;

                    }
                    else
                    {
                        response.Message = CommonConstant.Please_Enter_Valid_Data;
                    }

                    if (updateLevelResDTO != null)
                    {
                        response.Data = updateLevelResDTO;
                        response.Message = CommonConstant.Data_Updated_Successfully;
                        response.Status = true;
                        response.StatusCode = System.Net.HttpStatusCode.OK;
                    }

                }
                else if (request.LevelId == 3)
                {
                    var Data = await _thirdCollection.Find(x => x.id == request.id).FirstOrDefaultAsync();
                    if (Data != null)
                    {

                        var Filter = new BsonDocument()
                           .Add("IsDeleted", true)
                           .Add("UpdatedDate", DateTime.Now.ToString())
                           .Add("IsActive", false);

                        var updateDoc = new BsonDocument("$set", Filter);

                        var Result = await _thirdCollection.UpdateOneAsync(x => x.id == request.id, updateDoc);
                        updateLevelResDTO.id = Data.id;

                    }
                    else
                    {
                        response.Message = CommonConstant.Please_Enter_Valid_Data;
                    }

                    if (updateLevelResDTO != null)
                    {
                        response.Data = updateLevelResDTO;
                        response.Message = CommonConstant.Data_Updated_Successfully;
                        response.Status = true;
                        response.StatusCode = System.Net.HttpStatusCode.OK;
                    }

                }
                else if (request.LevelId == 4)
                {
                    var Data = await _forthCollection.Find(x => x.id == request.id).FirstOrDefaultAsync();
                    if (Data != null)
                    {

                        var Filter = new BsonDocument()
                           .Add("IsDeleted", true)
                           .Add("UpdatedDate", DateTime.Now.ToString())
                           .Add("IsActive", false);

                        var updateDoc = new BsonDocument("$set", Filter);

                        var Result = await _forthCollection.UpdateOneAsync(x => x.id == request.id, updateDoc);
                        updateLevelResDTO.id = Data.id;

                    }
                    else
                    {
                        response.Message = CommonConstant.Please_Enter_Valid_Data;
                    }

                    if (updateLevelResDTO != null)
                    {
                        response.Data = updateLevelResDTO;
                        response.Message = CommonConstant.Data_Updated_Successfully;
                        response.Status = true;
                        response.StatusCode = System.Net.HttpStatusCode.OK;
                    }

                }
                else if (request.LevelId == 5)
                {
                    var Data = await _forthCollection.Find(x => x.id == request.id).FirstOrDefaultAsync();
                    if (Data != null)
                    {

                        var Filter = new BsonDocument()
                           .Add("IsDeleted", true)
                           .Add("UpdatedDate", DateTime.Now.ToString())
                           .Add("IsActive", false);

                        var updateDoc = new BsonDocument("$set", Filter);

                        var Result = await _forthCollection.UpdateOneAsync(x => x.id == request.id, updateDoc);
                        updateLevelResDTO.id = Data.id;

                    }
                    else
                    {
                        response.Message = CommonConstant.Please_Enter_Valid_Data;
                    }


                    if (updateLevelResDTO != null)
                    {
                        response.Data = updateLevelResDTO;
                        response.Message = CommonConstant.Data_Updated_Successfully;
                        response.Status = true;
                        response.StatusCode = System.Net.HttpStatusCode.OK;
                    }

                }
                else
                {
                    response.Message = CommonConstant.Please_Enter_Valid_Data;
                }
            }
            catch { throw; }
            return response;
        }
        public async Task<CommonResponse> GetLevelTypeAsync(GetLevelTypeReqDTO request)
        {
            CommonResponse response = new CommonResponse();
            GetLevelTypeResDTO getLevelTypeRes1 = new GetLevelTypeResDTO();
            try
            {
                if (request.LevelId == 1)
                {
                    getLevelTypeRes1.firstLevelList = await _firstCollection.Find(x => x.IsActive == true && x.IsDeleted == false).ToListAsync();
                }
                else if (request.LevelId == 2 && !string.IsNullOrEmpty(request.ParentLevelTypeId))
                {

                    getLevelTypeRes1.AllLevelList = await _secondCollection.Find(x => x.IsActive == true && x.IsDeleted == false && x.ParentLevelTypeId == request.ParentLevelTypeId).ToListAsync();
                }
                else if (request.LevelId == 3 && !string.IsNullOrEmpty(request.ParentLevelTypeId))
                {
                    getLevelTypeRes1.AllLevelList = await _thirdCollection.Find(x => x.IsActive == true && x.IsDeleted == false && x.ParentLevelTypeId == request.ParentLevelTypeId).ToListAsync();
                }
                else if (request.LevelId == 4 && !string.IsNullOrEmpty(request.ParentLevelTypeId))
                {
                    getLevelTypeRes1.AllLevelList = await _forthCollection.Find(x => x.IsActive == true && x.IsDeleted == false && x.ParentLevelTypeId == request.ParentLevelTypeId).ToListAsync();

                }
                else if (request.LevelId == 5 && !string.IsNullOrEmpty(request.ParentLevelTypeId))
                {
                    getLevelTypeRes1.AllLevelList = await _fifthCollection.Find(x => x.IsActive == true && x.IsDeleted == false && x.ParentLevelTypeId == request.ParentLevelTypeId).ToListAsync();
                }
                if (getLevelTypeRes1.firstLevelList.Count > 0 || getLevelTypeRes1.AllLevelList.Count > 0)
                {
                    response.Data = getLevelTypeRes1;
                    response.Message = CommonConstant.Data_Found_Successfully;
                    response.Status = true;
                    response.StatusCode = System.Net.HttpStatusCode.OK;
                }
                else
                {
                    response.Message = CommonConstant.Data_Not_Found;
                }
            }
            catch { throw; }
            return response;
        }


    }
}
