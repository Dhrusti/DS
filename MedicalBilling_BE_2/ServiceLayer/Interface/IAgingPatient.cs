using DTO.ReqDTO;
using Helper.Models;

namespace ServiceLayer.Interface
{
    public interface IAgingPatient
    {
        public CommonResponse AddAgingPatient(AddAgingPatientReqDTO addAgingPatientReqDTO);
    }
}
