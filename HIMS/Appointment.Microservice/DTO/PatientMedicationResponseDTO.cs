using System;
using System.Collections.Generic;

namespace DataLayer
{
    public partial class PatientMedicationResponseDTO
    {
        public int PatientMedicationId { get; set; }
        public int AilmentId { get; set; }
        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
        public string Time { get; set; } = null!;
        public string Schedule { get; set; } = null!;
        public string? Note { get; set; }
    }
}
