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
    public class CompanyImpl : ICompany
    {

        private readonly CompanyBLL _companyBLL;

        public CompanyImpl(CompanyBLL companyBLL)
        { 
            _companyBLL = companyBLL;
        }
        public CommonResponse AddCompany(AddCompanyReqDTO addCompanyReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            commonResponse = _companyBLL.AddCompany(addCompanyReqDTO);
            return commonResponse;
        }
      
        public CommonResponse GetDetailCompanyList(GetDetailCompanyListReqDTO getAllCompanyReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            commonResponse = _companyBLL.GetDetailCompanyList(getAllCompanyReqDTO);
            return commonResponse;
        }

        public CommonResponse GetCompanyDetailById(GetCompanyDetailByIdReqDTO getAllCompanyByIdReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            commonResponse = _companyBLL.GetCompanyDetailById(getAllCompanyByIdReqDTO);
            return commonResponse;
        }

        public CommonResponse UpdateCompany(UpdateCompanyReqDTO updateCompanyReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            commonResponse = _companyBLL.UpdateCompany(updateCompanyReqDTO);
            return commonResponse;
        }

        public CommonResponse DeleteCompany(DeleteCompanyReqDTO deleteCompanyReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            commonResponse = _companyBLL.DeleteCompany(deleteCompanyReqDTO);
            return commonResponse;
        }

		public CommonResponse GetCompanyList()
        {
            return _companyBLL.GetCompanyList();
        }

	}
}
