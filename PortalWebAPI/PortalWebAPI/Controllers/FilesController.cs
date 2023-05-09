using DTOs.ReqDTOs;
using DTOs.ResDTOs;
using Helpers.CommonModels;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using PortalWebAPI.ViewModels.ReqViewModels;
using PortalWebAPI.ViewModels.ResViewModels;
using ServiceLayer.Interface;
using System.Threading.Tasks;

namespace PortalWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFile _iFile;

        public FilesController(IFile iFile)
        {
            _iFile = iFile;
        }

        [HttpPost("UploadFileAsync")]
        public async Task<CommonResponse> UploadFileAsync([FromForm] UploadFileReqViewModel request)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                response = await _iFile.UploadFileAsync(request.Adapt<UploadFileReqDTO>());
                UploadFileResDTO model = response.Data;
                response.Data = model.Adapt<UploadFileResViewModel>();
            }
            catch { throw; }
            return response;
        }

        [HttpPost("DownloadFileAsync")]
        public async Task<CommonResponse> DownloadFileAsync([FromForm] string vRef_number)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                response = await _iFile.DownloadFileAsync(vRef_number);
            }
            catch { throw; }
            return response;
        }
    }
}
