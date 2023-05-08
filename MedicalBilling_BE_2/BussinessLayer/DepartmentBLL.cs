using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Helper.Models;
using Mapster;
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
	public class DepartmentBLL
	{
		private readonly MedicalBillingDbContext _dbContext;
		private readonly CommonRepo _commonRepo;
		private readonly CommonHelper _commonHelper;
		private readonly IConfiguration _configuration;


		public DepartmentBLL(MedicalBillingDbContext dbcontext, CommonRepo commonRepo, CommonHelper commonHelper, IConfiguration configuration)
		{
			_dbContext = dbcontext;
			_commonHelper = commonHelper;
			_commonRepo = commonRepo;
			_configuration = configuration;
		}

		public CommonResponse AddDepartment(AddDepartmentReqDTO addDepartmentReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			AddDepartmentResDTO addDepartmentResDTO = new AddDepartmentResDTO();
			try
			{
				if (_commonRepo.HasPermission(CommonConstant.Aging_Department_Add))
				{

					var duplicateDepartment = _commonRepo.getAllDepartment().Where(x => x.DepartmentDisplayName == addDepartmentReqDTO.DepartmentDisplayName || x.DepartmentName == addDepartmentReqDTO.DepartmentName).ToList();

					var IsEmptydepartment = (string.IsNullOrWhiteSpace(addDepartmentReqDTO.DepartmentName) || (string.IsNullOrWhiteSpace(addDepartmentReqDTO.DepartmentDisplayName)));


					if (IsEmptydepartment == false)
					{
						if (duplicateDepartment.Count == 0)
						{
							DepartmentMst department = new DepartmentMst();
							department.OrganizationId = addDepartmentReqDTO.OrganizationId;
							department.CompanyId = addDepartmentReqDTO.CompanyId;
							department.DepartmentName = addDepartmentReqDTO.DepartmentName;
							department.DepartmentDisplayName = addDepartmentReqDTO.DepartmentDisplayName;
							department.Address = addDepartmentReqDTO.Address;
							department.ZipCode = addDepartmentReqDTO.ZipCode;
							department.Bcn = addDepartmentReqDTO.Bcn;
							department.FaxNo = addDepartmentReqDTO.FaxNo;
							department.Email = addDepartmentReqDTO.Email;
							department.Mobile = addDepartmentReqDTO.Mobile;
							department.Phone = addDepartmentReqDTO?.Phone;
							department.Sonarx = addDepartmentReqDTO?.Sonarx;
							department.Npi = addDepartmentReqDTO?.Npi;
							department.Website = addDepartmentReqDTO.Website;
							department.TaxId = addDepartmentReqDTO.TaxId;
							department.IsActive = addDepartmentReqDTO.IsActive;
							department.IsDelete = false;
							department.CreatedBy = _commonHelper.GetLoggedInUserId();
							department.UpdatedBy = _commonHelper.GetLoggedInUserId();
							department.CreatedDate = DateTime.Now;
							department.UpdatedDate = DateTime.Now;

							_dbContext.DepartmentMsts.Add(department);
							var result = _dbContext.SaveChanges();

							addDepartmentResDTO.DepartmentName = department.DepartmentName;
							addDepartmentResDTO.DepartmentDisplayName = department.DepartmentDisplayName;

							if (result != null)
							{
								commonResponse.Status = true;
								commonResponse.StatusCode = System.Net.HttpStatusCode.OK;
								commonResponse.Message = "Add Department Successfully";
								commonResponse.Data = addDepartmentResDTO;

							}
							else
							{
								commonResponse.Message = "Fail";
							}
						}
						else
						{
							commonResponse.Message = "DepartmentName or DepartmentDisplayName is Already Exist";
							commonResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
						}
					}
					else
					{
						commonResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
						commonResponse.Message = "DepartmentName or DepartmentDisplayName cannot be null";
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

		public CommonResponse GetDetailDepartmentList(GetDetailDepartmentListReqDTO getAllDepartmentReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			GetDetailDepartmentListResDTO getAllDepartmentResDTO = new GetDetailDepartmentListResDTO();
			List<DepartmentList> departmentList = new List<DepartmentList>();

			int Page = Convert.ToInt32(_configuration.GetSection("ByDefaultPagination:Page").Value);
			int PageSize = Convert.ToInt32(_configuration.GetSection("ByDefaultPagination:PageSize").Value);
			bool OrderBy = Convert.ToBoolean(_configuration.GetSection("ByDefaultPagination:OrderBy").Value);

			Page = getAllDepartmentReqDTO.PageNumber == 0 ? (Page) : getAllDepartmentReqDTO.PageNumber;
			PageSize = getAllDepartmentReqDTO.PageSize == 0 ? (PageSize) : getAllDepartmentReqDTO.PageSize;
			OrderBy = getAllDepartmentReqDTO.Orderby == true ? (OrderBy) : getAllDepartmentReqDTO.Orderby;

			try
			{
				if (_commonRepo.HasPermission(CommonConstant.Aging_Department_View))
				{
					departmentList = (from u in _commonRepo.getAllDepartment()
									  select new { u }).ToList().Select((x, Index) => new DepartmentList
									  {
										  DepartmentId = x.u.Id,
										  DepartmentName = x.u.DepartmentName,
										  DepartmentDisplayName = x.u.DepartmentDisplayName,
										  Address = x.u.Address,
										  OrganizationId = x.u.OrganizationId,
										  CompanyId = x.u.CompanyId,
										  FaxNo = x.u.FaxNo,
										  TaxId = x.u.TaxId,
										  Npi = x.u.Npi,
										  Bcn = x.u.Bcn,
										  ZipCode = x.u.ZipCode,
										  Phone = x.u.Phone,
										  Mobile = x.u.Mobile,
										  Email = x.u.Email,
										  Website = x.u.Website,
										  Sonarx = x.u.Sonarx,
										  IsActive = x.u.IsActive
									  }).ToList();

					getAllDepartmentResDTO.TotalCount = departmentList.Count;

					if (OrderBy)
					{
						if (departmentList.Count <= PageSize)
						{
							departmentList = departmentList.OrderBy(x => x.OrganizationId).ToList();
						}
						else
						{
							departmentList = departmentList.Skip((Page - 1) * PageSize)
									.Take(PageSize)
									.OrderBy(x => x.OrganizationId)
									.ToList();
						}
					}
					else
					{
						if (departmentList.Count <= PageSize)
						{
							departmentList = departmentList.OrderByDescending(x => x.OrganizationId).ToList();
						}
						else
						{
							departmentList = departmentList.OrderByDescending(x => x.OrganizationId).Skip((Page - 1) * PageSize)
								.Take(PageSize)
								.ToList();
						}
					}
					if (getAllDepartmentReqDTO.GlobalSearch != null && !string.IsNullOrWhiteSpace(getAllDepartmentReqDTO.GlobalSearch))
					{
						departmentList = departmentList.Where(x => x.DepartmentName.ToLower().Contains(getAllDepartmentReqDTO.GlobalSearch.ToLower()) || x.DepartmentDisplayName.ToLower().Contains(getAllDepartmentReqDTO.GlobalSearch.ToLower())).ToList();
					}

					getAllDepartmentResDTO.DepartmentList = departmentList;

					if (departmentList.Count > 0)
					{
						commonResponse.Status = true;
						commonResponse.StatusCode = HttpStatusCode.OK;
						commonResponse.Message = "GetAll Department Successfully";
						commonResponse.Data = getAllDepartmentResDTO;
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

		public CommonResponse GetDepartmentDetailsById(GetDepartmentDetailsByIdReqDTO getAllDepartmentbyIdReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			GetDepartmentDetailsByIdResDTO getAllDepartmentbyIdResDTO = new GetDepartmentDetailsByIdResDTO();
			var checkDepartment = _commonRepo.getAllDepartment().FirstOrDefault(x => x.Id == getAllDepartmentbyIdReqDTO.DepartmentId);
			try
			{
				if (_commonRepo.HasPermission(CommonConstant.Aging_Department_Update))
				{
					if (checkDepartment != null)
					{
						getAllDepartmentbyIdResDTO.DepartmentId = checkDepartment.Id;
						getAllDepartmentbyIdResDTO.OrganizationId = checkDepartment.OrganizationId;
						getAllDepartmentbyIdResDTO.CompanyId = checkDepartment.CompanyId;
						getAllDepartmentbyIdResDTO.DepartmentName = checkDepartment.DepartmentName;
						getAllDepartmentbyIdResDTO.DepartmentDisplayName = checkDepartment.DepartmentDisplayName;
						getAllDepartmentbyIdResDTO.Address = checkDepartment.Address;
						getAllDepartmentbyIdResDTO.ZipCode = checkDepartment.ZipCode;
						getAllDepartmentbyIdResDTO.Email = checkDepartment.Email;
						getAllDepartmentbyIdResDTO.Phone = checkDepartment.Phone;
						getAllDepartmentbyIdResDTO.Mobile = checkDepartment.Mobile;
						getAllDepartmentbyIdResDTO.Website = checkDepartment.Website;
						getAllDepartmentbyIdResDTO.FaxNo = checkDepartment.FaxNo;
						getAllDepartmentbyIdResDTO.Bcn = checkDepartment.Bcn;
						getAllDepartmentbyIdResDTO.Npi = checkDepartment.Npi;
						getAllDepartmentbyIdResDTO.Sonarx = checkDepartment.Sonarx;
						getAllDepartmentbyIdResDTO.TaxId = checkDepartment.TaxId;
						getAllDepartmentbyIdResDTO.IsActive = checkDepartment.IsActive;

						commonResponse.Status = true;
						commonResponse.StatusCode = HttpStatusCode.OK;
						commonResponse.Message = "GetAll Deparmtnet Data Successfully";
						commonResponse.Data = getAllDepartmentbyIdResDTO;
					}
					else
					{
						commonResponse.StatusCode = HttpStatusCode.NotFound;
						commonResponse.Message = "No Data Found";
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

		public CommonResponse UpdateDepartment(UpdateDepartmentReqDTO updateDepartmentReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			var department = _commonRepo.getAllDepartment().FirstOrDefault(x => x.Id == updateDepartmentReqDTO.DepartmentId);
			var checkdepartment = _commonRepo.getAllDepartment().Where(x => x.Id != updateDepartmentReqDTO.DepartmentId);
			var duplicatecheck = checkdepartment.FirstOrDefault(x => x.DepartmentName == updateDepartmentReqDTO.DepartmentName || x.DepartmentDisplayName == updateDepartmentReqDTO.DepartmentDisplayName);
			var IsNullCheck = (string.IsNullOrWhiteSpace(updateDepartmentReqDTO.DepartmentName) || (string.IsNullOrWhiteSpace(updateDepartmentReqDTO.DepartmentDisplayName)));
			try
			{
				if (_commonRepo.HasPermission(CommonConstant.Aging_Department_Update))
				{
					if (IsNullCheck == false)
					{
						if (duplicatecheck == null)
						{
							department.DepartmentName = updateDepartmentReqDTO.DepartmentName;
							department.DepartmentDisplayName = updateDepartmentReqDTO.DepartmentDisplayName;
							department.Address = updateDepartmentReqDTO.Address;
							department.Email = updateDepartmentReqDTO.Email;
							department.Phone = updateDepartmentReqDTO.Phone;
							department.Mobile = updateDepartmentReqDTO.Mobile;
							department.FaxNo = updateDepartmentReqDTO.FaxNo;
							department.Npi = updateDepartmentReqDTO.Npi;
							department.Bcn = updateDepartmentReqDTO.Bcn;
							department.ZipCode = updateDepartmentReqDTO.ZipCode;
							department.Website = updateDepartmentReqDTO.Website;
							department.Sonarx = updateDepartmentReqDTO.Sonarx;
							department.IsActive = updateDepartmentReqDTO.IsActive;
							department.UpdatedDate = DateTime.Now;
							department.UpdatedBy = _commonHelper.GetLoggedInUserId();

							_dbContext.DepartmentMsts.Update(department);
							_dbContext.SaveChanges();

							commonResponse.Status = true;
							commonResponse.StatusCode = HttpStatusCode.OK;
							commonResponse.Message = "department Updated Successfully";
							commonResponse.Data = department.Id;
						}
						else
						{
							commonResponse.StatusCode = HttpStatusCode.BadRequest;
							commonResponse.Message = "DepartmentName or DepartmentDisplayname is Already Exist";
						}
					}
					else
					{
						commonResponse.StatusCode = HttpStatusCode.BadRequest;
						commonResponse.Message = "DepartmentName or DepartmentDisplayName cannot be null";
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

		public CommonResponse DeleteDepartment(DeleteDepartmentReqDTO deleteDepartmentReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			var checkDepartment = _commonRepo.getAllDepartment().FirstOrDefault(x => x.Id == deleteDepartmentReqDTO.DepartmentId);
			try
			{
				if (_commonRepo.HasPermission(CommonConstant.Aging_Department_View))
				{
					if (checkDepartment != null)
					{
						checkDepartment.IsDelete = true;
						checkDepartment.UpdatedDate = DateTime.Now;
						checkDepartment.UpdatedBy = _commonHelper.GetLoggedInUserId();

						_dbContext.DepartmentMsts.Update(checkDepartment);
						_dbContext.SaveChanges();

						commonResponse.Status = true;
						commonResponse.StatusCode = HttpStatusCode.OK;
						commonResponse.Message = "Department Deleted Successfully";
						commonResponse.Data = checkDepartment.Id;
					}
					else
					{
						commonResponse.Message = "Data Not Found";
						commonResponse.StatusCode = HttpStatusCode.NotFound;
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

		public CommonResponse GetDepartmentList()
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				var response = _commonRepo.getAllDepartment().Where(x => x.IsActive == true && x.IsDelete == false).ToList();
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
				commonResponse.Data = response.Adapt<List<GetDepartmentListResDTO>>();
			}
			catch { throw; }
			return commonResponse;
		}
	}
}
