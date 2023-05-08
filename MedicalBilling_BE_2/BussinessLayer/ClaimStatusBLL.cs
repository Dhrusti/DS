using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Helper.Models;
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
	public class ClaimStatusBLL
	{
		private readonly MedicalBillingDbContext _dbContext;
		private readonly CommonRepo _commonRepo;
		private readonly CommonHelper _commonHelper;
		private readonly IConfiguration _configuration;


		public ClaimStatusBLL(MedicalBillingDbContext dbcontext, CommonRepo commonRepo, CommonHelper commonHelper, IConfiguration configuration)
		{
			_dbContext = dbcontext;
			_commonHelper = commonHelper;
			_commonRepo = commonRepo;
			_configuration = configuration;
		}

		public CommonResponse AddClaimStatus(AddClaimStatusReqDTO addClaimStatusReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				bool IsDuplicate = _commonRepo.getClaimStatusList().FirstOrDefault(x => x.ClaimStatusName == addClaimStatusReqDTO.ClaimStatusName) != null ? true : false;
				if (!IsDuplicate)
				{
					ClaimStatusMst claimStatus = new ClaimStatusMst();
					claimStatus.OrganizationId = addClaimStatusReqDTO.OrganizationId;
					claimStatus.CompanyId = addClaimStatusReqDTO.CompanyId;
					claimStatus.DepartmentId = addClaimStatusReqDTO.DepartmentId;
					claimStatus.ClaimStatusName = addClaimStatusReqDTO.ClaimStatusName;
					claimStatus.IsActive = addClaimStatusReqDTO.IsActive;
					claimStatus.IsDelete = false;
					claimStatus.CreatedDate = DateTime.Now;
					claimStatus.UpdatedDate = DateTime.Now;
					claimStatus.CreatedBy = _commonHelper.GetLoggedInUserId();
					claimStatus.UpdatedBy = _commonHelper.GetLoggedInUserId();

					_dbContext.ClaimStatusMsts.Add(claimStatus);
					_dbContext.SaveChanges();

					commonResponse.Status = true;
					commonResponse.StatusCode = System.Net.HttpStatusCode.OK;
					commonResponse.Message = "Claim Status Added Successfully!";
					commonResponse.Data = claimStatus.Id;
				}
				else
				{
					commonResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
					commonResponse.Message = "Claim Status Name is Already Exist!";
				}
			}
			catch { throw; }
			return commonResponse;
		}

		public CommonResponse GetAllClaimStatus(GetAllClaimStatusReqDTO getAllClaimStatusReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			GetAllClaimStatusResDTO getAllClaimStatusResDTO = new GetAllClaimStatusResDTO();
			List<ClaimStatusList> claimStatusLists = new List<ClaimStatusList>();

			int Page = Convert.ToInt32(_configuration.GetSection("ByDefaultPagination:Page").Value);
			int PageSize = Convert.ToInt32(_configuration.GetSection("ByDefaultPagination:PageSize").Value);
			bool OrderBy = Convert.ToBoolean(_configuration.GetSection("ByDefaultPagination:OrderBy").Value);

			Page = getAllClaimStatusReqDTO.PageNumber == 0 ? (Page) : getAllClaimStatusReqDTO.PageNumber;
			PageSize = getAllClaimStatusReqDTO.PageSize == 0 ? (PageSize) : getAllClaimStatusReqDTO.PageSize;
			OrderBy = getAllClaimStatusReqDTO.OrderBy == true ? (OrderBy) : getAllClaimStatusReqDTO.OrderBy;

			try
			{
				claimStatusLists = (from u in _commonRepo.getClaimStatusList()
									select new { u }).ToList().Select(x => new ClaimStatusList
									{
										OrganizationId = x.u.OrganizationId,
										CompanyId = x.u.CompanyId,
										DepartmentId = x.u.DepartmentId,
										ClaimStatusName = x.u.ClaimStatusName,
										IsActive = x.u.IsActive
									}).ToList();

				getAllClaimStatusResDTO.TotalCount = claimStatusLists.Count;
				if (OrderBy)
				{
					if (claimStatusLists.Count <= PageSize)
					{
						claimStatusLists = claimStatusLists.OrderBy(x => x.ClaimStatusName).ToList();
					}
					else
					{
						claimStatusLists = claimStatusLists.Skip((Page - 1) * PageSize)
								.Take(PageSize)
								.OrderBy(x => x.ClaimStatusName)
								.ToList();
					}
				}
				else
				{
					if (claimStatusLists.Count <= PageSize)
					{
						claimStatusLists = claimStatusLists.OrderByDescending(x => x.ClaimStatusName).ToList();
					}
					else
					{
						claimStatusLists = claimStatusLists.OrderByDescending(x => x.ClaimStatusName).Skip((Page - 1) * PageSize)
							.Take(PageSize)
							.ToList();
					}
				}
				getAllClaimStatusResDTO.ClaimStatusLists = claimStatusLists;

				if (claimStatusLists != null)
				{
					commonResponse.Status = true;
					commonResponse.StatusCode = System.Net.HttpStatusCode.OK;
					commonResponse.Message = "GetAll Claim Status Successfully";
					commonResponse.Data = getAllClaimStatusResDTO;
				}
				else
				{
					commonResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
					commonResponse.Message = "Data Not Found";
				}
			}
			catch (Exception) { throw; }
			return commonResponse;
		}
		public CommonResponse GetAllClaimStatusById(GetAllClaimStatusByIdReqDTO getAllClaimStatusByIdReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			GetAllClaimStatusByIdResDTO getAllClaimStatusByIdResDTO = new GetAllClaimStatusByIdResDTO();
			var IsclaimStatus = _commonRepo.getClaimStatusList().FirstOrDefault(x => x.Id == getAllClaimStatusByIdReqDTO.ClaimStatusId);
			// ClaimStatusMst claimStatus = new ClaimStatusMst();
			try
			{

			}
			catch (Exception) { throw; }
			return commonResponse;

		}

	}
}
