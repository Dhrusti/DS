using DTO;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.IService;
using System.Web;
using Users.Microservice.Helper;

namespace Users.Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _iUser;
        private readonly IConfiguration _iConfiguration;
        public UserController(IUser iUser, IConfiguration iConfiguration)
        {
            this._iUser = iUser;
            this._iConfiguration = iConfiguration;
        }

        [HttpGet]
        [Route("GetEncrypt")]
        public ResponseDTO GetEncrypt(string Name)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            responseDTO.Message = new EncryptionDecryptionHelper(_iConfiguration).EncryptString(Name);
            return responseDTO;
        }
        
        [HttpGet]
        [Route("GetDecrypt")]
        public ResponseDTO GetDecrypt(string Name)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            responseDTO.Message = new EncryptionDecryptionHelper(_iConfiguration).DecryptString(Name);
            return responseDTO;
        }
        
        [HttpGet]
        [Route("GetAllUsersAsync")]
        public async Task<ResponseDTO> GetAllUsersAsync()
        {
            return await _iUser.GetAllUsersAsync();
        }

        [HttpGet]
        [Route("GetAllDoctorNameAsync")]
        public async Task<ResponseDTO> GetAllDoctorNameAsync()
        {
            return await _iUser.GetAllDoctorNameAsync();
        }

        [HttpGet]
        [Route("GetAllPatientFirstNameAsync")]
        public async Task<ResponseDTO> GetAllPatientFirstNameAsync(string FirstName)
        {
            return await _iUser.GetAllPatientFirstNameAsync(FirstName);
        }

        [HttpGet]
        [Route("GetAllPatientLastNameAsync")]
        public async Task<ResponseDTO> GetAllPatientLastNameAsync(string FirstName)
        {
            return await _iUser.GetAllPatientLastNameAsync(FirstName);
        }

        [HttpGet]
        [Route("GetPatientDetailsAsync")]
        public async Task<ResponseDTO> GetPatientDetailsAsync(int PatientId)
        {
            return await _iUser.GetPatientDetailsAsync(PatientId);
        }

        [HttpGet]
        [Route("GetPatientAilmentDetailsAsync")]
        public async Task<ResponseDTO> GetPatientAilmentDetailsAsync(int AilmentId)
        {
            return await _iUser.GetPatientAilmentDetailsAsync(AilmentId);
        }

        [HttpGet]
        [Route("GetAllSymptomsAsync")]
        public async Task<ResponseDTO> GetAllSymptomsAsync(int SymptomId)
        {
            return await _iUser.GetAllSymptomsAsync(SymptomId);
        }

        [HttpGet]
        [Route("GetPatientAndDoctorDetailsAsync")]
        public async Task<ResponseDTO> GetPatientAndDoctorDetailsAsync(int AilmentId, int DoctorId)
        {
            return await _iUser.GetPatientAndDoctorDetailsAsync(AilmentId, DoctorId);
        }

        /* [HttpPost]
         [Route("AddUpdatePatientAsync")]
         public async Task<ResponseDTO> AddUpdatePatientAsync(PatientRequestDTO patientRequestDTO)
         {
             return await _iUser.AddUpdatePatientAsync(patientRequestDTO);
         }*/

        /*[HttpPost]
        [Route("AddAilmentImageAsync")]
        public async Task<ResponseDTO> AddAilmentImageAsync(AilmentImageRequestDTO ailmentImageRequestDTO)
        {
            return await _iUser.AddAilmentImageAsync(ailmentImageRequestDTO);
        }*/

        [HttpPost]
        [Route("AddUpdatePatientAsync")]
        public async Task<ResponseDTO> AddUpdatePatientAsync(PatientSymptomRequestDTO patientSymptomRequestDTO)
        {
            return await _iUser.AddUpdatePatientAsync(patientSymptomRequestDTO);
        }

        [HttpDelete]
        [Route("DeletePatientSymptomAsync")]
        public async Task<ResponseDTO> DeletePatientSymptomAsync(int SymptomId, int AilmentId)
        {
            return await _iUser.DeletePatientSymptomAsync(SymptomId,AilmentId);
        }
    }
}
