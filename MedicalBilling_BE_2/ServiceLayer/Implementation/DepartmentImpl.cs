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
    public class DepartmentImpl : IDepartment
    {
        private readonly DepartmentBLL _departmentBLL;
        public DepartmentImpl(DepartmentBLL departmentBLL)
        { 
            _departmentBLL = departmentBLL;
        }

        public CommonResponse AddDepartment(AddDepartmentReqDTO addDepartmentReqDTO)
        { 
            CommonResponse response = new CommonResponse();
            response = _departmentBLL.AddDepartment(addDepartmentReqDTO);
            return response;
        }

        public CommonResponse GetDetailDepartmentList(GetDetailDepartmentListReqDTO getAllDepartmentReqDTO)
        { 
            CommonResponse commonResponse = new CommonResponse();
            commonResponse = _departmentBLL.GetDetailDepartmentList(getAllDepartmentReqDTO);
            return commonResponse;
        }
        public CommonResponse GetDepartmentDetailsById(GetDepartmentDetailsByIdReqDTO getAllDepartmentbyIdReqDTO)
        { 
            CommonResponse commonResponse = new CommonResponse();
            commonResponse = _departmentBLL.GetDepartmentDetailsById(getAllDepartmentbyIdReqDTO);
            return commonResponse;
        }

        public CommonResponse UpdateDepartment(UpdateDepartmentReqDTO updateDepartmentReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            commonResponse = _departmentBLL.UpdateDepartment(updateDepartmentReqDTO);
            return commonResponse;
        } 
        public CommonResponse DeleteDepartment(DeleteDepartmentReqDTO deleteDepartmentReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            commonResponse = _departmentBLL.DeleteDepartment(deleteDepartmentReqDTO);
            return commonResponse;
        }

		public CommonResponse GetDepartmentList()
        {
            return _departmentBLL.GetDepartmentList();
		}

	}
}
