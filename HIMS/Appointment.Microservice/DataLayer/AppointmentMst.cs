using System;
using System.Collections.Generic;

namespace DataLayer
{
    public partial class AppointmentMst
    {
        public int AppointmentId { get; set; }
        public int AilmentId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string AppointmentTime { get; set; } = null!;
        public string Status { get; set; } = null!;
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public int CreatedBy { get; set; }
        public int UpdateBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
