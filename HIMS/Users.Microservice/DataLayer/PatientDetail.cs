using System;
using System.Collections.Generic;

namespace DataLayer
{
    public partial class PatientDetail
    {
        public int PatientId { get; set; }
        public string PatientCode { get; set; } = null!;
        public int UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public DateTime? Dob { get; set; }
        public string? Email { get; set; }
        public string MobileNo { get; set; } = null!;
        public string? AlternateMobileNo { get; set; }
        public string? Address { get; set; }
        public string? StreetLandMark { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? Pincode { get; set; }
        public string? Image { get; set; }
        public string? ImagePath { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public int CreatedBy { get; set; }
        public int UpdateBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
