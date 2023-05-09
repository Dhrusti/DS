using System.Collections.Generic;

namespace DTOs.ResDTOs
{
    public class UploadFileResDTO
    {
        public List<FileDetail> FileDetails { get; set; }

        public class FileDetail
        {
            public string FileName { get; set; }
            public string FilePath { get; set; }
        }
    }
}
