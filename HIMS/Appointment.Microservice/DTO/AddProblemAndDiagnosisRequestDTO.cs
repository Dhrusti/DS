using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class AddProblemAndDiagnosisRequestDTO
    {
        public List<ProblemRequestDTO> ProblemRequestDTO { get; set; }
        public List<DiagnosisRequestDTO> DiagnosisRequestDTO { get; set; }

        public int AilmentId { get; set; }
        public string DiagnosisNote { get; set; }
        public string ProblemNote { get; set; }
        public int CreatedBy { get; set; }
    }

}
