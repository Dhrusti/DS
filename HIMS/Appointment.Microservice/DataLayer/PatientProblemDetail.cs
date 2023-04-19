using System;
using System.Collections.Generic;

namespace DataLayer
{
    public partial class PatientProblemDetail
    {
        public int PatientProblemId { get; set; }
        public int AilmentId { get; set; }
        public string ProblemIds { get; set; } = null!;
        public string? Note { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public int CreatedBy { get; set; }
        public int UpdateBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
