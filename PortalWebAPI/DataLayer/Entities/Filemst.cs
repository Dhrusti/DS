using System;
using System.Collections.Generic;

#nullable disable

namespace DataLayer.Entities
{
    public partial class filemst
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileSize { get; set; }
        public string FileFormat { get; set; }
        public string ClientIP { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
