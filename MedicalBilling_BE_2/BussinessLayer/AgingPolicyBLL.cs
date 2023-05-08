using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Helper.Models;
using System.Net;

namespace BussinessLayer
{
	public class AgingPolicyBLL
	{

		private readonly MedicalBillingDbContext _dbContext;
		private readonly CommonRepo _commonRepo;
		private readonly CommonHelper _commonHelper;

		public AgingPolicyBLL(MedicalBillingDbContext dbcontext, CommonRepo commonRepo, CommonHelper commonHelper)
		{
			_dbContext = dbcontext;
			_commonHelper = commonHelper;
			_commonRepo = commonRepo;
		}

		public CommonResponse AddAgingPolicy(AddAgingPolicyReqDTO addAgingPolicyReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				bool Isduplicate = _commonRepo.getPolicyList().FirstOrDefault(x => x.OrganizationId == addAgingPolicyReqDTO.OrganizationId && x.CompanyId == addAgingPolicyReqDTO.CompanyId && x.DepartmentId == addAgingPolicyReqDTO.DepartmentId && x.PayerId == x.PayerId && x.PatientId == addAgingPolicyReqDTO.PatientId && x.PolicyCode == addAgingPolicyReqDTO.PolicyCode) != null ? true : false;
				commonResponse.Message = Isduplicate == true ? "Policy Already Exists" : "";
				int LoggedInUserId = _commonHelper.GetLoggedInUserId();
				DateTime CurrentDateTime = _commonHelper.GetCurrentDateTime();
				if (Isduplicate == false)
				{
					PolicyMst policyMst = new PolicyMst();
					policyMst.OrganizationId = addAgingPolicyReqDTO.OrganizationId;
					policyMst.CompanyId = addAgingPolicyReqDTO.CompanyId;
					policyMst.DepartmentId = addAgingPolicyReqDTO.DepartmentId;
					policyMst.PayerId = addAgingPolicyReqDTO.PayerId;
					policyMst.PatientId = addAgingPolicyReqDTO.PatientId;
					policyMst.PolicyCode = addAgingPolicyReqDTO.PolicyCode;
					policyMst.IsActive = true;
					policyMst.IsDelete = false;
					policyMst.CreatedBy = LoggedInUserId;
					policyMst.UpdatedBy = LoggedInUserId;
					policyMst.CreatedDate = CurrentDateTime;
					policyMst.UpdatedDate = CurrentDateTime;

					_dbContext.PolicyMsts.Add(policyMst);
					_dbContext.SaveChanges();

					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Message = "Policy Added Successfully!";
					commonResponse.Data = policyMst.Id;
				}
			}
			catch { throw; }
			return commonResponse;
		}

	}
}
