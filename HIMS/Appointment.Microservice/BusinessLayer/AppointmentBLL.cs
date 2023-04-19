using DataLayer;
using DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Http.Json;
using static Appointment.Microservice.Constants.Enums;

namespace BusinessLayer
{
    public class AppointmentBLL
    {
        private readonly HIMSAppointmentDBContext _context;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public AppointmentBLL(HttpClient httpClient, IConfiguration configuration, HIMSAppointmentDBContext context)
        {
            this._httpClient = httpClient;
            this._configuration = configuration;
            this._context = context;
        }

        public async Task<ResponseDTO> GetAllAppointmentAsync(int DoctorId, string AppointmentStatus)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                List<AppointmentMst> appointmentMsts = new List<AppointmentMst>();

                AppointmentResponseDTO appointmentResponseDTO;
                List<AppointmentResponseDTO> appointmentResponseDTOs = new List<AppointmentResponseDTO>();

                if (DoctorId != 0)
                {
                    appointmentMsts = await _context.AppointmentMsts.Where(x => x.IsActive && !x.IsDelete && x.Status == AppointmentStatus.ToString() && x.DoctorId == DoctorId).ToListAsync();
                }
                else
                {
                    appointmentMsts = await _context.AppointmentMsts.Where(x => x.IsActive && !x.IsDelete && x.Status == AppointmentStatus.ToString()).ToListAsync();
                }

                foreach (var item in appointmentMsts)
                {
                    AilmentResponseDTO ailmentResponseDTO = await _httpClient.GetFromJsonAsync<AilmentResponseDTO>(_configuration["UsersMicroserviceBaseURL"] + "api/User/GetPatientAilmentDetailsAsync?AilmentId=" + item.AilmentId);

                    if (ailmentResponseDTO != null)
                    {
                        appointmentResponseDTO = new AppointmentResponseDTO();

                        appointmentResponseDTO.AppointmentId = item.AppointmentId;
                        appointmentResponseDTO.AilmentId = item.AilmentId;
                        appointmentResponseDTO.DoctorId = item.DoctorId;
                        appointmentResponseDTO.AppointmentDate = item.AppointmentDate;
                        appointmentResponseDTO.AppointmentTime = item.AppointmentTime;
                        appointmentResponseDTO.Status = item.Status;

                        appointmentResponseDTO.PatientId = ailmentResponseDTO.Data.PatientId;
                        appointmentResponseDTO.FirstName = ailmentResponseDTO.Data.FirstName;
                        appointmentResponseDTO.LastName = ailmentResponseDTO.Data.LastName;
                        appointmentResponseDTO.FullName = ailmentResponseDTO.Data.FirstName + ailmentResponseDTO.Data.LastName;
                        appointmentResponseDTO.Gender = ailmentResponseDTO.Data.Gender;
                        appointmentResponseDTO.MobileNo = ailmentResponseDTO.Data.MobileNo;
                        appointmentResponseDTO.AlternateMobileNo = ailmentResponseDTO.Data.AlternateMobileNo;
                        appointmentResponseDTO.Image = ailmentResponseDTO.Data.Image;

                        appointmentResponseDTOs.Add(appointmentResponseDTO);
                    }
                }
                if (appointmentResponseDTOs.Count > 0)
                {
                    responseDTO.Data = appointmentResponseDTOs;
                    responseDTO.Message = "Appointment Data Found!";
                    responseDTO.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    responseDTO.Data = null;
                    responseDTO.Message = "Appointment Data Not Found!";
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

        public async Task<ResponseDTO> AddAppointmentAsync(AppointmentRequestDTO appointmentRequestDTO)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                if (appointmentRequestDTO != null)
                {
                    AppointmentMst appointmentMst = new AppointmentMst();
                    appointmentMst.AilmentId = appointmentRequestDTO.AilmentId;
                    appointmentMst.DoctorId = appointmentRequestDTO.DoctorId;
                    appointmentMst.AppointmentDate = appointmentRequestDTO.AppointmentDate;
                    appointmentMst.AppointmentTime = appointmentRequestDTO.AppointmentTime;
                    appointmentMst.Status = AppointmentStatus.Scheduled.ToString();
                    appointmentMst.IsActive = true;
                    appointmentMst.IsDelete = false;
                    appointmentMst.CreatedBy = appointmentRequestDTO.CreatedBy;
                    appointmentMst.UpdateBy = appointmentRequestDTO.CreatedBy;
                    appointmentMst.CreatedAt = DateTime.Now;
                    appointmentMst.UpdatedAt = DateTime.Now;

                    await _context.AppointmentMsts.AddAsync(appointmentMst);
                    await _context.SaveChangesAsync();

                    responseDTO.Data = appointmentRequestDTO;
                    responseDTO.Message = "Appointment Booked Successfully!";
                    responseDTO.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    responseDTO.Data = appointmentRequestDTO;
                    responseDTO.Message = "No Data Found!";
                    responseDTO.StatusCode = HttpStatusCode.ExpectationFailed;
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

        public async Task<ResponseDTO> UpdateAppointmentStatusAsync(AppointmentStatusRequestDTO appointmentStatusRequestDTO)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                AppointmentMst appointmentMst = await _context.AppointmentMsts.Where(x => x.AppointmentId == appointmentStatusRequestDTO.AppointmentId).FirstOrDefaultAsync();
                if (appointmentMst != null)
                {
                    appointmentMst.Status = appointmentStatusRequestDTO.Status;
                    appointmentMst.UpdatedAt = DateTime.Now;
                    appointmentMst.UpdateBy = appointmentStatusRequestDTO.UpdatedBy;

                    _context.Entry(appointmentMst).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    responseDTO.Data = null;
                    responseDTO.Message = "Appointment Status Updated Successfully!";
                    responseDTO.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    responseDTO.Data = appointmentStatusRequestDTO;
                    responseDTO.Message = "No Appointment Data Found By This AppointmentId!";
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

        public async Task<ResponseDTO> DeleteAppointmentAsync(int AppointmentId, int UpdatedBy)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                AppointmentMst appointmentMst = await _context.AppointmentMsts.Where(x => x.AppointmentId == AppointmentId).FirstOrDefaultAsync();
                if (appointmentMst != null)
                {
                    appointmentMst.IsActive = false;
                    appointmentMst.IsDelete = true;
                    appointmentMst.UpdatedAt = DateTime.Now;
                    appointmentMst.UpdateBy = UpdatedBy;

                    _context.Entry(appointmentMst).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    responseDTO.Data = null;
                    responseDTO.Message = "Appointment Deleted Successfully!";
                    responseDTO.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    responseDTO.Data = AppointmentId;
                    responseDTO.Message = "No Appointment Data Found By This AppointmentId!";
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
    }
}