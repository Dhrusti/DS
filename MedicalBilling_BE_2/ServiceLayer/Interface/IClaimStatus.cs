using DTO.ReqDTO;
using Helper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface
{
    public interface IClaimStatus
    {
        public CommonResponse AddClaimStatus(AddClaimStatusReqDTO addClaimStatusReqDTO);
        public CommonResponse GetAllClaimStatus(GetAllClaimStatusReqDTO getAllClaimStatusReqDTO);
        public CommonResponse GetAllClaimStatusById(GetAllClaimStatusByIdReqDTO getAllClaimStatusbyIdReqDTO);
        //public CommonResponse UpdateClaimStatus(UpdateClaimStatusReqDTO updateClaimStatusReqDTO);

        //public CommonResponse DeleteClaimStatus(DeleteClaimStatusReqDTO deleteClaimStatusReqDTO);
    }
}
