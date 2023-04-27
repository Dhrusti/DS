using System;
using System.Collections.Generic;

namespace ValidationDemoApi.Entities
{
    public partial class TblUserDocumentMst
    {
        public int DocumentId { get; set; }
        public string? DocumentType { get; set; }
        public string? UploadDocument { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}
