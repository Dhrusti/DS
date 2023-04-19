using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.IService
{
    public interface IUser
    {
        public Task<ResponseDTO> GetAllUsersAsync();
        public Task<ResponseDTO> GetAllDoctorNameAsync();
        public Task<ResponseDTO> GetAllPatientFirstNameAsync(string FirstName);
        public Task<ResponseDTO> GetAllPatientLastNameAsync(string FirstName);
        public Task<ResponseDTO> GetPatientDetailsAsync(int PatientId);
        public Task<ResponseDTO> GetPatientAilmentDetailsAsync(int AilmentId);
        public Task<ResponseDTO> GetAllSymptomsAsync(int SymptomId);
        public Task<ResponseDTO> GetPatientAndDoctorDetailsAsync(int AilmentId, int DoctorId);
        /*public Task<ResponseDTO> AddAilmentImageAsync(AilmentImageRequestDTO ailmentImageRequestDTO);*/
        public Task<ResponseDTO> AddUpdatePatientAsync(PatientSymptomRequestDTO patientSymptomRequestDTO);
        public Task<ResponseDTO> DeletePatientSymptomAsync(int SymptomId, int AilmentId);

    }
}
