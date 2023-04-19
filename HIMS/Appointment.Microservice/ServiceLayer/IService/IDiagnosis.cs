using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.IService
{
    public interface IDiagnosis
    {
        public Task<ResponseDTO> GetAllMedicineAsync();
        public Task<ResponseDTO> GetAllProblemAsync(string ProblemMst);
        public Task<ResponseDTO> GetAllDiagnosisAsync(string DiagnosisName);
        public Task<ResponseDTO> GetAllMedicationByAilmentAsync(int AilmentId);
        public Task<ResponseDTO> GetSummaryByAilmentAsync(int AilmentId);
        public Task<ResponseDTO> AddProblemAndDiagnosisAsync(AddProblemAndDiagnosisRequestDTO addProblemAndDiagnosisRequestDTO);
        public Task<ResponseDTO> AddUpdateMedicationAsync(MedicationRequestDTO medicationRequestDTO);
        public Task<ResponseDTO> DeleteMedicationAsync(int PatientMedicationId, int DeletedBy);

    }
}
