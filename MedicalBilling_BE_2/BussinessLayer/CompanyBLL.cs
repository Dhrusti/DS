using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Helper.Models;
using Mapster;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using System.Net;
using static DTO.ResDTO.GetAllAdminAppointmentResDTO;
using static DTO.ResDTO.GetAllAppointmentByLocalSearchResDTO;
using static DTO.ResDTO.GetAllAppointmentResDTO;
using static DTO.ResDTO.GetDetailCompanyListResDTO;
using static DTO.ResDTO.RemarksResDTO;

namespace BussinessLayer
{
	public class CompanyBLL
	{
		private readonly MedicalBillingDbContext _dbContext;
		private readonly CommonRepo _commonRepo;
		private readonly CommonHelper _commonHelper;
		private readonly IConfiguration _configuration;


		public CompanyBLL(MedicalBillingDbContext dbcontext, CommonRepo commonRepo, CommonHelper commonHelper, IConfiguration configuration)
		{
			_dbContext = dbcontext;
			_commonHelper = commonHelper;
			_commonRepo = commonRepo;
			_configuration = configuration;
		}
		public CommonResponse AddCompany(AddCompanyReqDTO addCompanyReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			AddCompanyResDTO addCompanyResDTO = new AddCompanyResDTO();
			CompanyMst company = new CompanyMst();
			var duplicatecompany = _commonRepo.getAllCompany().Where(x => x.CompanyDisplayName == addCompanyReqDTO.CompanyDisplayName || x.CompanyName == addCompanyReqDTO.CompanyName).ToList();
			var isEmptyCompany = (string.IsNullOrWhiteSpace(addCompanyReqDTO.CompanyName) || string.IsNullOrWhiteSpace(addCompanyReqDTO.CompanyDisplayName));
			try
			{
				if (_commonRepo.HasPermission(CommonConstant.Aging_Company_Add))
				{
					if (isEmptyCompany == false)
					{
						if (duplicatecompany.Count == 0)
						{

							company.OrganizationId = addCompanyReqDTO.OrganizationId;
							company.CompanyName = addCompanyReqDTO.CompanyName;
							company.CompanyDisplayName = addCompanyReqDTO.CompanyDisplayName;
							company.Address = addCompanyReqDTO.Address;
							company.Email = addCompanyReqDTO.Email;
							company.Bcn = addCompanyReqDTO.Bcn;
							company.ZipCode = addCompanyReqDTO.ZipCode;
							company.FaxNo = addCompanyReqDTO.FaxNo;
							company.Phone = addCompanyReqDTO.Phone;
							company.Mobile = addCompanyReqDTO.Mobile;
							company.Website = addCompanyReqDTO.Website;
							company.Npi = addCompanyReqDTO.Npi;
							company.Sonarx = addCompanyReqDTO.Sonarx;
							company.TaxId = addCompanyReqDTO.TaxId;
							company.IsActive = addCompanyReqDTO.IsActive;
							company.IsDelete = false;
							company.CreatedBy = 1;
							company.UpdatedBy = 0;
							company.CreatedDate = DateTime.Now;
							company.UpdatedDate = DateTime.Now;

							_dbContext.CompanyMsts.Add(company);
							var result = _dbContext.SaveChanges();

							addCompanyResDTO.CompanyId = company.Id;
							addCompanyResDTO.CompanyName = company.CompanyName;
							addCompanyResDTO.CompanyDisplayName = company.CompanyDisplayName;

							if (result != null)
							{
								commonResponse.StatusCode = HttpStatusCode.OK;
								commonResponse.Message = "Company Added Successfully";
								commonResponse.Status = true;
								commonResponse.Data = addCompanyResDTO;
							}
							else
							{
								commonResponse.Message = "Fail";
								commonResponse.StatusCode = HttpStatusCode.BadRequest;
							}
						}
						else
						{
							commonResponse.StatusCode = HttpStatusCode.BadRequest;
							commonResponse.Message = "Companyname or CompanyDisplayname is Already Exist";
						}
					}
					else
					{
						commonResponse.Message = "Companyname or CompanyDisplayName cannot be null";
						commonResponse.StatusCode = HttpStatusCode.BadRequest;
					}
				}
				else
				{
					commonResponse.StatusCode = HttpStatusCode.Forbidden;
					commonResponse.Message = "You Don't Have Permission To Access this!";
				}

			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}


		public CommonResponse GetDetailCompanyList(GetDetailCompanyListReqDTO getAllCompanyReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			GetDetailCompanyListResDTO getAllCompanyResDTO = new GetDetailCompanyListResDTO();

			List<CompanyList> companyList = new List<CompanyList>();

			int Page = Convert.ToInt32(_configuration.GetSection("ByDefaultPagination:Page").Value);
			int PageSize = Convert.ToInt32(_configuration.GetSection("ByDefaultPagination:PageSize").Value);
			bool OrderBy = Convert.ToBoolean(_configuration.GetSection("ByDefaultPagination:OrderBy").Value);

			Page = getAllCompanyReqDTO.PageNumber == 0 ? (Page) : getAllCompanyReqDTO.PageNumber;
			PageSize = getAllCompanyReqDTO.PageSize == 0 ? (PageSize) : getAllCompanyReqDTO.PageSize;
			OrderBy = getAllCompanyReqDTO.Orderby == true ? (OrderBy) : getAllCompanyReqDTO.Orderby;
			try
			{
				if (_commonRepo.HasPermission(CommonConstant.Aging_Company_View))
				{
					companyList = (from u in _commonRepo.getAllCompany()
								   select new { u }).ToList().Select((x, Index) => new CompanyList
								   {
									   SrNo = Index + 1,
									   CompanyId = x.u.Id,
									   OrganizationId = x.u.OrganizationId,
									   CompanyName = x.u.CompanyName,
									   CompanyDisplayName = x.u.CompanyDisplayName,
									   Address = x.u.Address,
									   Phone = x.u.Phone,
									   Mobile = x.u.Mobile,
									   Email = x.u.Email,
									   Website = x.u.Website,
									   FaxNo = x.u.FaxNo,
									   ZipCode = x.u.ZipCode,
									   Npi = x.u.Npi,
									   Bcn = x.u.Bcn,
									   Sonarx = x.u.Sonarx,
									   TaxId = x.u.TaxId,
									   IsActive = x.u.IsActive
								   }).ToList();

					getAllCompanyResDTO.TotalCount = companyList.Count();

					if (OrderBy)
					{
						if (companyList.Count <= PageSize)
						{
							companyList = companyList.OrderBy(x => x.OrganizationId).ToList();
						}
						else
						{
							companyList = companyList.Skip((Page - 1) * PageSize)
									.Take(PageSize)
									.OrderBy(x => x.OrganizationId)
									.ToList();
						}
					}
					else
					{
						if (companyList.Count <= PageSize)
						{
							companyList = companyList.OrderByDescending(x => x.OrganizationId).ToList();
						}
						else
						{
							companyList = companyList.OrderByDescending(x => x.OrganizationId).Skip((Page - 1) * PageSize)
								.Take(PageSize)
								.ToList();
						}
					}
					if (getAllCompanyReqDTO.GlobalSearch != null && !string.IsNullOrWhiteSpace(getAllCompanyReqDTO.GlobalSearch))
					{
						companyList = companyList.Where(x => x.CompanyName.ToLower().Contains(getAllCompanyReqDTO.GlobalSearch.ToLower()) || x.CompanyDisplayName.ToLower().Contains(getAllCompanyReqDTO.GlobalSearch.ToLower())).ToList();
					}


					getAllCompanyResDTO.companyList = companyList;


					if (companyList.Count > 0)
					{
						commonResponse.Status = true;
						commonResponse.StatusCode = HttpStatusCode.OK;
						commonResponse.Message = "GetAll CompanyList Successfully";
						commonResponse.Data = getAllCompanyResDTO;
					}
					else
					{
						commonResponse.StatusCode = HttpStatusCode.NotFound;
						commonResponse.Message = "No Data Found";
						commonResponse.Data = getAllCompanyResDTO;
					}
				}
				else
				{
					commonResponse.StatusCode = HttpStatusCode.Forbidden;
					commonResponse.Message = "You Don't Have Permission To Access this!";
				}

			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		public CommonResponse GetCompanyDetailById(GetCompanyDetailByIdReqDTO getAllCompanyByIdReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			GetCompanyDetailByIdResDTO getAllCompanyResDTO = new GetCompanyDetailByIdResDTO();
			var company = _commonRepo.getAllCompany().FirstOrDefault(x => x.Id == getAllCompanyByIdReqDTO.CompanyId);
			try
			{
				if (company != null)
				{
					getAllCompanyResDTO.OrganizationId = company.OrganizationId;
					getAllCompanyResDTO.CompanyId = company.Id;
					getAllCompanyResDTO.CompanyName = company.CompanyName;
					getAllCompanyResDTO.CompanyDisplayName = company.CompanyDisplayName;
					getAllCompanyResDTO.Phone = company.Phone;
					getAllCompanyResDTO.Mobile = company.Mobile;
					getAllCompanyResDTO.Address = company.Address;
					getAllCompanyResDTO.Website = company.Website;
					getAllCompanyResDTO.Email = company.Email;
					getAllCompanyResDTO.Bcn = company.Bcn;
					getAllCompanyResDTO.ZipCode = company.ZipCode;
					getAllCompanyResDTO.FaxNo = company.FaxNo;
					getAllCompanyResDTO.Sonarx = company.Sonarx;
					getAllCompanyResDTO.Npi = company.Npi;
					getAllCompanyResDTO.TaxId = company.TaxId;
					getAllCompanyResDTO.IsActive = company.IsActive;

					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Data = getAllCompanyResDTO;
					commonResponse.Message = "GetAll Company Successfully";
				}
				else
				{
					commonResponse.StatusCode = HttpStatusCode.NotFound;
					commonResponse.Message = "Data Not Found";
				}

			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		public CommonResponse UpdateCompany(UpdateCompanyReqDTO updateCompanyReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				if (_commonRepo.HasPermission(CommonConstant.Aging_Company_Update))
				{
					var companyList = _commonRepo.getAllCompany().FirstOrDefault(x => x.Id == updateCompanyReqDTO.CompanyId);
					var checkCompany = _commonRepo.getAllCompany().Where(x => x.Id != updateCompanyReqDTO.CompanyId);
					var duplicatecompany = checkCompany.FirstOrDefault(x => x.CompanyDisplayName == updateCompanyReqDTO.CompanyDisplayName || x.CompanyName == updateCompanyReqDTO.CompanyName);
					var isEmptycompany = (string.IsNullOrWhiteSpace(updateCompanyReqDTO.CompanyName) || string.IsNullOrWhiteSpace(updateCompanyReqDTO.CompanyDisplayName));

					if (companyList != null)
					{
						if (isEmptycompany == false)
						{
							if (duplicatecompany == null)
							{
								companyList.OrganizationId = updateCompanyReqDTO.OrganizationId;
								companyList.CompanyName = updateCompanyReqDTO.CompanyName;
								companyList.CompanyDisplayName = updateCompanyReqDTO.CompanyDisplayName;
								companyList.Address = updateCompanyReqDTO.Address;
								companyList.Email = updateCompanyReqDTO.Email;
								companyList.Mobile = updateCompanyReqDTO.Mobile;
								companyList.Phone = updateCompanyReqDTO.Phone;
								companyList.Bcn = updateCompanyReqDTO.Bcn;
								companyList.ZipCode = updateCompanyReqDTO.ZipCode;
								companyList.FaxNo = updateCompanyReqDTO.FaxNo;
								companyList.Npi = updateCompanyReqDTO.Npi;
								companyList.TaxId = updateCompanyReqDTO.TaxId;
								companyList.Website = updateCompanyReqDTO.Website;
								companyList.IsActive = updateCompanyReqDTO.IsActive;
								companyList.UpdatedDate = DateTime.Now;
								companyList.UpdatedBy = _commonHelper.GetLoggedInUserId();

								_dbContext.CompanyMsts.Update(companyList);
								_dbContext.SaveChanges();

								commonResponse.Status = true;
								commonResponse.StatusCode = HttpStatusCode.OK;
								commonResponse.Data = companyList.Id;
								commonResponse.Message = "Company Updated Successfully";
							}
							else
							{
								commonResponse.StatusCode = HttpStatusCode.BadRequest;
								commonResponse.Message = "Companyname or CompanyDisplayname is Already Exist";
							}
						}
						else
						{
							commonResponse.StatusCode = HttpStatusCode.BadRequest;
							commonResponse.Message = "CompanyName or CompanyDisplayName cannot be null";
						}
					}
					else
					{
						commonResponse.StatusCode = HttpStatusCode.BadRequest;
						commonResponse.Message = "Data Not Found";
					}
				}
				else
				{
					commonResponse.StatusCode = HttpStatusCode.Forbidden;
					commonResponse.Message = "You Don't Have Permission To Access this!";
				}


			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		public CommonResponse DeleteCompany(DeleteCompanyReqDTO deleteCompanyReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				if (_commonRepo.HasPermission(CommonConstant.Aging_Company_Delete))
				{
					var companyList = _commonRepo.getAllCompany().Where(x => x.Id == deleteCompanyReqDTO.CompanyId).FirstOrDefault();
					if (companyList != null)
					{
						companyList.IsDelete = true;
						companyList.UpdatedDate = DateTime.Now;
						companyList.UpdatedBy = _commonHelper.GetLoggedInUserId();

						_dbContext.CompanyMsts.Update(companyList);
						_dbContext.SaveChanges();

						commonResponse.Status = true;
						commonResponse.StatusCode = HttpStatusCode.OK;
						commonResponse.Data = companyList.Id;
						commonResponse.Message = "Company Deleted Successfully";
					}
					else
					{
						commonResponse.StatusCode = HttpStatusCode.NotFound;
						commonResponse.Message = "Data Not Found";
					}
				}
				else
				{
					commonResponse.StatusCode = HttpStatusCode.Forbidden;
					commonResponse.Message = "You Don't Have Permission To Access this!";
				}

			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		public CommonResponse GetCompanyList()
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				var response = _commonRepo.getAllCompany().Where(x => x.IsActive == true && x.IsDelete == false).ToList();
				if (response.Count > 0)
				{
					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Message = "Data found successfully !";
				}
				else
				{
					commonResponse.StatusCode = HttpStatusCode.NotFound;
					commonResponse.Message = "Data not found !";
				}
				commonResponse.Data = response.Adapt<List<GetCompanyListResDTO>>();
			}
			catch { throw; }
			return commonResponse;
		}
	}
}
