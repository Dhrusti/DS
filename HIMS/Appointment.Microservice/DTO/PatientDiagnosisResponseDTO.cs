using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PatientDiagnosisResponseDTO
    {
        public int PatientDiagnosisId { get; set; }
        public int AilmentId { get; set; }
        public string DiagnosisIds { get; set; }
        public string DiagnosisNames { get; set; }
        public string? Note { get; set; }
    }
}
