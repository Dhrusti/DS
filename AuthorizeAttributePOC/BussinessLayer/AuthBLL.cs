using DataLayer.Entities;
using DTO.ReqDTO;
using Helper.CommonHelpers;
using Helper.CommonModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class AuthBLL
    {
        private readonly AuthHelper _authHelper;
        private readonly CommonRepo _commonRepo;
        private readonly DBContext _dbContext;
        private readonly CommonHelper _commonHelper;
        private readonly IConfiguration _configuration;

        public AuthBLL(AuthHelper authHelper, CommonRepo commonRepo, DBContext dbContext, CommonHelper commonHelper, IConfiguration configuration)
        {
            _authHelper = authHelper;
            _commonRepo = commonRepo;
            _dbContext = dbContext;
            _commonHelper = commonHelper;
            _configuration = configuration;
        }
        public async Task<CommonResponse> Login(LoginReqDTO request)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                response = await _authHelper.Login(request);
            }
            catch { throw; }
            return response;
        }
    }
}
