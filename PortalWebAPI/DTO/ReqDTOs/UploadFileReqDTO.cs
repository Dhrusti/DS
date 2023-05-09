using System.Collections.Generic;

namespace DTOs.ReqDTOs
{
    public class UploadFileReqDTO
    {
        public string sRef_number { get; set; }
        public string? vDoc_description { get; set; } = null;
        public string sDoc_type { get; set; }
        public int? vStorageID { get; set; } = 0;
        public List<dynamic> File { get; set; }
    }
}
