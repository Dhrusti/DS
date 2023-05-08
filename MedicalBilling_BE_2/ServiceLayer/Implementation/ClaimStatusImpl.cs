using BussinessLayer;
using DTO.ReqDTO;
using Helper.Models;
using ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Implementation
{
    public class ClaimStatusImpl : IClaimStatus
    {
        private readonly ClaimStatusBLL _claimStatusBLL;

        public ClaimStatusImpl(ClaimStatusBLL claimStatusBLL)
        { 
            _claimStatusBLL = claimStatusBLL;
        }

        public CommonResponse AddClaimStatus(AddClaimStatusReqDTO addClaimStatusReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            commonResponse = _claimStatusBLL.AddClaimStatus(addClaimStatusReqDTO);
            return commonResponse;
            
        }
        public CommonResponse GetAllClaimStatus(GetAllClaimStatusReqDTO getAllClaimStatusReqDTO)
        { 
            CommonResponse commonResponse = new CommonResponse();
            commonResponse = _claimStatusBLL.GetAllClaimStatus(getAllClaimStatusReqDTO);
            return commonResponse;
        }
        public CommonResponse GetAllClaimStatusById(GetAllClaimStatusByIdReqDTO getAllClaimStatusByIdReqDTO)
        { 
            CommonResponse commonResponse = new CommonResponse();
            commonResponse = _claimStatusBLL.GetAllClaimStatusById(getAllClaimStatusByIdReqDTO);
            return commonResponse;
        }
        //public CommonResponse UpdateClaimStatus(UpdateClaimStatusReqDTO updateClaimStatusReqDTO)
        //{
        //    CommonResponse commonResponse = new CommonResponse();
        //    commonResponse = _claimStatusBLL.UpdateClaimStatus(updateClaimStatusReqDTO);
        //    return commonResponse;
        //}

        //public CommonResponse DeleteClaimStatus(DeleteClaimStatusReqDTO deleteClaimStatusReqDTO)
        //{ 
        //    CommonResponse commonResponse= new CommonResponse();
        //    commonResponse = _claimStatusBLL.DeleteClaimStatus(deleteClaimStatusReqDTO);
        //    return commonResponse;
        //}

    }
}
