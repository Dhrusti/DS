using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class AppointmentResponseDTO
    {
        public int AppointmentId { get; set; }
        public int AilmentId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
        public string Status { get; set; } = null!;

        public int? PatientId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public string? Gender { get; set; }
        public string? MobileNo { get; set; }
        public string? AlternateMobileNo { get; set; }
        public string? Image { get; set; }
    }
}
