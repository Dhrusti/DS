using BussinessLayer;
using DTO.ReqDTO;
using Helper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface
{
    public interface ICompany
    {

        public CommonResponse AddCompany(AddCompanyReqDTO addCompanyReqDTO);
        public CommonResponse GetDetailCompanyList(GetDetailCompanyListReqDTO getAllCompanyReqDTO);
        public CommonResponse GetCompanyDetailById(GetCompanyDetailByIdReqDTO getAllCompanyByIdReqDTO);
        public CommonResponse UpdateCompany(UpdateCompanyReqDTO updateCompanyReqDTO);
        public CommonResponse DeleteCompany(DeleteCompanyReqDTO deleteCompanyReqDTO);
        public CommonResponse GetCompanyList();
	}
}
