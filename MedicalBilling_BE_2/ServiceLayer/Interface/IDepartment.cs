using DTO.ReqDTO;
using Helper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface
{
    public interface IDepartment
    {
        public CommonResponse AddDepartment(AddDepartmentReqDTO addDepartmentReqDTO);
        public CommonResponse GetDetailDepartmentList(GetDetailDepartmentListReqDTO getAllDepartmentReqDTO);
        public CommonResponse GetDepartmentDetailsById(GetDepartmentDetailsByIdReqDTO getAllDepartmentbyIdReqDTO);
        public CommonResponse UpdateDepartment(UpdateDepartmentReqDTO updateDepartmentReqDTO);
        public CommonResponse DeleteDepartment(DeleteDepartmentReqDTO deleteDepartmentReqDTO);

        public CommonResponse GetDepartmentList();

	}
}
