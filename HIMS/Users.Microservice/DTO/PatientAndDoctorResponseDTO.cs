using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PatientAndDoctorResponseDTO
    {
        public PatientResponseDTO PatientResponseDTO { get; set; }
        public DoctorNameResponseDTO DoctorNameResponseDTO { get; set; }
    }
}
