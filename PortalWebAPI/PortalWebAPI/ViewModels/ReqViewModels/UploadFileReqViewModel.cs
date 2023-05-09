using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace PortalWebAPI.ViewModels.ReqViewModels
{
    public class UploadFileReqViewModel
    {
        public string sRef_number { get; set; }
        public string? vDoc_description { get; set; } = null;
        public string sDoc_type { get; set; }
        public int? vStorageID { get; set; } = 0;
        public List<IFormFile> File { get; set; }
    }
}
