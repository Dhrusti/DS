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
    public class OrganizationImpl : IOrganization
    {
        private readonly OrganizationBLL _organizationBLL;

        public OrganizationImpl(OrganizationBLL organizationBLL)
        { 
            _organizationBLL = organizationBLL;
        }

        public CommonResponse AddOrganization(AddOrganizationReqDTO addOrganizationReqDTO)
        { 
            CommonResponse commonResponse = new CommonResponse();
            commonResponse = _organizationBLL.AddOrganization(addOrganizationReqDTO);
            return commonResponse;
            
        }
        public CommonResponse GetDetailOrganizationList(GetDetailOrganizationListReqDTO getDetailOrganizationListReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            commonResponse = _organizationBLL.GetDetailOrganizationList(getDetailOrganizationListReqDTO);
            return commonResponse;
        }
        public CommonResponse GetOrganizationDetailById(GetOrganizationDetailByIdReqDTO getOrganizationDetailByIdReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            commonResponse = _organizationBLL.GetOrganizationDetailById(getOrganizationDetailByIdReqDTO);
            return commonResponse;
        }
        public CommonResponse UpdateOrganization(UpdateOrganizationReqDTO updateOrganizationReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            commonResponse = _organizationBLL.UpdateOrganization(updateOrganizationReqDTO);
            return commonResponse;
        }

        public CommonResponse DeleteOrganization(DeleteOrganizationReqDTO deleteOrganizationReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            commonResponse = _organizationBLL.DeleteOrganization(deleteOrganizationReqDTO);
            return commonResponse;
        }

		public CommonResponse GetOrganizationList()
        {
            return _organizationBLL.GetOrganizationList();
		}
	}
}
