using DataLayer;
using DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Http.Json;

namespace BusinessLayer
{
    public class DiagnosisBLL
    {
        private readonly HIMSAppointmentDBContext _context;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public DiagnosisBLL(HttpClient httpClient, IConfiguration configuration, HIMSAppointmentDBContext context)
        {
            this._httpClient = httpClient;
            this._configuration = configuration;
            this._context = context;
        }

        public async Task<ResponseDTO> GetAllMedicineAsync()
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                List<MedicineMst> medicineMst = new List<MedicineMst>();

                MedicineResponseDTO medicineResponseDTO = new MedicineResponseDTO();
                List<MedicineResponseDTO> medicineResponseDTOs = new List<MedicineResponseDTO>();

                medicineMst = await _context.MedicineMsts.Where(x => x.IsActive && !x.IsDelete).ToListAsync();

                foreach (var item in medicineMst)
                {
                    medicineResponseDTO = new MedicineResponseDTO();

                    medicineResponseDTO.MedicineId = item.MedicineId;
                    medicineResponseDTO.MedicineName = item.MedicineName;

                    medicineResponseDTOs.Add(medicineResponseDTO);
                }

                if (medicineResponseDTOs.Count > 0)
                {
                    responseDTO.Data = medicineResponseDTOs;
                    responseDTO.Message = "Medicine Data Found!";
                    responseDTO.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    responseDTO.Data = null;
                    responseDTO.Message = "Medicine Data Not Found!";
                    responseDTO.StatusCode = HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                responseDTO.Data = ex.ToString();
                responseDTO.Message = ex.Message;
                responseDTO.StatusCode = HttpStatusCode.ExpectationFailed;
            }
            return responseDTO;
        }

        public async Task<ResponseDTO> GetAllProblemAsync(string ProblemName)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                List<ProblemMst> problemMst = new List<ProblemMst>();

                ProblemResponseDTO problemResponseDTO = new ProblemResponseDTO();
                List<ProblemResponseDTO> problemResponseDTOs = new List<ProblemResponseDTO>();

                if (ProblemName != null)
                {
                    problemMst = await _context.ProblemMsts.Where(x => x.IsActive && !x.IsDelete && x.ProblemName.Substring(0, ProblemName.Length) == ProblemName).ToListAsync();
                }
                else
                {
                    problemMst = await _context.ProblemMsts.Where(x => x.IsActive && !x.IsDelete).ToListAsync();
                }

                foreach (var item in problemMst)
                {
                    problemResponseDTO = new ProblemResponseDTO();

                    problemResponseDTO.ProblemId = item.ProblemId;
                    problemResponseDTO.ProblemName = item.ProblemName;

                    problemResponseDTOs.Add(problemResponseDTO);
                }

                if (problemResponseDTOs.Count > 0)
                {
                    responseDTO.Data = problemResponseDTOs;
                    responseDTO.Message = "Problem Data Found!";
                    responseDTO.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    responseDTO.Data = null;
                    responseDTO.Message = "Problem Data Not Found!";
                    responseDTO.StatusCode = HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                responseDTO.Data = ex.ToString();
                responseDTO.Message = ex.Message;
                responseDTO.StatusCode = HttpStatusCode.ExpectationFailed;
            }
            return responseDTO;
        }

        public async Task<ResponseDTO> GetAllDiagnosisAsync(string DiagnosisName)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                List<DiagnosisMst> diagnosisMst = new List<DiagnosisMst>();

                DiagnosisResponseDTO diagnosisResponseDTO = new DiagnosisResponseDTO();
                List<DiagnosisResponseDTO> diagnosisResponseDTOs = new List<DiagnosisResponseDTO>();

                if (DiagnosisName != null)
                {
                    diagnosisMst = await _context.DiagnosisMsts.Where(x => x.IsActive && !x.IsDelete && x.DiagnosisName.Substring(0, DiagnosisName.Length) == DiagnosisName).ToListAsync();
                }
                else
                {
                    diagnosisMst = await _context.DiagnosisMsts.Where(x => x.IsActive && !x.IsDelete).ToListAsync();
                }

                foreach (var item in diagnosisMst)
                {
                    diagnosisResponseDTO = new DiagnosisResponseDTO();

                    diagnosisResponseDTO.DiagnosisId = item.DiagnosisId;
                    diagnosisResponseDTO.DiagnosisName = item.DiagnosisName;

                    diagnosisResponseDTOs.Add(diagnosisResponseDTO);
                }

                if (diagnosisResponseDTOs.Count > 0)
                {
                    responseDTO.Data = diagnosisResponseDTOs;
                    responseDTO.Message = "Diagnosis Data Found!";
                    responseDTO.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    responseDTO.Data = null;
                    responseDTO.Message = "Diagnosis Data Not Found!";
                    responseDTO.StatusCode = HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                responseDTO.Data = ex.ToString();
                responseDTO.Message = ex.Message;
                responseDTO.StatusCode = HttpStatusCode.ExpectationFailed;
            }
            return responseDTO;
        }

        public async Task<ResponseDTO> GetAllMedicationByAilmentAsync(int AilmentId)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                List<PatientMedicationResponseDTO> patientMedicationResponseDTOs = await (from pmd in _context.PatientMedicationDetails
                                                                                          join md in _context.MedicineMsts on pmd.MedicineId equals md.MedicineId into medicines
                                                                                          from medicineData in medicines.DefaultIfEmpty()
                                                                                          where pmd.AilmentId == AilmentId
                                                                                          select new PatientMedicationResponseDTO
                                                                                          {
                                                                                              PatientMedicationId = pmd.PatientMedicationId,
                                                                                              AilmentId = pmd.AilmentId,
                                                                                              MedicineId = pmd.MedicineId,
                                                                                              MedicineName = medicineData.MedicineName,
                                                                                              Time = pmd.Time,
                                                                                              Schedule = pmd.Schedule,
                                                                                              Note = pmd.Note,
                                                                                          }).ToListAsync() ?? new List<PatientMedicationResponseDTO>();

                if (patientMedicationResponseDTOs.Count > 0)
                {
                    responseDTO.Data = patientMedicationResponseDTOs;
                    responseDTO.Message = "Medication Details Found!";
                    responseDTO.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    responseDTO.Data = null;
                    responseDTO.Message = "Medication Details Not Found!";
                    responseDTO.StatusCode = HttpStatusCode.NotFound;
                }

            }
            catch (Exception ex)
            {
                responseDTO.Data = ex.ToString();
                responseDTO.Message = ex.Message;
                responseDTO.StatusCode = HttpStatusCode.ExpectationFailed;
            }
            return responseDTO;
        }

        public async Task<ResponseDTO> GetSummaryByAilmentAsync(int AilmentId)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                List<PatientMedicationResponseDTO> patientMedicationResponseDTOs = await (from pmd in _context.PatientMedicationDetails
                                                                                          join md in _context.MedicineMsts on pmd.MedicineId equals md.MedicineId into medicines
                                                                                          from medicineData in medicines.DefaultIfEmpty()
                                                                                          where pmd.AilmentId == AilmentId
                                                                                          select new PatientMedicationResponseDTO
                                                                                          {
                                                                                              PatientMedicationId = pmd.PatientMedicationId,
                                                                                              AilmentId = pmd.AilmentId,
                                                                                              MedicineId = pmd.MedicineId,
                                                                                              MedicineName = medicineData.MedicineName,
                                                                                              Time = pmd.Time,
                                                                                              Schedule = pmd.Schedule,
                                                                                              Note = pmd.Note,
                                                                                          }).ToListAsync() ?? new List<PatientMedicationResponseDTO>();

                AppointmentResponseDTO appointmentResponseDTO = await (from appointment in _context.AppointmentMsts
                                                                       where appointment.AilmentId == AilmentId//119999352511
                                                                       select new AppointmentResponseDTO
                                                                       {
                                                                           AppointmentId = appointment.AppointmentId,
                                                                           AilmentId = appointment.AilmentId,
                                                                           DoctorId = appointment.DoctorId,
                                                                           AppointmentDate = appointment.AppointmentDate,
                                                                           AppointmentTime = appointment.AppointmentTime,
                                                                           Status = appointment.Status
                                                                       }).SingleOrDefaultAsync() ?? new AppointmentResponseDTO();

                PatientAndDoctorResponseDTO patientAndDoctorResponseDTO = await _httpClient.GetFromJsonAsync<PatientAndDoctorResponseDTO>(_configuration["UsersMicroserviceBaseURL"] + "api/User/GetPatientAndDoctorDetailsAsync?AilmentId=" + AilmentId + "&DoctorId=" + appointmentResponseDTO.DoctorId) ?? new PatientAndDoctorResponseDTO();
                
                PatientDiagnosisResponseDTO patientDiagnosisResponseDTO = await (from patientDiagnosis in _context.PatientDiagnosisDetails
                                                                           where patientDiagnosis.AilmentId == AilmentId
                                                                           select new PatientDiagnosisResponseDTO
                                                                           {
                                                                               PatientDiagnosisId = patientDiagnosis.PatientDiagnosisId,
                                                                               AilmentId = patientDiagnosis.AilmentId,
                                                                               DiagnosisIds = patientDiagnosis.DiagnosisIds,
                                                                               Note = patientDiagnosis.Note
                                                                           }).SingleOrDefaultAsync() ?? new PatientDiagnosisResponseDTO();

                string DiagnosisIds = patientDiagnosisResponseDTO.DiagnosisIds ?? string.Empty;
                string DiagnosisNames = patientDiagnosisResponseDTO.DiagnosisNames;

                foreach (var item in DiagnosisIds.Split(","))
                {
                    DiagnosisNames = DiagnosisNames != null && DiagnosisNames != string.Empty ? DiagnosisNames + "," : DiagnosisNames;

                    DiagnosisMst diagnosisMst = await _context.DiagnosisMsts.Where(x => x.IsActive && !x.IsDelete && x.DiagnosisId.ToString() == item).SingleOrDefaultAsync() ?? new DiagnosisMst();
                    DiagnosisNames += diagnosisMst.DiagnosisName; 
                }
                patientAndDoctorResponseDTO.Data.PatientResponseDTO.DiagnosisIds = DiagnosisIds;
                patientAndDoctorResponseDTO.Data.PatientResponseDTO.DiagnosisNames = DiagnosisNames;

                SummaryReponseDTO summaryReponseDTO = new SummaryReponseDTO();
                summaryReponseDTO.PatientMedicationResponseDTOs = patientMedicationResponseDTOs;
                summaryReponseDTO.AppointmentResponseDTO = appointmentResponseDTO;
                summaryReponseDTO.PatientResponseDTO = patientAndDoctorResponseDTO.Data.PatientResponseDTO;
                summaryReponseDTO.DoctorNameResponseDTO = patientAndDoctorResponseDTO.Data.DoctorNameResponseDTO;

                if (summaryReponseDTO.PatientMedicationResponseDTOs.Count > 0)
                {
                    responseDTO.Data = summaryReponseDTO;
                    responseDTO.Message = "Medication Details Found!";
                    responseDTO.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    responseDTO.Data = null;
                    responseDTO.Message = "Medication Details Not Found!";
                    responseDTO.StatusCode = HttpStatusCode.NotFound;
                }

            }
            catch (Exception ex)
            {
                responseDTO.Data = ex.ToString();
                responseDTO.Message = ex.Message;
                responseDTO.StatusCode = HttpStatusCode.ExpectationFailed;
            }
            return responseDTO;
        }

        public async Task<ResponseDTO> AddProblemAndDiagnosisAsync(AddProblemAndDiagnosisRequestDTO addProblemAndDiagnosisRequestDTO)
        {
            ResponseDTO responseDTO = new ResponseDTO();

            try
            {
                if (addProblemAndDiagnosisRequestDTO != null)
                {
                    if (addProblemAndDiagnosisRequestDTO.ProblemRequestDTO != null && addProblemAndDiagnosisRequestDTO.ProblemRequestDTO.Count > 0
                        && addProblemAndDiagnosisRequestDTO.DiagnosisRequestDTO != null && addProblemAndDiagnosisRequestDTO.DiagnosisRequestDTO.Count > 0)
                    {
                        string DiagnosisIds = string.Empty;
                        foreach (var item in addProblemAndDiagnosisRequestDTO.DiagnosisRequestDTO)
                        {
                            DiagnosisMst diagnosisMst = await _context.DiagnosisMsts.Where(x => x.IsActive && !x.IsDelete && x.DiagnosisName == item.DiagnosisName).FirstOrDefaultAsync();
                            if (diagnosisMst == null)
                            {
                                diagnosisMst = new DiagnosisMst();
                                diagnosisMst.DiagnosisName = item.DiagnosisName;
                                diagnosisMst.IsActive = true;
                                diagnosisMst.IsDelete = false;
                                diagnosisMst.CreatedBy = addProblemAndDiagnosisRequestDTO.CreatedBy;
                                diagnosisMst.UpdateBy = addProblemAndDiagnosisRequestDTO.CreatedBy;
                                diagnosisMst.CreatedAt = DateTime.Now;
                                diagnosisMst.UpdatedAt = DateTime.Now;

                                await _context.AddAsync(diagnosisMst);
                                await _context.SaveChangesAsync();
                            }

                            DiagnosisIds = DiagnosisIds != string.Empty ? DiagnosisIds + "," + diagnosisMst.DiagnosisId : diagnosisMst.DiagnosisId.ToString();
                        }

                        PatientDiagnosisDetail patientDiagnosisDetail = new PatientDiagnosisDetail();
                        patientDiagnosisDetail.AilmentId = addProblemAndDiagnosisRequestDTO.AilmentId;
                        patientDiagnosisDetail.DiagnosisIds = DiagnosisIds;
                        patientDiagnosisDetail.Note = addProblemAndDiagnosisRequestDTO.DiagnosisNote;
                        patientDiagnosisDetail.IsActive = true;
                        patientDiagnosisDetail.IsDelete = false;
                        patientDiagnosisDetail.CreatedBy = addProblemAndDiagnosisRequestDTO.CreatedBy;
                        patientDiagnosisDetail.UpdateBy = addProblemAndDiagnosisRequestDTO.CreatedBy;
                        patientDiagnosisDetail.CreatedAt = DateTime.Now;
                        patientDiagnosisDetail.UpdatedAt = DateTime.Now;

                        await _context.AddAsync(patientDiagnosisDetail);
                        await _context.SaveChangesAsync();

                        string ProblemIds = string.Empty;
                        foreach (var item in addProblemAndDiagnosisRequestDTO.ProblemRequestDTO)
                        {
                            ProblemMst problemMst = await _context.ProblemMsts.Where(x => x.IsActive && !x.IsDelete && x.ProblemName == item.ProblemName).FirstOrDefaultAsync();
                            if (problemMst == null)
                            {
                                problemMst = new ProblemMst();
                                problemMst.ProblemName = item.ProblemName;
                                problemMst.IsActive = true;
                                problemMst.IsDelete = false;
                                problemMst.CreatedBy = addProblemAndDiagnosisRequestDTO.CreatedBy;
                                problemMst.UpdateBy = addProblemAndDiagnosisRequestDTO.CreatedBy;
                                problemMst.CreatedAt = DateTime.Now;
                                problemMst.UpdatedAt = DateTime.Now;

                                await _context.AddAsync(problemMst);
                                await _context.SaveChangesAsync();
                            }

                            ProblemIds = ProblemIds != string.Empty ? ProblemIds + "," + problemMst.ProblemId : problemMst.ProblemId.ToString();
                        }

                        PatientProblemDetail patientProblemDetail = new PatientProblemDetail();
                        patientProblemDetail.AilmentId = addProblemAndDiagnosisRequestDTO.AilmentId;
                        patientProblemDetail.ProblemIds = ProblemIds;
                        patientProblemDetail.Note = addProblemAndDiagnosisRequestDTO.ProblemNote;
                        patientProblemDetail.IsActive = true;
                        patientProblemDetail.IsDelete = false;
                        patientProblemDetail.CreatedBy = addProblemAndDiagnosisRequestDTO.CreatedBy;
                        patientProblemDetail.UpdateBy = addProblemAndDiagnosisRequestDTO.CreatedBy;
                        patientProblemDetail.CreatedAt = DateTime.Now;
                        patientProblemDetail.UpdatedAt = DateTime.Now;

                        await _context.AddAsync(patientProblemDetail);
                        await _context.SaveChangesAsync();

                        responseDTO.Data = null;
                        responseDTO.Message = "Problem And Diagnosis Saved Successfully!";
                        responseDTO.StatusCode = HttpStatusCode.OK;

                    }
                    else
                    {
                        responseDTO.Data = null;
                        responseDTO.Message = "Please Select Atleast 1 Problem and Diagnosis!";
                        responseDTO.StatusCode = HttpStatusCode.NotFound;
                    }
                }
                else
                {
                    responseDTO.Data = null;
                    responseDTO.Message = "Please Enter Valid Data!";
                    responseDTO.StatusCode = HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                responseDTO.Data = ex.ToString();
                responseDTO.Message = ex.Message.ToString();
                responseDTO.StatusCode = HttpStatusCode.ExpectationFailed;
            }
            return responseDTO;
        }

        public async Task<ResponseDTO> AddUpdateMedicationAsync(MedicationRequestDTO medicationRequestDTO)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                if (medicationRequestDTO != null)
                {
                    if (medicationRequestDTO.PatientMedicationId != 0)
                    {
                        PatientMedicationDetail patientMedicationDetail = await _context.PatientMedicationDetails.Where(x => x.IsActive && !x.IsDelete && x.PatientMedicationId == medicationRequestDTO.PatientMedicationId).FirstOrDefaultAsync();
                        if (patientMedicationDetail != null)
                        {
                            patientMedicationDetail.MedicineId = medicationRequestDTO.MedicineId;
                            patientMedicationDetail.Time = medicationRequestDTO.Time;
                            patientMedicationDetail.Schedule = medicationRequestDTO.Schedule;
                            patientMedicationDetail.Note = medicationRequestDTO.Note;
                            patientMedicationDetail.UpdateBy = medicationRequestDTO.CreatedBy;
                            patientMedicationDetail.UpdatedAt = DateTime.Now;

                            _context.Entry(patientMedicationDetail).State = EntityState.Modified;
                            await _context.SaveChangesAsync();

                            responseDTO.Data = null;
                            responseDTO.Message = "Medication Details Updated Successfully!";
                            responseDTO.StatusCode = HttpStatusCode.OK;

                        }
                        else
                        {
                            responseDTO.Data = null;
                            responseDTO.Message = "No Data Found Related To This Medication!";
                            responseDTO.StatusCode = HttpStatusCode.NotFound;
                        }
                    }
                    else
                    {
                        PatientMedicationDetail patientMedicationDetail = new PatientMedicationDetail();

                        patientMedicationDetail.MedicineId = medicationRequestDTO.MedicineId;
                        patientMedicationDetail.AilmentId = medicationRequestDTO.AilmentId;
                        patientMedicationDetail.Time = medicationRequestDTO.Time;
                        patientMedicationDetail.Schedule = medicationRequestDTO.Schedule;
                        patientMedicationDetail.Note = medicationRequestDTO.Note;
                        patientMedicationDetail.IsActive = true;
                        patientMedicationDetail.IsDelete = false;
                        patientMedicationDetail.CreatedBy = medicationRequestDTO.CreatedBy;
                        patientMedicationDetail.UpdateBy = medicationRequestDTO.CreatedBy;
                        patientMedicationDetail.CreatedAt = DateTime.Now;
                        patientMedicationDetail.UpdatedAt = DateTime.Now;

                        await _context.PatientMedicationDetails.AddAsync(patientMedicationDetail);
                        await _context.SaveChangesAsync();

                        responseDTO.Data = null;
                        responseDTO.Message = "Medication Details Added Successfully!";
                        responseDTO.StatusCode = HttpStatusCode.OK;
                    }

                }
                else
                {
                    responseDTO.Data = null;
                    responseDTO.Message = "No Data Found!";
                    responseDTO.StatusCode = HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                responseDTO.Data = ex.ToString();
                responseDTO.Message = ex.Message.ToString();
                responseDTO.StatusCode = HttpStatusCode.ExpectationFailed;
            }
            return responseDTO;
        }

        public async Task<ResponseDTO> DeleteMedicationAsync(int PatientMedicationId, int DeletedBy)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                PatientMedicationDetail patientMedicationDetail = await _context.PatientMedicationDetails.Where(x => x.IsActive && !x.IsDelete && x.PatientMedicationId == PatientMedicationId).FirstOrDefaultAsync();
                if (patientMedicationDetail != null)
                {
                    patientMedicationDetail.IsDelete = true;
                    patientMedicationDetail.IsActive = false;
                    patientMedicationDetail.UpdateBy = DeletedBy;
                    patientMedicationDetail.UpdatedAt = DateTime.Now;

                    _context.Entry(patientMedicationDetail).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    responseDTO.Data = null;
                    responseDTO.Message = "Medication Details Deleted Successfully!";
                    responseDTO.StatusCode = HttpStatusCode.OK;

                }
                else
                {
                    responseDTO.Data = null;
                    responseDTO.Message = "No Data Found Related To This Medication!";
                    responseDTO.StatusCode = HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                responseDTO.Data = ex.ToString();
                responseDTO.Message = ex.Message.ToString();
                responseDTO.StatusCode = HttpStatusCode.ExpectationFailed;
            }
            return responseDTO;
        }
    }
}