using BusinessLayer;
using DTOs.ReqDTOs;
using Helpers.CommonModels;
using ServiceLayer.Interface;
using System.Threading.Tasks;

namespace ServiceLayer.Implemetation
{
    public class FilesImpl : IFile
    {
        private readonly FilesBLL _fileBLL;

        public FilesImpl(FilesBLL fileBLL)
        {
            _fileBLL = fileBLL;
        }

        public async Task<CommonResponse> UploadFileAsync(UploadFileReqDTO request) => await _fileBLL.UploadFileAsync(request);

        public async Task<CommonResponse> DownloadFileAsync(string vRef_number) => await _fileBLL.DownloadFileAsync(vRef_number);
    }
}
