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
    public class ClaimImpl : IClaim
    {
        private readonly ClaimBLL _claimBLL;

        public ClaimImpl(ClaimBLL claimBLL)
        { 
            _claimBLL = claimBLL;
        }

        public CommonResponse GetAllClaim(GetAllClaimReqDTO getAllClaimReqDTO)
        { 
            CommonResponse commonResponse = new CommonResponse();
            commonResponse = _claimBLL.GetAllClaim(getAllClaimReqDTO);
            return commonResponse;
        }

        public CommonResponse AddClaim(AddClaimReqDTO addClaimReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            commonResponse = _claimBLL.AddClaim(addClaimReqDTO);
            return commonResponse;
        }
    }
}
