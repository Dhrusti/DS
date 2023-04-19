using System;
using System.Collections.Generic;

namespace DataLayer
{
    public partial class DiagnosisMst
    {
        public int DiagnosisId { get; set; }
        public string DiagnosisName { get; set; } = null!;
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public int CreatedBy { get; set; }
        public int UpdateBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
