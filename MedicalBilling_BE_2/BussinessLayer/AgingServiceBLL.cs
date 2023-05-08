using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Helper.Models;
using System.Net;

namespace BussinessLayer
{
	public class AgingServiceBLL
	{

		private readonly MedicalBillingDbContext _dbContext;
		private readonly CommonRepo _commonRepo;
		private readonly CommonHelper _commonHelper;

		public AgingServiceBLL(MedicalBillingDbContext dbcontext, CommonRepo commonRepo, CommonHelper commonHelper)
		{
			_dbContext = dbcontext;
			_commonHelper = commonHelper;
			_commonRepo = commonRepo;
		}

		public CommonResponse AddAgingService(AddAgingServiceReqDTO addAgingServiceReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				bool Isduplicate = _commonRepo.getServiceList().FirstOrDefault(x => x.OrganizationId == addAgingServiceReqDTO.OrganizationId && x.CompanyId == addAgingServiceReqDTO.CompanyId && x.DepartmentId == addAgingServiceReqDTO.DepartmentId && x.PayerId == x.PayerId && x.PatientId == addAgingServiceReqDTO.PatientId && x.PolicyId == addAgingServiceReqDTO.PolicyId && x.ClaimId == addAgingServiceReqDTO.ClaimId && x.ServiceCpt == addAgingServiceReqDTO.ServiceCpt && x.DateOfService == addAgingServiceReqDTO.DateOfService) != null ? true : false;
				commonResponse.Message = Isduplicate == true ? "Service Already Exists" : "";
				int LoggedInUserId = _commonHelper.GetLoggedInUserId();
				DateTime CurrentDateTime = _commonHelper.GetCurrentDateTime();
				if (Isduplicate == false)
				{
					ServiceMst serviceMst = new ServiceMst();
					serviceMst.OrganizationId = addAgingServiceReqDTO.OrganizationId;
					serviceMst.CompanyId = addAgingServiceReqDTO.CompanyId;
					serviceMst.DepartmentId = addAgingServiceReqDTO.DepartmentId;
					serviceMst.PayerId = addAgingServiceReqDTO.PayerId;
					serviceMst.PatientId = addAgingServiceReqDTO.PatientId;
					serviceMst.PolicyId = addAgingServiceReqDTO.PolicyId;
					serviceMst.ClaimId = addAgingServiceReqDTO.ClaimId;
					serviceMst.DateOfService = addAgingServiceReqDTO.DateOfService;
					serviceMst.ServiceCpt = addAgingServiceReqDTO.ServiceCpt;
					serviceMst.ServiceCode = addAgingServiceReqDTO.ServiceCode;
					serviceMst.Modifier = addAgingServiceReqDTO.Modifier;
					serviceMst.DiagnosisCode1 = addAgingServiceReqDTO.DiagnosisCode1;
					serviceMst.DiagnosisCode2 = addAgingServiceReqDTO.DiagnosisCode2;
					serviceMst.DiagnosisCode3 = addAgingServiceReqDTO.DiagnosisCode3;
					serviceMst.DiagnosisCode4 = addAgingServiceReqDTO.DiagnosisCode4;
					serviceMst.Cob = addAgingServiceReqDTO.Cob;
					serviceMst.InsuranceAmount1 = addAgingServiceReqDTO.InsuranceAmount1;
					serviceMst.InsuranceAmount2 = addAgingServiceReqDTO.InsuranceAmount2;
					serviceMst.InsuranceAmount3 = addAgingServiceReqDTO.InsuranceAmount3;
					serviceMst.InsuranceAmount4 = addAgingServiceReqDTO.InsuranceAmount4;
					serviceMst.ChargeAmount = addAgingServiceReqDTO.ChargeAmount;
					serviceMst.LineItemAmount = addAgingServiceReqDTO.LineItemAmount;
					serviceMst.IsActive = true;
					serviceMst.IsDelete = false;
					serviceMst.CreatedBy = LoggedInUserId;
					serviceMst.UpdatedBy = LoggedInUserId;
					serviceMst.CreatedDate = CurrentDateTime;
					serviceMst.UpdatedDate = CurrentDateTime;
					
					_dbContext.ServiceMsts.Add(serviceMst);
					_dbContext.SaveChanges();

					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Message = "Service Added Successfully!";
					commonResponse.Data = serviceMst.Id;
				}
			}
			catch { throw; }
			return commonResponse;
		}

	}
}
