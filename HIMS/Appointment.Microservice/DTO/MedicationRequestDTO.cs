using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class MedicationRequestDTO
    {
        public int PatientMedicationId { get; set; }
        public int AilmentId { get; set; }
        public int MedicineId { get; set; }
        public string Time { get; set; }
        public string Schedule { get; set; }
        public string Note { get; set; }
        public int CreatedBy { get; set; }
    }
}
