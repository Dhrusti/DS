using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class AppointmentStatusRequestDTO
    {
        public int AppointmentId { get; set; }
        public string Status { get; set; }
        public int UpdatedBy { get; set; }
    }
}
