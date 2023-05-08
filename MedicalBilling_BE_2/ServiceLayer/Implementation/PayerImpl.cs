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
    public class PayerImpl : IPayer
    {
        private readonly PayerBLL _payerBLL;

        public PayerImpl(PayerBLL payerBLL)
        { 
            _payerBLL = payerBLL;
        }

        public CommonResponse GetAllPayer()
        {
            CommonResponse commonResponse = new CommonResponse();
            commonResponse = _payerBLL.GetAllPayer();
            return commonResponse;
        }
        public CommonResponse AddPayer(AddPayerReqDTO addPayerReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            commonResponse  =_payerBLL.AddPayer(addPayerReqDTO);
            return commonResponse;
        }

    }
}
