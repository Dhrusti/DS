using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Helper.Models;
using Mapster;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static DTO.ResDTO.GetDetailOrganizationListResDTO;

namespace BussinessLayer
{
	public class OrganizationBLL
	{
		private readonly MedicalBillingDbContext _dbContext;
		private readonly CommonRepo _commonRepo;
		private readonly CommonHelper _commonHelper;
		private readonly IConfiguration _configuration;
		public OrganizationBLL(MedicalBillingDbContext dbcontext, CommonRepo commonRepo, CommonHelper commonHelper, IConfiguration configuration)
		{
			_dbContext = dbcontext;
			_commonHelper = commonHelper;
			_commonRepo = commonRepo;
			_configuration = configuration;
		}

		public CommonResponse AddOrganization(AddOrganizationReqDTO addOrganizationReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				bool IsDuplicate = _commonRepo.getOrganizationList().FirstOrDefault(x => x.OrganizationDisplayName == addOrganizationReqDTO.OrganizationDisplayName || x.OrganizationName == addOrganizationReqDTO.OrganizationName) != null ? true : false;
				bool IsRequired = (string.IsNullOrWhiteSpace(addOrganizationReqDTO.OrganizationName) || string.IsNullOrWhiteSpace(addOrganizationReqDTO.OrganizationDisplayName));
				if (_commonRepo.HasPermission(CommonConstant.Aging_Organization_Add))
				{
					if (!IsRequired)
					{
						if (!IsDuplicate)
						{
							DateTime CurrentDateTime = _commonHelper.GetCurrentDateTime();
							int LoggedInUserId = _commonHelper.GetLoggedInUserId();
							OrganizationMst organizationMst = new OrganizationMst();
							organizationMst.OrganizationDisplayName = addOrganizationReqDTO.OrganizationDisplayName;
							organizationMst.OrganizationName = addOrganizationReqDTO.OrganizationName;
							organizationMst.Address = addOrganizationReqDTO.Address;
							organizationMst.Email = addOrganizationReqDTO.Email;
							organizationMst.Sonarx = addOrganizationReqDTO.Sonarx;
							organizationMst.Npi = addOrganizationReqDTO.Npi;
							organizationMst.Phone = addOrganizationReqDTO.Phone;
							organizationMst.Bcn = addOrganizationReqDTO.Bcn;
							organizationMst.ZipCode = addOrganizationReqDTO.ZipCode;
							organizationMst.FaxNo = addOrganizationReqDTO.FaxNo;
							organizationMst.Mobile = addOrganizationReqDTO.Mobile;
							organizationMst.Website = addOrganizationReqDTO.Website;
							organizationMst.TaxId = addOrganizationReqDTO.TaxId;
							organizationMst.IsActive = addOrganizationReqDTO.IsActive;
							organizationMst.IsDelete = false;
							organizationMst.CreatedDate = CurrentDateTime;
							organizationMst.UpdatedDate = CurrentDateTime;
							organizationMst.CreatedBy = LoggedInUserId;
							organizationMst.UpdatedBy = LoggedInUserId;

							_dbContext.OrganizationMsts.Add(organizationMst);
							_dbContext.SaveChanges();

							commonResponse.Status = true;
							commonResponse.StatusCode = HttpStatusCode.OK;
							commonResponse.Message = "Organization Added Successfully!";
							commonResponse.Data = organizationMst.Id;
						}
						else
						{
							commonResponse.StatusCode = HttpStatusCode.BadRequest;
							commonResponse.Message = "Organization Name or Organization DisplayName is Already Exist!";
						}
					}
					else
					{
						commonResponse.StatusCode = HttpStatusCode.BadRequest;
						commonResponse.Message = "Organization Name or Organization DisplayName cannot be null!";
					}
				}
				else
				{
					commonResponse.StatusCode = HttpStatusCode.Forbidden;
					commonResponse.Message = "You Don't Have Permission To Access this!";
				}
			}
			catch { throw; }
			return commonResponse;
		}
		public CommonResponse GetDetailOrganizationList(GetDetailOrganizationListReqDTO getDetailOrganizationListReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			GetDetailOrganizationListResDTO getAllOrganizationResDTO = new GetDetailOrganizationListResDTO();

			List<OrganizationList> organizationList = new List<OrganizationList>();

			int Page = Convert.ToInt32(_configuration.GetSection("ByDefaultPagination:Page").Value);
			int PageSize = Convert.ToInt32(_configuration.GetSection("ByDefaultPagination:PageSize").Value);
			bool OrderBy = Convert.ToBoolean(_configuration.GetSection("ByDefaultPagination:OrderBy").Value);

			Page = getDetailOrganizationListReqDTO.PageNumber == 0 ? (Page) : getDetailOrganizationListReqDTO.PageNumber;
			PageSize = getDetailOrganizationListReqDTO.PageSize == 0 ? (PageSize) : getDetailOrganizationListReqDTO.PageSize;
			OrderBy = getDetailOrganizationListReqDTO.Orderby == true ? (OrderBy) : getDetailOrganizationListReqDTO.Orderby;

			try
			{
				if (_commonRepo.HasPermission(CommonConstant.Aging_Organization_View))
				{
					organizationList = (from u in _commonRepo.getOrganizationList()
										select new { u }).ToList().Select((x, Index) => new OrganizationList
										{
											SrNo = Index + 1,
											OrganizationId = x.u.Id,
											OrganizationName = x.u.OrganizationName,
											OrganizationDisplayName = x.u.OrganizationDisplayName,
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

					getAllOrganizationResDTO.TotalCount = organizationList.Count;

					if (OrderBy)
					{
						if (organizationList.Count <= PageSize)
						{
							organizationList = organizationList.OrderBy(x => x.OrganizationId).ToList();
						}
						else
						{
							organizationList = organizationList.Skip((Page - 1) * PageSize)
									.Take(PageSize)
									.OrderBy(x => x.OrganizationId)
									.ToList();
						}
					}
					else
					{
						if (organizationList.Count <= PageSize)
						{
							organizationList = organizationList.OrderByDescending(x => x.OrganizationId).ToList();
						}
						else
						{
							organizationList = organizationList.OrderByDescending(x => x.OrganizationId).Skip((Page - 1) * PageSize)
								.Take(PageSize)
								.ToList();
						}
					}

					if (getDetailOrganizationListReqDTO.GlobalSearch != null && !string.IsNullOrWhiteSpace(getDetailOrganizationListReqDTO.GlobalSearch))
					{
						organizationList = organizationList.Where(x => x.OrganizationName.ToLower().Contains(getDetailOrganizationListReqDTO.GlobalSearch.ToLower()) || x.OrganizationDisplayName.ToLower().Contains(getDetailOrganizationListReqDTO.GlobalSearch.ToLower())).ToList();
					}


					getAllOrganizationResDTO.organizationList = organizationList;

					if (organizationList.Count > 0)
					{
						commonResponse.Status = true;
						commonResponse.StatusCode = HttpStatusCode.OK;
						commonResponse.Message = "All Organization List";
						commonResponse.Data = getAllOrganizationResDTO;
					}
					else
					{
						commonResponse.StatusCode = HttpStatusCode.NotFound;
						commonResponse.Message = "No Data Found";
						commonResponse.Data = getAllOrganizationResDTO;
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
		public CommonResponse GetOrganizationDetailById(GetOrganizationDetailByIdReqDTO getOrganizationDetailByIdReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			GetOrganizationDetailByIdResDTO getOrganizationDetailByIdResDTO = new GetOrganizationDetailByIdResDTO();
			try
			{
				if (_commonRepo.HasPermission(CommonConstant.Aging_Organization_Update))
				{
					OrganizationMst organizationMst = new OrganizationMst();
					var organizationList = _commonRepo.getOrganizationList().Where(x => x.Id == getOrganizationDetailByIdReqDTO.OrganizationId).FirstOrDefault();
					if (organizationList != null)
					{
						getOrganizationDetailByIdResDTO.OrganizationId = organizationList.Id;
						getOrganizationDetailByIdResDTO.OrganizationName = organizationList.OrganizationName;
						getOrganizationDetailByIdResDTO.OrganizationDisplayName = organizationList.OrganizationDisplayName;
						getOrganizationDetailByIdResDTO.Phone = organizationList.Phone;
						getOrganizationDetailByIdResDTO.Mobile = organizationList.Mobile;
						getOrganizationDetailByIdResDTO.Address = organizationList.Address;
						getOrganizationDetailByIdResDTO.Website = organizationList.Website;
						getOrganizationDetailByIdResDTO.Email = organizationList.Email;
						getOrganizationDetailByIdResDTO.Bcn = organizationList.Bcn;
						getOrganizationDetailByIdResDTO.ZipCode = organizationList.ZipCode;
						getOrganizationDetailByIdResDTO.FaxNo = organizationList.FaxNo;
						getOrganizationDetailByIdResDTO.Sonarx = organizationList.Sonarx;
						getOrganizationDetailByIdResDTO.Npi = organizationList.Npi;
						getOrganizationDetailByIdResDTO.TaxId = organizationList.TaxId;
						getOrganizationDetailByIdResDTO.IsActive = organizationList.IsActive;

						commonResponse.Status = true;
						commonResponse.StatusCode = HttpStatusCode.OK;
						commonResponse.Data = getOrganizationDetailByIdResDTO;
						commonResponse.Message = "GetAll Organization Successfully";
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
			catch (Exception) { throw; }
			return commonResponse;

		}
		public CommonResponse UpdateOrganization(UpdateOrganizationReqDTO updateOrganizationReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				if (_commonRepo.HasPermission(CommonConstant.Aging_Organization_Update))
				{
					var organizationList = _commonRepo.getOrganizationList().Where(x => x.Id == updateOrganizationReqDTO.OrganizationId).FirstOrDefault();
					var checkOrganization = _commonRepo.getOrganizationList().Where(x => x.Id != updateOrganizationReqDTO.OrganizationId);

					var duplicateorganization = checkOrganization.FirstOrDefault(x => x.OrganizationDisplayName == updateOrganizationReqDTO.OrganizationDisplayName || x.OrganizationName == updateOrganizationReqDTO.OrganizationName);

					var isEmptyorganization = (string.IsNullOrWhiteSpace(updateOrganizationReqDTO.OrganizationName) || string.IsNullOrWhiteSpace(updateOrganizationReqDTO.OrganizationDisplayName));

					if (organizationList != null)
					{
						if (isEmptyorganization == false)
						{
							if (duplicateorganization == null)
							{
								organizationList.OrganizationName = updateOrganizationReqDTO.OrganizationName;
								organizationList.OrganizationDisplayName = updateOrganizationReqDTO.OrganizationDisplayName;
								organizationList.Address = updateOrganizationReqDTO.Address;
								organizationList.Email = updateOrganizationReqDTO.Email;
								organizationList.Mobile = updateOrganizationReqDTO.Mobile;
								organizationList.Phone = updateOrganizationReqDTO.Phone;
								organizationList.Bcn = updateOrganizationReqDTO.Bcn;
								organizationList.ZipCode = updateOrganizationReqDTO.ZipCode;
								organizationList.FaxNo = updateOrganizationReqDTO.FaxNo;
								organizationList.Npi = updateOrganizationReqDTO.Npi;
								organizationList.TaxId = updateOrganizationReqDTO.TaxId;
								organizationList.Website = updateOrganizationReqDTO.Website;
								organizationList.IsActive = updateOrganizationReqDTO.IsActive;
								organizationList.UpdatedDate = _commonHelper.GetCurrentDateTime();
								organizationList.UpdatedBy = _commonHelper.GetLoggedInUserId();

								_dbContext.OrganizationMsts.Update(organizationList);
								_dbContext.SaveChanges();

								commonResponse.Status = true;
								commonResponse.StatusCode = HttpStatusCode.OK;
								commonResponse.Data = organizationList.Id;
								commonResponse.Message = "Organization Updated Successfully!";
							}
							else
							{
								commonResponse.StatusCode = HttpStatusCode.BadRequest;
								commonResponse.Message = "Organizationname or OrganizationDisplayname is Already Exist";
							}
						}
						else
						{
							commonResponse.StatusCode = HttpStatusCode.BadRequest;
							commonResponse.Message = "OrganizationName or OrganizationCompany cannot be null";
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
			catch { throw; }
			return commonResponse;
		}
		public CommonResponse DeleteOrganization(DeleteOrganizationReqDTO deleteOrganizationReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				if (_commonRepo.HasPermission(CommonConstant.Aging_Organization_Delete))
				{
					var organizationList = _commonRepo.getOrganizationList().Where(x => x.Id == deleteOrganizationReqDTO.OrganizationId).FirstOrDefault();
					var IsorganizationAssigned = _commonRepo.getAllCompany().Where(x => x.OrganizationId == deleteOrganizationReqDTO.OrganizationId).FirstOrDefault();

					if (IsorganizationAssigned == null)
					{
						if (organizationList != null)
						{
							//organizationList.IsActive = false;
							organizationList.IsDelete = true;
							organizationList.UpdatedDate = DateTime.Now;
							organizationList.UpdatedBy = _commonHelper.GetLoggedInUserId();

							_dbContext.OrganizationMsts.Update(organizationList);
							_dbContext.SaveChanges();

							commonResponse.Status = true;
							commonResponse.StatusCode = HttpStatusCode.OK;
							commonResponse.Data = organizationList.Id;
							commonResponse.Message = "Organization Deleted Successfully!";
						}
						else
						{
							commonResponse.StatusCode = HttpStatusCode.NotFound;
							commonResponse.Message = "Data Not Found";
						}
					}
					else
					{
						commonResponse.StatusCode = HttpStatusCode.NotFound;
						commonResponse.Message = "Organization Is Already Assigned to Particular Company";
					}
				}
				else
				{
					commonResponse.StatusCode = HttpStatusCode.Forbidden;
					commonResponse.Message = "You Don't Have Permission To Access this!";
				}

			}
			catch { throw; }
			return commonResponse;
		}
		public CommonResponse GetOrganizationList()
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				var response = _commonRepo.getOrganizationList().Where(x => x.IsActive == true && x.IsDelete == false).ToList();
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
				commonResponse.Data = response.Adapt<List<GetOrganizationListResDTO>>();
			}
			catch { throw; } 
			return commonResponse;
		}

	}
}
