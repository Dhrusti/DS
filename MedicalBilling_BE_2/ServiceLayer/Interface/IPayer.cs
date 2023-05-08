using DTO.ReqDTO;
using Helper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface
{
    public interface IPayer
    {
        public CommonResponse GetAllPayer();

        public CommonResponse AddPayer(AddPayerReqDTO addPayerReqDTO);

    }
}
