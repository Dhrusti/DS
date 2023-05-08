using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Helper.Models;
using Mapster;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Digests;

namespace BussinessLayer
{
	public class FileCategoryHistoryBLL
	{
		private readonly CommonRepo _commonRepo;
		private readonly IHostingEnvironment _hostEnvironment;
		private readonly CommonHelper _commonHelper;
		private readonly MedicalBillingDbContext _dbContext;
		public FileCategoryHistoryBLL(CommonRepo commonRepo, IHostingEnvironment hostEnvironment, CommonHelper commonHelper, MedicalBillingDbContext dbContext)
		{
			_commonRepo = commonRepo;
			_hostEnvironment = hostEnvironment;
			_commonHelper = commonHelper;
			_dbContext = dbContext;
		}

		public CommonResponse UploadFileCategoryHistory(FileCategoryHistoryReqDTO fileCategoryHistoryReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			FileCategoryHistoryResDTO fileCategoryHistoryRes = new FileCategoryHistoryResDTO();
			try
			{
				IFormFile file = fileCategoryHistoryReqDTO.File;
				string FileName = file.FileName;
				FileInfo fileInfo = new FileInfo(FileName);
				string FileExtension = fileInfo.Extension;
				long fileSize = file.Length;
				bool validateFileExtension = false;
				bool validateFileSize = false;
				string[] allowedFileExtensions = { CommonConstant.xlsx };
				long allowedFileSize = 1 * 1024 * 1024 * 10; // 10MB
				validateFileExtension = allowedFileExtensions.Contains(FileExtension) ? true : false;
				validateFileSize = fileSize <= allowedFileSize ? true : false;

				if (validateFileExtension && validateFileSize)
				{
					bool IsFileCategoryDuplicate = _commonRepo.getFileCategoryHistoryList().FirstOrDefault(x => x.FileCategoryName == fileCategoryHistoryReqDTO.FileCategoryName && x.OrganizationId == fileCategoryHistoryReqDTO.OrganizationId && x.CompanyId == fileCategoryHistoryReqDTO.CompanyId && x.DepartmentId == fileCategoryHistoryReqDTO.DepartmentId) != null ? true : false;
					if (!IsFileCategoryDuplicate)
					{
						var FileCategoryHistoryRes = _commonHelper.UploadFile(fileCategoryHistoryReqDTO.File, @"Aging\FileCategoryHistory", FileName, false, true, true);
						FileCategoryHistoryMst fileCategoryHistory = new FileCategoryHistoryMst();
						fileCategoryHistory.FileCategoryName = fileCategoryHistoryReqDTO.FileCategoryName;
						fileCategoryHistory.CompanyId = fileCategoryHistoryReqDTO.CompanyId;
						fileCategoryHistory.DepartmentId = fileCategoryHistoryReqDTO.DepartmentId;
						fileCategoryHistory.OrganizationId = fileCategoryHistoryReqDTO.OrganizationId;
						fileCategoryHistory.FileFormatPath = FileCategoryHistoryRes.Data;
						fileCategoryHistory.IsActive = true;
						fileCategoryHistory.IsDelete = false;
						fileCategoryHistory.CreatedDate = _commonHelper.GetCurrentDateTime();
						fileCategoryHistory.UpdatedDate = _commonHelper.GetCurrentDateTime();
						fileCategoryHistory.CreatedBy = _commonHelper.GetLoggedInUserId();
						fileCategoryHistory.UpdatedBy = _commonHelper.GetLoggedInUserId();

						_dbContext.FileCategoryHistoryMsts.Add(fileCategoryHistory);
						_dbContext.SaveChanges();

						commonResponse.Status = true;
						commonResponse.StatusCode = HttpStatusCode.OK;
						commonResponse.Message = "File Category Created Successfully!";
						commonResponse.Data = fileCategoryHistory.Id;
					}
					else
					{
						commonResponse.Message = "File Category Name Already Exists for Selected Organization, Company and Department !";
					}
				}
				else
				{
					commonResponse.Message = "Only Excel(.xlxs) file With Max Size : 10 MB is Allowed !";
				}
			}
			catch { throw; }
			return commonResponse;
		}

		public CommonResponse GetAllFileCategoryHistory()
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				var response = _commonRepo.getFileCategoryHistoryList().Where(x => x.IsActive == true && x.IsDelete == false).ToList();
				if (response != null)
				{
					commonResponse.Data = response.Adapt<List<GetAllFileCategoryHistoryResDTO>>();
					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Message = "Data found successfully.!";
				}
				else
				{
					commonResponse.StatusCode = HttpStatusCode.NotFound;
					commonResponse.Message = "Data not found.!";
				}
			}
			catch { throw; }
			return commonResponse;
		}

		public CommonResponse GetFileCategoryHistoryById(GetFileCategoryHistoryByIdReqDTO getAllFileCategoryHistoryReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				var res = _commonRepo.getFileCategoryHistoryList().FirstOrDefault(x => x.Id == getAllFileCategoryHistoryReqDTO.Id);
				if (res != null)
				{
					commonResponse.Data = res.Adapt<GetFileCategoryHistoryByIdResDTO>();
					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Message = "Data found successfully.!";
				}
				else
				{
					commonResponse.StatusCode = HttpStatusCode.NotFound;
					commonResponse.Message = "Data not found.!";
				}
			}
			catch { throw; }
			return commonResponse;
		}

		public CommonResponse UpdateFileCategoryHistory(UpdateFileCategoryHistoryReqDTO updateFileCategoryHistoryReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			UpdateFileCategoryHistoryResDTO updateFileCategoryHistoryResDTO = new UpdateFileCategoryHistoryResDTO();
			try
			{
				var response = _commonRepo.getFileCategoryHistoryList().FirstOrDefault(x => x.Id == updateFileCategoryHistoryReqDTO.Id);
				if (response != null)
				{
					IFormFile file = updateFileCategoryHistoryReqDTO.File;
					string FileName = file.FileName;
					FileInfo fileInfo = new FileInfo(FileName);
					string FileExtension = fileInfo.Extension;
					long fileSize = file.Length;
					bool validateFileExtension = false;
					bool validateFileSize = false;
					string[] allowedFileExtensions = { CommonConstant.xlsx };
					long allowedFileSize = 1 * 1024 * 1024 * 10; // 10MB
					validateFileExtension = allowedFileExtensions.Contains(FileExtension) ? true : false;
					validateFileSize = fileSize <= allowedFileSize ? true : false;

					if (validateFileExtension && validateFileSize)
					{
						bool IsFileCategoryDuplicate = _commonRepo.getFileCategoryHistoryList().FirstOrDefault(x => x.FileCategoryName == updateFileCategoryHistoryReqDTO.FileCategoryName && x.OrganizationId == updateFileCategoryHistoryReqDTO.OrganizationId && x.CompanyId == updateFileCategoryHistoryReqDTO.CompanyId && x.DepartmentId == updateFileCategoryHistoryReqDTO.DepartmentId) != null ? true : false;
						if (!IsFileCategoryDuplicate)
						{
							var FileCategoryHistoryRes = _commonHelper.UploadFile(updateFileCategoryHistoryReqDTO.File, @"Aging\FileCategoryHistory", FileName, false, true, true);

							FileCategoryHistoryMst fileCategoryHistoryMst = new FileCategoryHistoryMst();
							response.FileCategoryName = updateFileCategoryHistoryReqDTO.FileCategoryName;
							response.OrganizationId = updateFileCategoryHistoryReqDTO.OrganizationId;
							response.CompanyId = updateFileCategoryHistoryReqDTO.CompanyId;
							response.DepartmentId = updateFileCategoryHistoryReqDTO.DepartmentId;
							response.FileFormatPath = FileCategoryHistoryRes.Data;
							response.IsActive = true;
							response.IsDelete = false;
							response.UpdatedDate = DateTime.Now;
							response.CreatedDate = DateTime.Now;
							response.CreatedBy = _commonHelper.GetLoggedInUserId();
							response.UpdatedBy = _commonHelper.GetLoggedInUserId();

							_dbContext.Entry(response).State = EntityState.Modified;
							_dbContext.SaveChanges();

							commonResponse.Data = response.Adapt<UpdateFileCategoryHistoryResDTO>();
							commonResponse.Status = true;
							commonResponse.StatusCode = HttpStatusCode.OK;
							commonResponse.Message = "Data updated successfully !";
						}
						else
						{
							commonResponse.StatusCode = HttpStatusCode.BadRequest;
							commonResponse.Message = "File Category Name Already Exists for Selected Organization, Company and Department !";
						}
					}
					else
					{
						commonResponse.Message = "Only Excel(.xlxs) file With Max Size : 10 MB is Allowed !";
					}
				}
				else
				{
					commonResponse.Message = "Data not found !";
				}
			}
			catch { throw; }
			return commonResponse;
		}

		public CommonResponse DeleteFileCategoryHistory(DeleteFileCategoryHistoryReqDTO deleteFileCategoryHistoryReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			DeleteFileCategoryHistoryResDTO deleteFileCategoryHistoryResDTO = new DeleteFileCategoryHistoryResDTO();
			try
			{
				var response = _commonRepo.getFileCategoryHistoryList().FirstOrDefault(x => x.Id == deleteFileCategoryHistoryReqDTO.Id);
				if (response != null)
				{
					response.IsDelete = true;
					response.UpdatedBy = _commonHelper.GetLoggedInUserId();
					response.UpdatedDate = DateTime.Now;

					_dbContext.Entry(response).State = EntityState.Modified;
					_dbContext.SaveChanges();

					commonResponse.Data = response.Adapt<DeleteFileCategoryHistoryResDTO>();
					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Message = "Data deleted successfully.!";
				}
				else
				{
					commonResponse.StatusCode = HttpStatusCode.BadRequest;
					commonResponse.Message = "Data not found.!";
				}
			}
			catch { throw; }
			return commonResponse;
		}
	}
}
