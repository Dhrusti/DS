using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.IService;

namespace Appointment.Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiagnosisController : ControllerBase
    {
        private readonly IDiagnosis _iDiagnosis;
        public DiagnosisController(IDiagnosis iDiagnosis)
        {
            this._iDiagnosis = iDiagnosis;
        }

        [HttpGet]
        [Route("GetAllMedicineAsync")]
        public async Task<ResponseDTO> GetAllMedicineAsync()
        {
            return await _iDiagnosis.GetAllMedicineAsync();
        }

        [HttpGet]
        [Route("GetAllProblemAsync")]
        public async Task<ResponseDTO> GetAllProblemAsync(string? ProblemName)
        {
            return await _iDiagnosis.GetAllProblemAsync(ProblemName);
        }

        [HttpGet]
        [Route("GetAllDiagnosisAsync")]
        public async Task<ResponseDTO> GetAllDiagnosisAsync(string? DiagnosisName)
        {
            return await _iDiagnosis.GetAllDiagnosisAsync(DiagnosisName);
        }

        [HttpGet]
        [Route("GetAllMedicationByAilmentAsync")]
        public async Task<ResponseDTO> GetAllMedicationByAilmentAsync(int AilmentId)
        {
            return await _iDiagnosis.GetAllMedicationByAilmentAsync(AilmentId);
        }

        [HttpGet]
        [Route("GetSummaryByAilmentAsync")]
        public async Task<ResponseDTO> GetSummaryByAilmentAsync(int AilmentId)
        {
            return await _iDiagnosis.GetSummaryByAilmentAsync(AilmentId);
        }

        [HttpPost]
        [Route("AddProblemAndDiagnosisAsync")]
        public async Task<ResponseDTO> AddProblemAndDiagnosisAsync(AddProblemAndDiagnosisRequestDTO addProblemAndDiagnosisRequestDTO)
        {
            return await _iDiagnosis.AddProblemAndDiagnosisAsync(addProblemAndDiagnosisRequestDTO);
        }

        [HttpPost]
        [Route("AddUpdateMedicationAsync")]
        public async Task<ResponseDTO> AddUpdateMedicationAsync(MedicationRequestDTO medicationRequestDTO)
        {
            return await _iDiagnosis.AddUpdateMedicationAsync(medicationRequestDTO);
        }

        [HttpPost]
        [Route("DeleteMedicationAsync")]
        public async Task<ResponseDTO> DeleteMedicationAsync(int PatientMedicationId, int DeletedBy)
        {
            return await _iDiagnosis.DeleteMedicationAsync(PatientMedicationId, DeletedBy);
        }
    }
}
