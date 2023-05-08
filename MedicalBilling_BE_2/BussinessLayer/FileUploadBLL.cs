using Azure;
using DataLayer.Entities;
using DTO.ReqDTO;
using Helper;
using Helper.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace BussinessLayer
{
	public class FileUploadBLL
	{
		private readonly CommonHelper _commonHelper;
		private readonly CommonRepo _commonRepo;
		private readonly MedicalBillingDbContext _dbContext;
		private readonly PayerBLL _payerBLL;
		private readonly AgingPatientBLL _agingPatientBLL;
		private readonly AgingPolicyBLL _agingPolicyBLL;
		private readonly ClaimBLL _claimBLL;
		private readonly AgingServiceBLL _agingServiceBLL;
		public FileUploadBLL(CommonRepo commonRepo, CommonHelper commonHelper, MedicalBillingDbContext dbContext, PayerBLL payerBLL, AgingPatientBLL agingPatientBLL, AgingPolicyBLL agingPolicyBLL, ClaimBLL claimBLL, AgingServiceBLL agingServiceBLL)
		{
			_commonHelper = commonHelper;
			_commonRepo = commonRepo;
			_dbContext = dbContext;
			_payerBLL = payerBLL;
			_agingPatientBLL = agingPatientBLL;
			_agingPolicyBLL = agingPolicyBLL;
			_claimBLL = claimBLL;
			_agingServiceBLL = agingServiceBLL;
		}

		public CommonResponse UploadFileData(UploadFileDataReqDTO uploadFileDataReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				List<UploadFileDataErrorStatus> errorStatusList = new List<UploadFileDataErrorStatus>();
				List<UploadFileExcelDataModel> excelDataModelList = new List<UploadFileExcelDataModel>();
				UploadFileDataErrorStatus errorStatus = new UploadFileDataErrorStatus();

				IFormFile file = uploadFileDataReqDTO.File;
				string fileName = file.FileName;
				FileInfo fileInfo = new FileInfo(file.FileName);
				string fileExtension = fileInfo.Extension.ToLower();
				long fileSize = file.Length;
				//fileName = fileName + fileExtension;

				var fileRes = _commonHelper.UploadFile(file, @"Aging\FileHistory", fileName, false, true, true);
				string filePath = Path.Combine(_commonHelper.GetPhysicalRootPath(false), fileRes.Data);
				bool validateFileExtension = false;
				bool validateFileSize = false;
				int errorCount = 0;

				string[] allowedFileExtensions = { CommonConstant.xlsx };
				long allowedFileSize = 1 * 1024 * 1024 * 500; //500MB
				validateFileExtension = allowedFileExtensions.Contains(fileExtension) ? true : false;
				validateFileSize = fileSize <= allowedFileSize ? true : false;

				if (!validateFileExtension || !validateFileSize)
				{
					commonResponse.Message = "File Extension or FileSize Invalid!";
					errorCount++;
				}

				// If you use EPPlus in a noncommercial context
				// according to the Polyform Noncommercial license:
				ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
				using (var package = new ExcelPackage(filePath))
				{
					var currentSheet = package.Workbook.Worksheets;
					var workSheet = currentSheet["First Sheet"];
					var noOfCol = workSheet.Dimension.End.Column;
					var noOfRow = workSheet.Dimension.End.Row;
					if (noOfCol == 30)
					{
						UploadFileExcelDataModel excelData = new UploadFileExcelDataModel();
						Type ApplicantInfo = excelData.GetType();
						PropertyInfo[] properties = ApplicantInfo.GetProperties();

						//Validate Column Names with Excel Headers
						int columnNo = 1;
						bool validateColumnNames = true;
						for (int i = 1; i < noOfCol; i++)
						{
							if (properties[i].Name.ToString() != workSheet.Cells[1, columnNo].Value.ToString())
							{
								validateColumnNames = false;
								errorCount++;
								break;
							}
							columnNo++;
						}
						bool validateRequiredFields = true;

						//Getting Data From Excel into model List
						if (validateColumnNames)
						{
							if (noOfRow >= 2)
							{
								for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
								{
									UploadFileExcelDataModel excelDataModel = new UploadFileExcelDataModel();

									excelDataModel.PayerName = workSheet.Cells[rowIterator, 1].Value == null || string.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 1].Value.ToString()) ? "" : workSheet.Cells[rowIterator, 1].Value.ToString();
									excelDataModel.PayerCode = workSheet.Cells[rowIterator, 2].Value == null || String.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 2].Value.ToString()) ? "" : workSheet.Cells[rowIterator, 2].Value.ToString();
									excelDataModel.RenderingFullName = workSheet.Cells[rowIterator, 3].Value == null || String.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 3].Value.ToString()) ? "" : workSheet.Cells[rowIterator, 3].Value.ToString();
									excelDataModel.RefferringFullName = workSheet.Cells[rowIterator, 4].Value == null || String.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 4].Value.ToString()) ? "" : workSheet.Cells[rowIterator, 4].Value.ToString();
									excelDataModel.PatientName = workSheet.Cells[rowIterator, 5].Value == null || String.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 5].Value.ToString()) ? "" : workSheet.Cells[rowIterator, 5].Value.ToString();
									excelDataModel.PatientCode = workSheet.Cells[rowIterator, 6].Value == null || String.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 6].Value.ToString()) ? "" : workSheet.Cells[rowIterator, 6].Value.ToString();
									excelDataModel.PatientBirthDate = workSheet.Cells[rowIterator, 7].Value == null || String.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 7].Value.ToString()) ? null : Convert.ToDateTime(workSheet.Cells[rowIterator, 7].Value.ToString());
									excelDataModel.MedicalRecordCode = workSheet.Cells[rowIterator, 8].Value == null || String.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 8].Value.ToString()) ? "" : workSheet.Cells[rowIterator, 8].Value.ToString();
									excelDataModel.EAIBCode = workSheet.Cells[rowIterator, 9].Value == null || String.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 9].Value.ToString()) ? "" : workSheet.Cells[rowIterator, 9].Value.ToString();
									excelDataModel.Componant = workSheet.Cells[rowIterator, 10].Value == null || String.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 10].Value.ToString()) ? "" : workSheet.Cells[rowIterator, 10].Value.ToString();
									excelDataModel.PayerPhone = workSheet.Cells[rowIterator, 11].Value == null || String.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 11].Value.ToString()) ? "" : workSheet.Cells[rowIterator, 11].Value.ToString();
									excelDataModel.PolicyCode = workSheet.Cells[rowIterator, 12].Value == null || String.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 12].Value.ToString()) ? "" : workSheet.Cells[rowIterator, 12].Value.ToString();
									excelDataModel.ClaimStatus = workSheet.Cells[rowIterator, 13].Value == null || String.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 13].Value.ToString()) ? "" : workSheet.Cells[rowIterator, 13].Value.ToString();
									excelDataModel.ClaimCode = workSheet.Cells[rowIterator, 14].Value == null || String.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 14].Value.ToString()) ? "" : workSheet.Cells[rowIterator, 14].Value.ToString();
									excelDataModel.DateOfService = workSheet.Cells[rowIterator, 15].Value == null || String.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 15].Value.ToString()) ? null : Convert.ToDateTime(workSheet.Cells[rowIterator, 15].Value.ToString());
									excelDataModel.ServiceCPT = workSheet.Cells[rowIterator, 16].Value == null || String.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 16].Value.ToString()) ? "" : workSheet.Cells[rowIterator, 16].Value.ToString();
									excelDataModel.ServiceCode = workSheet.Cells[rowIterator, 17].Value == null || String.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 17].Value.ToString()) ? "" : workSheet.Cells[rowIterator, 17].Value.ToString();
									excelDataModel.ClaimModifier = workSheet.Cells[rowIterator, 18].Value == null || String.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 18].Value.ToString()) ? "" : workSheet.Cells[rowIterator, 18].Value.ToString();
									excelDataModel.DiagnosisCode1 = workSheet.Cells[rowIterator, 19].Value == null || String.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 19].Value.ToString()) ? "" : workSheet.Cells[rowIterator, 18].Value.ToString();
									excelDataModel.DiagnosisCode2 = workSheet.Cells[rowIterator, 20].Value == null || String.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 20].Value.ToString()) ? "" : workSheet.Cells[rowIterator, 18].Value.ToString();
									excelDataModel.DiagnosisCode3 = workSheet.Cells[rowIterator, 21].Value == null || String.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 21].Value.ToString()) ? "" : workSheet.Cells[rowIterator, 18].Value.ToString();
									excelDataModel.DiagnosisCode4 = workSheet.Cells[rowIterator, 22].Value == null || String.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 22].Value.ToString()) ? "" : workSheet.Cells[rowIterator, 18].Value.ToString();
									excelDataModel.COB = workSheet.Cells[rowIterator, 23].Value == null || String.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 23].Value.ToString()) ? "" : workSheet.Cells[rowIterator, 18].Value.ToString();
									excelDataModel.InsuranceAmount1 = workSheet.Cells[rowIterator, 24].Value == null || String.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 24].Value.ToString()) ? null : Convert.ToDecimal(workSheet.Cells[rowIterator, 18].Value.ToString());
									excelDataModel.InsuranceAmount2 = workSheet.Cells[rowIterator, 25].Value == null || String.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 25].Value.ToString()) ? null : Convert.ToDecimal(workSheet.Cells[rowIterator, 18].Value.ToString());
									excelDataModel.InsuranceAmount3 = workSheet.Cells[rowIterator, 26].Value == null || String.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 26].Value.ToString()) ? null : Convert.ToDecimal(workSheet.Cells[rowIterator, 18].Value.ToString());
									excelDataModel.InsuranceAmount4 = workSheet.Cells[rowIterator, 27].Value == null || String.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 27].Value.ToString()) ? null : Convert.ToDecimal(workSheet.Cells[rowIterator, 18].Value.ToString());
									excelDataModel.ChargeAmount = workSheet.Cells[rowIterator, 28].Value == null || String.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 28].Value.ToString()) ? 0 : Convert.ToDecimal(workSheet.Cells[rowIterator, 18].Value.ToString());
									excelDataModel.LineItemAmount = workSheet.Cells[rowIterator, 29].Value == null || String.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 29].Value.ToString()) ? null : Convert.ToDecimal(workSheet.Cells[rowIterator, 18].Value.ToString());
									excelDataModel.LastBillDate = workSheet.Cells[rowIterator, 30].Value == null || String.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 30].Value.ToString()) ? null : Convert.ToDateTime(workSheet.Cells[rowIterator, 15].Value.ToString());

									excelDataModel.RowId = rowIterator;
									excelDataModelList.Add(excelDataModel);

									if (string.IsNullOrWhiteSpace(excelDataModel.PayerName) ||
													string.IsNullOrWhiteSpace(excelDataModel.PatientName) ||
													string.IsNullOrWhiteSpace(excelDataModel.PatientCode) ||
													string.IsNullOrWhiteSpace(excelDataModel.PolicyCode) ||
													excelDataModel.DateOfService == null ||
													excelDataModel.ChargeAmount <= 0)
									{
										errorStatus = new UploadFileDataErrorStatus();
										errorStatus.RowNumber = rowIterator;
										errorStatus.Description = "PayerName, PatientName, PatientCode, PolicyCode, DateOfService, ChargeAmount are Mandatory Fields.";
										errorStatusList.Add(errorStatus);
										validateRequiredFields = false;
										errorCount++;
									}
								}
							}
						}
					}
					else
					{
						errorStatus = new UploadFileDataErrorStatus();
						errorStatus.RowNumber = 0;
						errorStatus.Description = "No of Columns Are Mismatched, As per File Format need to have " + noOfCol + " Columns!";
						errorStatusList.Add(errorStatus);
						errorCount++;
					}
				}

				if (errorCount == 0)
				{
					bool TransactionStatus = false;
					using (TransactionScope transactionScope = new TransactionScope())
					{
						AddFileHistoryReqDTO addFileHistoryReqDTO = new AddFileHistoryReqDTO();
						addFileHistoryReqDTO.FileCategoryId = uploadFileDataReqDTO.FileCategoryId;
						addFileHistoryReqDTO.FileName = fileName;
						addFileHistoryReqDTO.FileExtension = fileExtension;
						addFileHistoryReqDTO.FileSize = Convert.ToString(fileSize);
						addFileHistoryReqDTO.FilePath = fileRes.Data;
						addFileHistoryReqDTO.IsActive = true;

						var addFileHistoryRes = AddFileHistory(addFileHistoryReqDTO);
						if (addFileHistoryRes.Status)
						{
							var addFileDataRes = AddFileData(excelDataModelList, uploadFileDataReqDTO.FileCategoryId, addFileHistoryRes.Data);
							if (addFileDataRes.Status)
							{
								var setTablwWiseDataRes = SetTablwWiseData(excelDataModelList, uploadFileDataReqDTO.FileCategoryId, addFileHistoryRes.Data, errorStatusList);
								if (setTablwWiseDataRes.Status && setTablwWiseDataRes.Data.Count == 0)
								{
									TransactionStatus = true;
								}
								else
								{
									commonResponse = setTablwWiseDataRes;
								}
							}
							else
							{
								commonResponse = addFileDataRes;
							}
						}
						else
						{
							commonResponse = addFileHistoryRes;
						}

						if (TransactionStatus)
						{
							transactionScope.Complete();
							commonResponse.Status = true;
							commonResponse.StatusCode = HttpStatusCode.OK;
							commonResponse.Message = "File Uploaded Successfully!";
							commonResponse.Data = addFileHistoryRes.Data;
						}
					}
				}
				else
				{
					commonResponse.Data = errorStatusList;
				}

			}
			catch { throw; }
			return commonResponse;
		}

		public CommonResponse AddFileHistory(AddFileHistoryReqDTO addFileHistoryReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				int UserId = _commonHelper.GetLoggedInUserId();
				DateTime CurrentDateTime = _commonHelper.GetCurrentDateTime();
				FileHistoryMst fileHistoryMst = new FileHistoryMst();
				fileHistoryMst.FileCategoryId = addFileHistoryReqDTO.FileCategoryId;
				fileHistoryMst.FileName = addFileHistoryReqDTO.FileName;
				fileHistoryMst.FileExtension = addFileHistoryReqDTO.FileExtension;
				fileHistoryMst.FileSize = addFileHistoryReqDTO.FileSize;
				fileHistoryMst.FilePath = addFileHistoryReqDTO.FilePath;
				fileHistoryMst.StartDate = addFileHistoryReqDTO.StartDate;
				fileHistoryMst.EndDate = addFileHistoryReqDTO.EndDate;
				fileHistoryMst.IsActive = addFileHistoryReqDTO.IsActive;
				fileHistoryMst.CreatedBy = UserId;
				fileHistoryMst.UpdatedBy = UserId;
				fileHistoryMst.CreatedDate = CurrentDateTime;
				fileHistoryMst.UpdatedDate = CurrentDateTime;

				_dbContext.FileHistoryMsts.Add(fileHistoryMst);
				_dbContext.SaveChanges();

				commonResponse.Status = true;
				commonResponse.StatusCode = HttpStatusCode.OK;
				commonResponse.Message = "File History Added Successfully!";
				commonResponse.Data = fileHistoryMst.Id;
			}
			catch { throw; }
			return commonResponse;
		}

		public CommonResponse AddFileData(List<UploadFileExcelDataModel> excelDataModelList, int FileCategoryHistoryId, int FileHistoryId)
		{
			CommonResponse commonResponse = new CommonResponse();
			List<FileDataMst> fileDataMstList = new List<FileDataMst>();
			FileDataMst fileDataMst = new FileDataMst();
			try
			{
				int CurrentUserId = _commonHelper.GetLoggedInUserId();
				DateTime CurrentDateTime = _commonHelper.GetCurrentDateTime();
				foreach (var item in excelDataModelList)
				{
					fileDataMst = new FileDataMst();
					fileDataMst.FileCategoryHistoryId = FileCategoryHistoryId;
					fileDataMst.FileHistoryId = FileHistoryId;
					fileDataMst.PayerName = item.PayerName;
					fileDataMst.PayerCode = item.PayerCode;
					fileDataMst.RenderingFullName = item.RenderingFullName;
					fileDataMst.RefferingFullName = item.RefferringFullName;
					fileDataMst.PatientName = item.PatientName;
					fileDataMst.PatientCode = item.PatientCode;
					fileDataMst.PatientDob = item.PatientBirthDate;
					fileDataMst.MedicalRecordCode = item.MedicalRecordCode;
					fileDataMst.Eaibcode = item.EAIBCode;
					fileDataMst.Componant = item.Componant;
					fileDataMst.PayerPhone = item.PayerPhone;
					fileDataMst.PolicyCode = item.PolicyCode;
					fileDataMst.ClaimStatus = item.ClaimStatus;
					fileDataMst.ClaimCode = item.ClaimCode;
					fileDataMst.DateOfService = item.DateOfService;
					fileDataMst.ServiceCpt = item.ServiceCPT;
					fileDataMst.ServiceCode = item.ServiceCode;
					fileDataMst.Modifier = item.ClaimModifier;
					fileDataMst.DiagnosisCode1 = item.DiagnosisCode1;
					fileDataMst.DiagnosisCode2 = item.DiagnosisCode2;
					fileDataMst.DiagnosisCode3 = item.DiagnosisCode3;
					fileDataMst.DiagnosisCode4 = item.DiagnosisCode4;
					fileDataMst.Cob = item.COB;
					fileDataMst.InsuranceAmount1 = item.InsuranceAmount1;
					fileDataMst.InsuranceAmount2 = item.InsuranceAmount2;
					fileDataMst.InsuranceAmount3 = item.InsuranceAmount3;
					fileDataMst.InsuranceAmount4 = item.InsuranceAmount4;
					fileDataMst.ChargeAmount = item.ChargeAmount;
					fileDataMst.LineItemAmount = item.LineItemAmount;
					fileDataMst.LastBillDate = item.LastBillDate;
					fileDataMst.IsActive = true;
					fileDataMst.IsDelete = false;
					fileDataMst.CreatedBy = CurrentUserId;
					fileDataMst.UpdatedBy = CurrentUserId;
					fileDataMst.CreatedDate = CurrentDateTime;
					fileDataMst.UpdatedDate = CurrentDateTime;

					fileDataMstList.Add(fileDataMst);
				}

				if (fileDataMstList.Count() > 0)
				{
					_dbContext.FileDataMsts.AddRange(fileDataMstList);
					_dbContext.SaveChanges();
					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Message = "File Data Added Successfully!";
				}
			}
			catch { throw; }
			return commonResponse;
		}

		public CommonResponse SetTablwWiseData(List<UploadFileExcelDataModel> excelDataModelList, int FileCategoryHistoryId, int FileHistoryId, List<UploadFileDataErrorStatus> errorStatusList)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				var FileCategoryDetail = _commonRepo.getFileCategoryHistoryList().FirstOrDefault(x => x.Id == FileCategoryHistoryId);
				if (FileCategoryDetail != null)
				{
					var FileDataList = _commonRepo.getFileDataList().Where(x => x.FileCategoryHistoryId == FileCategoryHistoryId && x.FileHistoryId == FileHistoryId).ToList();
					var ClaimStatusList = _commonRepo.getClaimStatusList().ToList();
					if (FileDataList.Count > 0)
					{
						AddPayerReqDTO addPayerReqDTO = new AddPayerReqDTO();
						AddAgingPatientReqDTO addAgingPatientReqDTO = new AddAgingPatientReqDTO();
						AddAgingPolicyReqDTO addAgingPolicyReqDTO = new AddAgingPolicyReqDTO();

						foreach (var fileItem in excelDataModelList)
						{
							var ClaimStatusDetails = ClaimStatusList.FirstOrDefault(x => x.ClaimStatusName.ToLower() == fileItem.ClaimStatus.ToLower());
							if (ClaimStatusDetails != null)
							{
								//Payer Details
								addPayerReqDTO = new AddPayerReqDTO();
								addPayerReqDTO.PayerName = fileItem.PayerName;
								addPayerReqDTO.PayerCode = fileItem.PayerCode;
								//addPayerReqDTO.Address = fileItem.Address;
								addPayerReqDTO.Componant = fileItem.Componant;
								//addPayerReqDTO.Mobile = fileItem.Mobile;
								//addPayerReqDTO.Phone = fileItem.Phone;
								//addPayerReqDTO.Email = fileItem.Email;
								//addPayerReqDTO.Website = fileItem.Website;

								var PayerRes = _payerBLL.AddPayer(addPayerReqDTO);

								if (PayerRes.Status)
								{
									//Agigng Patient Details
									addAgingPatientReqDTO = new AddAgingPatientReqDTO();
									addAgingPatientReqDTO.OrganizationId = FileCategoryDetail.OrganizationId;
									addAgingPatientReqDTO.CompanyId = FileCategoryDetail.CompanyId;
									addAgingPatientReqDTO.DepartmentId = FileCategoryDetail.DepartmentId;
									addAgingPatientReqDTO.PayerId = PayerRes.Data;
									//addAgingPatientReqDTO.Address = fileItem.Address;
									//addAgingPatientReqDTO.Phone = fileItem.Phone;
									//addAgingPatientReqDTO.Mobile = fileItem.Mobile;
									//addAgingPatientReqDTO.Email = fileItem.Email;
									addAgingPatientReqDTO.RenderingFullName = fileItem.RenderingFullName;
									addAgingPatientReqDTO.RefferingFullName = fileItem.RefferringFullName;
									addAgingPatientReqDTO.PatientName = fileItem.PatientName;
									addAgingPatientReqDTO.PatientCode = fileItem.PatientCode;
									addAgingPatientReqDTO.PatientDob = fileItem.PatientBirthDate;
									addAgingPatientReqDTO.MedicalRecordCode = fileItem.MedicalRecordCode;
									addAgingPatientReqDTO.Eaibcode = fileItem.EAIBCode;

									var PatientRes = _agingPatientBLL.AddAgingPatient(addAgingPatientReqDTO);

									if (PatientRes.Status)
									{
										//Policy Details
										addAgingPolicyReqDTO = new AddAgingPolicyReqDTO();
										addAgingPolicyReqDTO.OrganizationId = FileCategoryDetail.OrganizationId;
										addAgingPolicyReqDTO.CompanyId = FileCategoryDetail.CompanyId;
										addAgingPolicyReqDTO.DepartmentId = FileCategoryDetail.DepartmentId;
										addAgingPolicyReqDTO.PayerId = PayerRes.Data;
										addAgingPolicyReqDTO.PatientId = PatientRes.Data;
										addAgingPolicyReqDTO.PolicyCode = fileItem.PolicyCode;

										var PolicyRes = _agingPolicyBLL.AddAgingPolicy(addAgingPolicyReqDTO);

										if (PolicyRes.Status)
										{
											//Claim Details
											AddClaimReqDTO addClaimReqDTO = new AddClaimReqDTO();
											addClaimReqDTO.OrganizationId = FileCategoryDetail.OrganizationId;
											addClaimReqDTO.CompanyId = FileCategoryDetail.CompanyId;
											addClaimReqDTO.DepartmentId = FileCategoryDetail.DepartmentId;
											addClaimReqDTO.PayerId = PayerRes.Data;
											addClaimReqDTO.PatientId = PatientRes.Data;
											addClaimReqDTO.PolicyId = PolicyRes.Data;
											addClaimReqDTO.ClaimCode = fileItem.ClaimCode;
											addClaimReqDTO.ClaimStatusId = ClaimStatusDetails.Id;
											addClaimReqDTO.LastBillDate = fileItem.LastBillDate;
											var ClaimRes = _claimBLL.AddClaim(addClaimReqDTO);

											if (ClaimRes.Status)
											{
												//Service Details
												AddAgingServiceReqDTO addAgingServiceReqDTO = new AddAgingServiceReqDTO();
												addAgingServiceReqDTO.OrganizationId = FileCategoryDetail.OrganizationId;
												addAgingServiceReqDTO.CompanyId = FileCategoryDetail.CompanyId;
												addAgingServiceReqDTO.DepartmentId = FileCategoryDetail.DepartmentId;
												addAgingServiceReqDTO.PayerId = PayerRes.Data;
												addAgingServiceReqDTO.PatientId = PatientRes.Data;
												addAgingServiceReqDTO.PolicyId = PolicyRes.Data;
												addAgingServiceReqDTO.ClaimId = ClaimRes.Data;
												addAgingServiceReqDTO.DateOfService = fileItem.DateOfService;
												addAgingServiceReqDTO.ServiceCpt = fileItem.ServiceCPT;
												addAgingServiceReqDTO.ServiceCode = fileItem.ServiceCode;
												addAgingServiceReqDTO.Modifier = fileItem.ClaimModifier;
												addAgingServiceReqDTO.DiagnosisCode1 = fileItem.DiagnosisCode1;
												addAgingServiceReqDTO.DiagnosisCode2 = fileItem.DiagnosisCode2;
												addAgingServiceReqDTO.DiagnosisCode3 = fileItem.DiagnosisCode3;
												addAgingServiceReqDTO.DiagnosisCode4 = fileItem.DiagnosisCode4;
												addAgingServiceReqDTO.Cob = fileItem.COB;
												addAgingServiceReqDTO.InsuranceAmount1 = fileItem.InsuranceAmount1;
												addAgingServiceReqDTO.InsuranceAmount2 = fileItem.InsuranceAmount2;
												addAgingServiceReqDTO.InsuranceAmount3 = fileItem.InsuranceAmount3;
												addAgingServiceReqDTO.InsuranceAmount4 = fileItem.InsuranceAmount4;
												addAgingServiceReqDTO.ChargeAmount = fileItem.ChargeAmount;
												addAgingServiceReqDTO.LineItemAmount = fileItem.LineItemAmount;

												var ServiceRes = _agingServiceBLL.AddAgingService(addAgingServiceReqDTO);

												if (!ServiceRes.Status)
												{
													commonResponse = ServiceRes;
													UploadFileDataErrorStatus errorStatus = new UploadFileDataErrorStatus();
													errorStatus.RowNumber = fileItem.RowId;
													errorStatus.Description = ServiceRes.Message;
													errorStatusList.Add(errorStatus);
												}
											}
											else
											{
												commonResponse = ClaimRes;
												UploadFileDataErrorStatus errorStatus = new UploadFileDataErrorStatus();
												errorStatus.RowNumber = fileItem.RowId;
												errorStatus.Description = ClaimRes.Message;
												errorStatusList.Add(errorStatus);
											}
										}
										else
										{
											commonResponse = PolicyRes;
											UploadFileDataErrorStatus errorStatus = new UploadFileDataErrorStatus();
											errorStatus.RowNumber = fileItem.RowId;
											errorStatus.Description = PolicyRes.Message;
											errorStatusList.Add(errorStatus);
										}
									}
									else
									{
										commonResponse = PatientRes;
										UploadFileDataErrorStatus errorStatus = new UploadFileDataErrorStatus();
										errorStatus.RowNumber = fileItem.RowId;
										errorStatus.Description = PatientRes.Message;
										errorStatusList.Add(errorStatus);
									}
								}
								else
								{
									commonResponse = PayerRes;
									UploadFileDataErrorStatus errorStatus = new UploadFileDataErrorStatus();
									errorStatus.RowNumber = fileItem.RowId;
									errorStatus.Description = PayerRes.Message;
									errorStatusList.Add(errorStatus);
								}
							}
							else
							{
								UploadFileDataErrorStatus errorStatus = new UploadFileDataErrorStatus();
								errorStatus.RowNumber = fileItem.RowId;
								errorStatus.Description = "Claim Status Invalid or Not Found!";
								errorStatusList.Add(errorStatus);
								commonResponse.Message = "Claim Status Invalid or Not Found!";
							}
						}
					}
				}
				else
				{
					commonResponse.Message = "File Category Not Found!";
					commonResponse.StatusCode = HttpStatusCode.NotFound;
				}

				commonResponse.Data = errorStatusList;
				if (errorStatusList.Count == 0)
				{
					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Message = "Data Inserted Successfully in All Related Tables!";
				}

			}
			catch { throw; }
			return commonResponse;
		}
	}
}
