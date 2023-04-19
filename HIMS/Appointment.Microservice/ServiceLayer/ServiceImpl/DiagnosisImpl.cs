using BusinessLayer;
using DTO;
using ServiceLayer.IService;

namespace ServiceLayer.ServiceImpl
{
    public class DiagnosisImpl : IDiagnosis
    {
        private readonly DiagnosisBLL _diagnosisBLL;
        public DiagnosisImpl(DiagnosisBLL diagnosisBLL)
        {
            _diagnosisBLL = diagnosisBLL;
        }

        public async Task<ResponseDTO> GetAllMedicineAsync()
        {
            return await _diagnosisBLL.GetAllMedicineAsync();
        }
        public async Task<ResponseDTO> GetAllProblemAsync(string ProblemName)
        {
            return await _diagnosisBLL.GetAllProblemAsync(ProblemName);
        }
        public async Task<ResponseDTO> GetAllDiagnosisAsync(string DiagnosisName)
        {
            return await _diagnosisBLL.GetAllDiagnosisAsync(DiagnosisName);
        }
        public async Task<ResponseDTO> GetAllMedicationByAilmentAsync(int AilmentId)
        {
            return await _diagnosisBLL.GetAllMedicationByAilmentAsync(AilmentId);
        }
        public async Task<ResponseDTO> GetSummaryByAilmentAsync(int AilmentId)
        {
            return await _diagnosisBLL.GetSummaryByAilmentAsync(AilmentId);
        }
        public async Task<ResponseDTO> AddProblemAndDiagnosisAsync(AddProblemAndDiagnosisRequestDTO addProblemAndDiagnosisRequestDTO)
        {
            return await _diagnosisBLL.AddProblemAndDiagnosisAsync(addProblemAndDiagnosisRequestDTO);
        }
        public async Task<ResponseDTO> AddUpdateMedicationAsync(MedicationRequestDTO medicationRequestDTO)
        {
            return await _diagnosisBLL.AddUpdateMedicationAsync(medicationRequestDTO);
        }
        public async Task<ResponseDTO> DeleteMedicationAsync(int PatientMedicationId, int DeletedBy)
        {
            return await _diagnosisBLL.DeleteMedicationAsync(PatientMedicationId, DeletedBy);
        }
    }
}
