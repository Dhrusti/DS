using BusinessLayer;
using DTO;
using ServiceLayer.IService;

namespace ServiceLayer.ServiceImpl
{
    public class UserImpl : IUser
    {
        private readonly UserBLL _userBLL;
        public UserImpl(UserBLL userBLL)
        {
            _userBLL = userBLL;
        }

        public async Task<ResponseDTO> GetAllUsersAsync()
        {
            return await _userBLL.GetAllUsersAsync();
        }

        public async Task<ResponseDTO> GetAllDoctorNameAsync()
        {
            return await _userBLL.GetAllDoctorNameAsync();
        }

        public async Task<ResponseDTO> GetAllPatientFirstNameAsync(string FirstName)
        {
            return await _userBLL.GetAllPatientFirstNameAsync(FirstName);
        }

        public async Task<ResponseDTO> GetAllPatientLastNameAsync(string FirstName)
        {
            return await _userBLL.GetAllPatientLastNameAsync(FirstName);
        }

        public async Task<ResponseDTO> GetPatientDetailsAsync(int PatientId)
        {
            return await _userBLL.GetPatientDetailsAsync(PatientId);
        }

        public async Task<ResponseDTO> GetPatientAilmentDetailsAsync(int AilmentId)
        {
            return await _userBLL.GetPatientAilmentDetailsAsync(AilmentId);
        }

        public async Task<ResponseDTO> GetAllSymptomsAsync(int SymptomId)
        {
            return await _userBLL.GetAllSymptomsAsync(SymptomId);
        }

        public async Task<ResponseDTO> GetPatientAndDoctorDetailsAsync(int AilmentId, int DoctorId)
        {
            return await _userBLL.GetPatientAndDoctorDetailsAsync(AilmentId, DoctorId);
        }

        /*public async Task<ResponseDTO> AddAilmentImageAsync(AilmentImageRequestDTO ailmentImageRequestDTO)
        {
            return await _userBLL.AddAilmentImageAsync(ailmentImageRequestDTO);
        }*/

        public async Task<ResponseDTO> AddUpdatePatientAsync(PatientSymptomRequestDTO patientSymptomRequestDTO)
        {
            return await _userBLL.AddUpdatePatientAsync(patientSymptomRequestDTO);
        }

        public async Task<ResponseDTO> DeletePatientSymptomAsync(int SymptomId, int AilmentId)
        {
            return await _userBLL.DeletePatientSymptomAsync(SymptomId, AilmentId);
        }

        /*public async Task<ResponseDTO> AddUpdatePatientAsync(PatientRequestDTO patientRequestDTO)
        {
            return await _userBLL.AddUpdatePatientAsync(patientRequestDTO);
        }*/
    }
}
