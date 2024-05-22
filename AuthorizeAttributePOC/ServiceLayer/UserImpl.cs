using BussinessLayer;
using DTO.ReqDTO;
using Helper.CommonHelpers;
using Helper.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class UserImpl : IUser
    {
        private readonly UserBLL _userBLL;
        public UserImpl(UserBLL userBLL)
        {
            _userBLL = userBLL;
        }
        public async Task<CommonResponse> GetAllUserDetailList() => await _userBLL.GetAllUserDetailList();
        public async Task<CommonResponse> AddEditUser(AddEditUserReqDTO request) => await _userBLL.AddEditUser(request);
    }
    public interface IUser
    {
        public Task<CommonResponse> GetAllUserDetailList();
        public Task<CommonResponse> AddEditUser(AddEditUserReqDTO request); 
    }
}
