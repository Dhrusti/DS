using DataLayer.Entities;
using DTO.ResDTO;
using Helper;
using Helper.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class RoleBLL
    {
        private readonly CommonHelper _commonHelper;
        private readonly CommonRepo _commonRepo;
        public RoleBLL(CommonRepo commonRepo, CommonHelper commonHelper)
        {
            _commonHelper = commonHelper;
            _commonRepo = commonRepo;
        }
        public CommonResponse GetRoles()
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                List<GetRolesResDTO> getRolesResDTOList = new List<GetRolesResDTO>();
                GetRolesResDTO getRolesResDTO = new GetRolesResDTO();

                int UserId = _commonHelper.GetLoggedInUserId();
                var RoleList = _commonRepo.getAllRoles().Where(x => x.Id != CommonConstant.Super_Admin).ToList();
                if (RoleList.Count > 0)
                {
                    foreach (var item in RoleList)
                    {
                        getRolesResDTO = new GetRolesResDTO();
                        getRolesResDTO.Id = item.Id;
                        getRolesResDTO.RoleName = item.RoleName;
                        getRolesResDTOList.Add(getRolesResDTO);
                    }

                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Success!";
                    commonResponse.Data = getRolesResDTOList;
                }
            }
            catch { throw; }
            return commonResponse;
        }
    }
}
