using System.Collections.Generic;

namespace PortalWebAPI.ViewModels.ResViewModels
{
    public class UploadFileResViewModel
    {
        public List<FileDetail> FileDetails { get; set; }

        public class FileDetail
        {
            public string FileName { get; set; }
            public string FilePath { get; set; }
        }
    }
}
