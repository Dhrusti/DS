using System;
using System.Collections.Generic;

namespace DataLayer
{
    public partial class AilmentMst
    {
        public int AilmentId { get; set; }
        public int PatientId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public int CreatedBy { get; set; }
        public int UpdateBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
