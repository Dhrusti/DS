using BussinessLayer;
using DTO.ReqDTO;
using Helper.Models;
using ServiceLayer.Interface;

namespace ServiceLayer.Implementation
{
    public class AgingPatientImpl : IAgingPatient
    {
        private readonly AgingPatientBLL _agingPatientBLL;
        public AgingPatientImpl(AgingPatientBLL agingPatientBLL)
        {
            _agingPatientBLL = agingPatientBLL;
        }

        public CommonResponse AddAgingPatient(AddAgingPatientReqDTO addAgingPatientReqDTO) => _agingPatientBLL.AddAgingPatient(addAgingPatientReqDTO);


    }
}
