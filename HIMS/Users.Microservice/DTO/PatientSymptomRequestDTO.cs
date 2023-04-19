using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PatientSymptomRequestDTO
    {
        public int AilmentId { get; set; }
        public PatientRequestDTO PatientRequestDTO { get; set; }
        public List<SymptomRequestDTO> SymptomRequestDTOs { get; set; }
        public List<AilmentImageRequestDTO> AilmentImageRequestDTOs { get; set; }
    }
}
