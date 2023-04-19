using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class AppointmentRequestDTO
    {
        public int AppointmentId { get; set; }
        public int AilmentId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
        public int CreatedBy { get; set; }
    }
}
