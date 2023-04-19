using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.IService;

namespace Appointment.Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointment _iAppointment;
        public AppointmentController(IAppointment iAppointment)
        {
            this._iAppointment = iAppointment;
        }

        [HttpGet]
        [Route("GetAllAppointmentAsync")]
        public async Task<ResponseDTO> GetAllAppointmentAsync(int DoctorId, string AppointmentStatus)
        {
            return await _iAppointment.GetAllAppointmentAsync(DoctorId, AppointmentStatus);
        }
        
        [HttpPost]
        [Route("AddAppointmentAsync")]
        public async Task<ResponseDTO> AddAppointmentAsync(AppointmentRequestDTO appointmentRequestDTO)
        {
            return await _iAppointment.AddAppointmentAsync(appointmentRequestDTO);
        }
        
        [HttpPost]
        [Route("UpdateAppointmentStatusAsync")]
        public async Task<ResponseDTO> UpdateAppointmentStatusAsync(AppointmentStatusRequestDTO appointmentStatusRequestDTO)
        {
            return await _iAppointment.UpdateAppointmentStatusAsync(appointmentStatusRequestDTO);
        }
        
        [HttpPost]
        [Route("DeleteAppointmentAsync")]
        public async Task<ResponseDTO> DeleteAppointmentAsync(int AppointmentId, int UpdatedBy)
        {
            return await _iAppointment.DeleteAppointmentAsync(AppointmentId, UpdatedBy);
        }
    }
}
