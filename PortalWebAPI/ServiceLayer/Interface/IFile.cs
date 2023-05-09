using DTOs.ReqDTOs;
using Helpers.CommonModels;
using System.Threading.Tasks;

namespace ServiceLayer.Interface
{
    public interface IFile
    {
        public Task<CommonResponse> UploadFileAsync(UploadFileReqDTO request);
        public Task<CommonResponse> DownloadFileAsync(string vRef_number);
    }
}
