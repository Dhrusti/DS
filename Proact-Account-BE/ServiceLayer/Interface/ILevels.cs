using DTO.ReqDTO;
using Helpers.CommonModels;

namespace ServiceLayer.Interface
{
    public interface ILevels
    {
        public Task<CommonResponse> AddLevelFirstAsync(AddLevelFirstReqDTO request);
        public Task<CommonResponse> AddAllLevelAsync(AddAllLevelReqDTO request);
        public Task<CommonResponse> GenerateTypeCodeAsync(CodeGenerateReqDTO request);
        public Task<CommonResponse> UpdateLevelTypeAsync(UpdateLevelReqDTO request);
        public Task<CommonResponse> DeleteLevelTypeAsync(DeleteLevelReqDTO request);
        public Task<CommonResponse> GetLevelTypeAsync(GetLevelTypeReqDTO request);


    }
}
