using System;
using System.Collections.Generic;

namespace DataLayer
{
    public partial class SymptomMst
    {
        public int SymptomId { get; set; }
        public string SymptomName { get; set; } = null!;
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public int CreatedBy { get; set; }
        public int UpdateBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
