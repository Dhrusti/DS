using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Helper.Models;
using System.Net;

namespace BussinessLayer
{
	public class AgingPatientBLL
	{

		private readonly MedicalBillingDbContext _dbContext;
		private readonly CommonRepo _commonRepo;
		private readonly CommonHelper _commonHelper;



		public AgingPatientBLL(MedicalBillingDbContext dbcontext, CommonRepo commonRepo, CommonHelper commonHelper)
		{
			_dbContext = dbcontext;
			_commonHelper = commonHelper;
			_commonRepo = commonRepo;
		}

		public CommonResponse AddAgingPatient(AddAgingPatientReqDTO addAgingPatientReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				bool IsDuplicate = _commonRepo.getPatientMsts().FirstOrDefault(x => x.OrganizationId == addAgingPatientReqDTO.OrganizationId && x.CompanyId == addAgingPatientReqDTO.CompanyId && x.DepartmentId == addAgingPatientReqDTO.DepartmentId && x.PayerId == addAgingPatientReqDTO.PayerId && x.PatientName.ToLower() == addAgingPatientReqDTO.PatientName.ToLower()) != null ? true : false;
				commonResponse.Message = IsDuplicate == true ? "Patient Already Exists" : "";
				if (!IsDuplicate)
				{
					PatientMst patientMst = new PatientMst();
					patientMst.OrganizationId = addAgingPatientReqDTO.OrganizationId;
					patientMst.CompanyId = addAgingPatientReqDTO.CompanyId;
					patientMst.DepartmentId = addAgingPatientReqDTO.DepartmentId;
					patientMst.PayerId = addAgingPatientReqDTO.PayerId;
					patientMst.Address = addAgingPatientReqDTO.Address;
					patientMst.Phone = addAgingPatientReqDTO.Phone;
					patientMst.Mobile = addAgingPatientReqDTO.Mobile;
					patientMst.Email = addAgingPatientReqDTO.Email;
					patientMst.RenderingFullName = addAgingPatientReqDTO.RenderingFullName;
					patientMst.RefferingFullName = addAgingPatientReqDTO.RefferingFullName;
					patientMst.PatientName = addAgingPatientReqDTO.PatientName;
					patientMst.PatientCode = addAgingPatientReqDTO.PatientCode;
					patientMst.PatientDob = addAgingPatientReqDTO.PatientDob;
					patientMst.MedicalRecordCode = addAgingPatientReqDTO.MedicalRecordCode;
					patientMst.Eaibcode = addAgingPatientReqDTO.Eaibcode;
					patientMst.IsActive = true;
					patientMst.IsDelete = false;
					patientMst.CreatedBy = _commonHelper.GetLoggedInUserId();
					patientMst.UpdatedBy = _commonHelper.GetLoggedInUserId();
					patientMst.CreatedDate = _commonHelper.GetCurrentDateTime();
					patientMst.UpdatedDate = _commonHelper.GetCurrentDateTime();

					_dbContext.PatientMsts.Add(patientMst);
					_dbContext.SaveChanges();

					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Message = "Patient Added Successfully!";
					commonResponse.Data = patientMst.Id;
				}
			}
			catch { throw; }
			return commonResponse;
		}
	}
}
