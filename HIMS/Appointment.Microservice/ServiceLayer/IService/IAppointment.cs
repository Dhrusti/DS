using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.IService
{
    public interface IAppointment
    {
        public Task<ResponseDTO> GetAllAppointmentAsync(int DoctorId, String AppointmentStatus);
        public Task<ResponseDTO> AddAppointmentAsync(AppointmentRequestDTO appointmentRequestDTO);
        public Task<ResponseDTO> UpdateAppointmentStatusAsync(AppointmentStatusRequestDTO appointmentStatusRequestDTO);
        public Task<ResponseDTO> DeleteAppointmentAsync(int AppointmentId, int UpdatedBy);

    }
}
