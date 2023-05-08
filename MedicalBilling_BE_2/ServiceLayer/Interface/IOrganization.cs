using DTO.ReqDTO;
using Helper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface
{
    public interface IOrganization
    {
        public CommonResponse AddOrganization(AddOrganizationReqDTO addOrganizationReqDTO);

        public CommonResponse GetDetailOrganizationList(GetDetailOrganizationListReqDTO getDetailOrganizationListReqDTO);

        public CommonResponse GetOrganizationDetailById(GetOrganizationDetailByIdReqDTO getOrganizationDetailByIdReqDTO);
        public CommonResponse UpdateOrganization(UpdateOrganizationReqDTO updateOrganizationReqDTO);
        public CommonResponse DeleteOrganization(DeleteOrganizationReqDTO deleteOrganizationReqDTO);
        public CommonResponse GetOrganizationList();

	}
}
