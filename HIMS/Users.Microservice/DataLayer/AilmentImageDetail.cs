using System;
using System.Collections.Generic;

namespace DataLayer
{
    public partial class AilmentImageDetail
    {
        public int AilmentImageId { get; set; }
        public int AilmentId { get; set; }
        public string Image { get; set; } = null!;
        public bool IsDelete { get; set; }
        public int CreatedBy { get; set; }
        public int UpdateBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
