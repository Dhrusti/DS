using BusinessLayer;
using DTO;
using ServiceLayer.IService;

namespace ServiceLayer.ServiceImpl
{
    public class AppointmentImpl : IAppointment
    {
        private readonly AppointmentBLL _appointmentBLL;
        public AppointmentImpl(AppointmentBLL appointmentBLL)
        {
            _appointmentBLL = appointmentBLL;
        }

        public async Task<ResponseDTO> GetAllAppointmentAsync(int DoctorId, string AppointmentStatus)
        {
            return await _appointmentBLL.GetAllAppointmentAsync(DoctorId, AppointmentStatus);
        }
        public async Task<ResponseDTO> AddAppointmentAsync(AppointmentRequestDTO appointmentRequestDTO)
        {
            return await _appointmentBLL.AddAppointmentAsync(appointmentRequestDTO);
        }
        public async Task<ResponseDTO> UpdateAppointmentStatusAsync(AppointmentStatusRequestDTO appointmentStatusRequestDTO)
        {
            return await _appointmentBLL.UpdateAppointmentStatusAsync(appointmentStatusRequestDTO);
        }
        public async Task<ResponseDTO> DeleteAppointmentAsync(int AppointmentId, int UpdatedBy)
        {
            return await _appointmentBLL.DeleteAppointmentAsync(AppointmentId, UpdatedBy);
        }
    }
}
