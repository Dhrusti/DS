using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Helper.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
	public class PayerBLL
	{
		private readonly MedicalBillingDbContext _dbContext;
		private readonly CommonRepo _commonRepo;
		private readonly CommonHelper _commonHelper;
		private readonly IConfiguration _configuration;


		public PayerBLL(MedicalBillingDbContext dbcontext, CommonRepo commonRepo, CommonHelper commonHelper, IConfiguration configuration)
		{
			_dbContext = dbcontext;
			_commonHelper = commonHelper;
			_commonRepo = commonRepo;
			_configuration = configuration;
		}


		public CommonResponse GetAllPayer()
		{
			CommonResponse commonResponse = new CommonResponse();
			List<GetAllPayerResDTO> getAllPayerResDTO = new List<GetAllPayerResDTO>();
			PayerMst payerMst = new PayerMst();
			try
			{
				var payerList = _commonRepo.getAllPayer;
				getAllPayerResDTO = (from u in _commonRepo.getAllPayer()
									 select new { u }).Select(x => new GetAllPayerResDTO
									 {
										 Id = x.u.Id,
										 PayerName = x.u.PayerName,
										 PayerCode = x.u.PayerCode
									 }).ToList();

				if (getAllPayerResDTO != null)
				{
					commonResponse.Status = true;
					commonResponse.StatusCode = System.Net.HttpStatusCode.OK;
					commonResponse.Message = "GetAll Payer Successfully";
					commonResponse.Data = getAllPayerResDTO;
				}
				else
				{
					commonResponse.Message = "No Data Found";
					commonResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
				}

			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		public CommonResponse AddPayer(AddPayerReqDTO addPayerReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			var duplicatePayer = _commonRepo.getAllPayer().FirstOrDefault(x => x.PayerName == addPayerReqDTO.PayerName || x.PayerCode == addPayerReqDTO.PayerCode);
			bool checkNull = (string.IsNullOrWhiteSpace(addPayerReqDTO.PayerName) && string.IsNullOrWhiteSpace(addPayerReqDTO.PayerCode));
			try
			{
				if (checkNull == false)
				{
					if (duplicatePayer == null)
					{
						PayerMst payer = new PayerMst();
						payer.PayerName = addPayerReqDTO.PayerName;
						payer.PayerCode = addPayerReqDTO.PayerCode;
						payer.Address = addPayerReqDTO.Address;
						payer.Componant = addPayerReqDTO.Componant;
						payer.Mobile = addPayerReqDTO.Mobile;
						payer.Phone = addPayerReqDTO.Phone;
						payer.Email = addPayerReqDTO.Email;
						payer.Website = addPayerReqDTO.Website;
						payer.IsActive = true;
						payer.IsDelete = false;
						payer.CreatedBy = _commonHelper.GetLoggedInUserId();
						payer.UpdatedBy = _commonHelper.GetLoggedInUserId();
						payer.CreatedDate = _commonHelper.GetCurrentDateTime();
						payer.UpdatedDate = _commonHelper.GetCurrentDateTime();

						_dbContext.PayerMsts.Add(payer);
						_dbContext.SaveChanges();

						commonResponse.Status = true;
						commonResponse.StatusCode = System.Net.HttpStatusCode.OK;
						commonResponse.Message = "Add Payer Successfully!";
						commonResponse.Data = payer.Id;
					}
					else
					{
						commonResponse.Message = "PayerName Or PayerCode Already Exist";
						commonResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
					}
				}
				else
				{
					commonResponse.Message = "PayerName Or PayerCode Cannot be Null";
					commonResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
				}

			}
			catch { throw; }
			return commonResponse;
		}

	}
}
