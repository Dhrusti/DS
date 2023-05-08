using DataLayer.Entities;
using DTO.ReqDTO;
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
    public class ClaimBLL
    {
        private readonly MedicalBillingDbContext _dbContext;
        private readonly CommonRepo _commonRepo;
        private readonly CommonHelper _commonHelper;
        private readonly IConfiguration _configuration;


        public ClaimBLL(MedicalBillingDbContext dbcontext, CommonRepo commonRepo, CommonHelper commonHelper, IConfiguration configuration)
        {
            _dbContext = dbcontext;
            _commonHelper = commonHelper;
            _commonRepo = commonRepo;
            _configuration = configuration;
        }

        public CommonResponse GetAllClaim(GetAllClaimReqDTO getAllClaimReqDTO)
        { 
            CommonResponse commonResponse = new CommonResponse();
            GetAllClaimResDTO getAllClaimResDTO = new GetAllClaimResDTO();
            List<ClaimList> claimList = new List<ClaimList>();
          
            try
            {
                claimList = (from u in _commonRepo.getClaimList()
                                     select new { u}).ToList().Select(x => new ClaimList
                                     { 
                                       OrganizationId = x.u.OrganizationId,
                                       CompanyId = x.u.CompanyId,
                                       DepartmentId = x.u.DepartmentId,
                                       PayerId = x.u.PayerId,
                                       PatientId = x.u.PatientId,
                                       PolicyId = x.u.PolicyId,
                                       ClaimCode = x.u.ClaimCode,
                                       ClaimStatusId  =x.u.ClaimStatusId,
                                       LastBillDate = x.u.LastBillDate,
                                       IsActive = x.u.IsActive
                                     }).ToList();

                getAllClaimResDTO.claimLists = claimList;
                getAllClaimResDTO.TotalCount = claimList.Count;
                if (claimList != null)
                {
                    commonResponse.Status = true;
                    commonResponse.StatusCode = System.Net.HttpStatusCode.OK;
                    commonResponse.Data = getAllClaimResDTO;
                    commonResponse.Message = "Get All Claim List Successfully";
                }
                else
                {
                    commonResponse.Message = "No Data Found";
                    commonResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                }
            }
            catch (Exception) { throw; }

            return commonResponse;
        }

        public CommonResponse AddClaim(AddClaimReqDTO addClaimReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
				bool Isduplicate = _commonRepo.getClaimList().FirstOrDefault(x => x.OrganizationId == addClaimReqDTO.OrganizationId && x.CompanyId == addClaimReqDTO.CompanyId && x.DepartmentId == addClaimReqDTO.DepartmentId && x.PayerId == x.PayerId && x.PatientId == addClaimReqDTO.PatientId && x.PolicyId == addClaimReqDTO.PolicyId && x.ClaimCode==addClaimReqDTO.ClaimCode) != null ? true : false;
				commonResponse.Message = Isduplicate == true ? "Claim Already Exists" : "";
				int LoggedInUserId = _commonHelper.GetLoggedInUserId();
				DateTime CurrentDateTime = _commonHelper.GetCurrentDateTime();
                if (!Isduplicate)
                {
					ClaimMst claim = new ClaimMst();
					claim.OrganizationId = addClaimReqDTO.OrganizationId;
					claim.CompanyId = addClaimReqDTO.CompanyId;
					claim.PatientId = addClaimReqDTO.PatientId;
					claim.PolicyId = addClaimReqDTO.PolicyId;
					claim.DepartmentId = addClaimReqDTO.DepartmentId;
					claim.PayerId = addClaimReqDTO.PayerId;
					claim.ClaimCode = addClaimReqDTO.ClaimCode;
					claim.ClaimStatusId = addClaimReqDTO.ClaimStatusId;
					claim.LastBillDate = addClaimReqDTO.LastBillDate;
					claim.IsActive = true;
					claim.IsDelete = false;
					claim.CreatedBy = LoggedInUserId;
					claim.UpdatedBy = LoggedInUserId;
					claim.CreatedDate = CurrentDateTime;
					claim.UpdatedDate = CurrentDateTime;

					_dbContext.ClaimMsts.Add(claim);
					_dbContext.SaveChanges();
					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Message = "Claim Added Successfully!";
					commonResponse.Data = claim.Id;
				}
            }
            catch(Exception) { throw; }

            return commonResponse;
        }

    }
}
