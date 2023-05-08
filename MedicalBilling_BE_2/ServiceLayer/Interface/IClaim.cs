using DTO.ReqDTO;
using Helper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface
{
    public interface IClaim
    {
        public CommonResponse GetAllClaim(GetAllClaimReqDTO getAllClaimReqDTO);
        public CommonResponse AddClaim(AddClaimReqDTO addClaimReqDTO);
    }
}
