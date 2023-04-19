using System;
using System.Collections.Generic;

namespace DataLayer
{
    public partial class MedicineMst
    {
        public int MedicineId { get; set; }
        public string MedicineName { get; set; } = null!;
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public int CreatedBy { get; set; }
        public int UpdateBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
