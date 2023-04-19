using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class SummaryReponseDTO
    {
        public List<PatientMedicationResponseDTO> PatientMedicationResponseDTOs { get; set; }
        public AppointmentResponseDTO AppointmentResponseDTO { get; set; }
        public PatientResponseDTO PatientResponseDTO { get; set; }
        public DoctorNameResponseDTO DoctorNameResponseDTO { get; set; }
    }
}
