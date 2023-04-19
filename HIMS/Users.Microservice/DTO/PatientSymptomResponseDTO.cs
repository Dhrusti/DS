using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PatientSymptomResponseDTO
    {
        public int? SymptomId { get; set; }
        public string SymptomName { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete{ get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
