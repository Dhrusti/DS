using DTO.ReqDTO;
using DTO.ResDTO;
using Helpers.CommonModels;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;
using WebAPI.ViewModels.ReqViewModels;
using WebAPI.ViewModels.ResViewModels;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LevelsController : ControllerBase
    {
        private readonly ILevels _iLevels;
        public LevelsController(ILevels iLevels)
        {
            _iLevels = iLevels;
        }

        [HttpPost("AddLevelFirst")]
        public async Task<CommonResponse> AddLevelFirstAsync(AddLevelFirstReqViewModel request)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                response = await _iLevels.AddLevelFirstAsync(request.Adapt<AddLevelFirstReqDTO>());
                AddLevelFirstResDTO model = response.Data;
                response.Data = model.Adapt<AddLevelFirstResViewModel>();
            }
            catch { throw; }
            return response;
        }

        [HttpPost("AddAllLevel")]
        public async Task<CommonResponse> AddAllLevelAsync(AddAllLevelReqViewModel request)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                response = await _iLevels.AddAllLevelAsync(request.Adapt<AddAllLevelReqDTO>());
                AddAllLevelResDTO model = response.Data;
                response.Data = model.Adapt<AddAllLevelResViewModel>();
            }
            catch { throw; }
            return response;
        }

        [HttpPost("GenerateTypeCode")]
        public async Task<CommonResponse> GenerateTypeCodeAsync(CodeGenerateReqViewModel request)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                response = await _iLevels.GenerateTypeCodeAsync(request.Adapt<CodeGenerateReqDTO>());
                CodeGenerateResDTO model = response.Data;
                response.Data = model.Adapt<CodeGenerateResViewModel>();
            }
            catch { throw; }
            return response;
        }

        [HttpPost("UpdateLevelType")]
        public async Task<CommonResponse> UpdateLevelTypeAsync(UpdateLevelReqViewModel request)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                response = await _iLevels.UpdateLevelTypeAsync(request.Adapt<UpdateLevelReqDTO>());
                UpdateLevelResDTO model = response.Data;
                response.Data = model.Adapt<UpdateLevelResViewModel>();
            }
            catch { throw; }
            return response;
        }


        [HttpPost("DeleteLevelType")]
        public async Task<CommonResponse> DeleteLevelTypeAsync(DeleteLevelReqViewModel request)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                response = await _iLevels.DeleteLevelTypeAsync(request.Adapt<DeleteLevelReqDTO>());
                DeleteLevelResDTO model = response.Data;
                response.Data = model.Adapt<DeleteLevelResViewModel>();
            }
            catch { throw; }
            return response;
        }


        [HttpPost("GetLevelTypeAsync")]
        public async Task<CommonResponse> GetLevelTypeAsync(GetLevelTypesReqViewModel request)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                response = await _iLevels.GetLevelTypeAsync(request.Adapt<GetLevelTypeReqDTO>());
                GetLevelTypeResDTO model = response.Data;
                response.Data = model.Adapt<GetLevelTypeResViewModel>();
            }
            catch { throw; }
            return response;
        }

    }
}
