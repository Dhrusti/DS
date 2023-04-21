using BussinessLayer;
using DTO.ReqDTO;
using Helpers.CommonModels;
using ServiceLayer.Interface;

namespace ServiceLayer.Implementation
{
    public class LevelsImpl : ILevels
    {
        private readonly LevelBLL _levelBLL;
        public LevelsImpl(LevelBLL levelBLL)
        {
            _levelBLL = levelBLL;
        }
        public async Task<CommonResponse> AddLevelFirstAsync(AddLevelFirstReqDTO request) => await _levelBLL.AddLevelFirstAsync(request);
        public async Task<CommonResponse> AddAllLevelAsync(AddAllLevelReqDTO request) => await _levelBLL.AddAllLevelAsync(request);
        public async Task<CommonResponse> GenerateTypeCodeAsync(CodeGenerateReqDTO request) => await _levelBLL.GenerateTypeCodeAsync(request);
        public async Task<CommonResponse> UpdateLevelTypeAsync(UpdateLevelReqDTO request) => await _levelBLL.UpdateLevelTypeAsync(request);
        public async Task<CommonResponse> DeleteLevelTypeAsync(DeleteLevelReqDTO request) => await _levelBLL.DeleteLevelTypeAsync(request);
        public async Task<CommonResponse> GetLevelTypeAsync(GetLevelTypeReqDTO request) => await _levelBLL.GetLevelTypeAsync(request);

    }
}
