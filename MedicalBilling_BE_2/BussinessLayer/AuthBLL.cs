using Azure;
using DataLayer.Entities;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Spreadsheet;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Helper.Models;
using Mapster;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
	public class AuthBLL
	{
		private readonly MedicalBillingDbContext _dbContext;
		private readonly CommonRepo _commonRepo;
		private readonly IConfiguration _iConfiguration;
		private readonly AuthRepo _authRepo;
		private readonly CommonHelper _commonHelper;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private IHostingEnvironment _hostingEnvironment { get; }
		//private List<SiderbarResDTO> siderbarRes;
		public AuthBLL(MedicalBillingDbContext dbContext, CommonRepo commonRepo, AuthRepo authRepo, IConfiguration iConfiguration, CommonHelper commonHelper, IHostingEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor)
		{
			_dbContext = dbContext;
			_commonRepo = commonRepo;
			_authRepo = authRepo;
			_iConfiguration = iConfiguration;
			_commonHelper = commonHelper;
			_hostingEnvironment = hostingEnvironment;
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<CommonResponse> LoginAsync(LogInReqDTO logInReqDTO)
		{
			CommonResponse response = new CommonResponse();
			LogInResDTO logInResDTO = new LogInResDTO();
			logInResDTO.UserDetail = new UserDetail();
			logInResDTO.RoleDetail = new RoleDetail();
			logInResDTO.Permissions = new List<Permissions>();

			var UserList = _commonRepo.getAllUsers();
			var RoleList = _commonRepo.getAllRoles();

			var UserDetail = UserList.FirstOrDefault(x => x.UserName == logInReqDTO.UserName && x.Password == logInReqDTO.Password);
			try
			{
				if (UserDetail != null)
				{
					var RoleDetail = RoleList.FirstOrDefault(x => x.Id == UserDetail.Role);
					var token = _authRepo.CreateJWT(UserDetail.Id, false);
					if (token != null && !string.IsNullOrWhiteSpace(token.Token) && !string.IsNullOrWhiteSpace(token.RefreshToken))
					{
						//Token & Refresh Token
						logInResDTO.Token = token.Token;
						logInResDTO.RefreshToken = token.RefreshToken;

						//UserDetail
						logInResDTO.UserDetail.Id = UserDetail.Id;
						logInResDTO.UserDetail.FirstName = UserDetail.FirstName;
						logInResDTO.UserDetail.LastName = UserDetail.LastName;
						logInResDTO.UserDetail.Email = UserDetail.Email;
						logInResDTO.UserDetail.IsActive = UserDetail.IsActive;
						logInResDTO.UserDetail.UserName = UserDetail.UserName;

						//RoleDetail
						logInResDTO.RoleDetail.RoleId = RoleDetail.Id;
						logInResDTO.RoleDetail.RoleName = RoleDetail.RoleName;

						//Permissions
						logInResDTO.Permissions = GetPermissionsByUserId(UserDetail.Id);

						//SidebarList
						logInResDTO.SidebarList = GetSidebarList(UserDetail.Id);

						response.Status = true;
						response.StatusCode = HttpStatusCode.OK;
						response.Message = "LogIn Sucessfully.";
						response.Data = logInResDTO;
					}
					else
					{
						response.StatusCode = HttpStatusCode.Unauthorized;
						response.Message = "Token not Generated";
					}
				}
				else
				{
					response.StatusCode = HttpStatusCode.BadRequest;
					response.Message = "Please Enter valid UserName and Password!";
				}
			}
			catch { throw; }
			return response;

		}
	
		public CommonResponse GetRefreshToken(GetRefreshTokenReqDTO refreshTokenReqDTO)
		{
			CommonResponse response = new CommonResponse();
			response = _authRepo.ValidateToken(refreshTokenReqDTO.Adapt<TokenModel>());
			return response;
		}

		public string EncryptString(string plainText)
		{
			return _commonHelper.EncryptString(plainText);
		}

		public string DecryptString(string cipherText)
		{
			return _commonHelper.DecryptString(cipherText);
		}

		public List<Permissions> GetPermissionsByUserId(int UserId)
		{
			List<Permissions> permissions = new List<Permissions>();
			try
			{
				var UserDetail = _commonRepo.getAllUsers().FirstOrDefault(x => x.Id == UserId);
				if (UserDetail != null)
				{
					var PermissionList = _commonRepo.getAllPermissions().ToList();
					var DefaultPermissionIdList = _commonRepo.getAllDefaultPermissions().Where(x => x.RoleId == UserDetail.Role).Select(x => x.PermissionId).ToList();
					var UserPermissionIdList = _commonRepo.getAllUserPermissions().Where(x => x.UserId == UserId).Select(x => x.PermissionId).ToList();

					bool hasPermission = false;
					Permissions permissionsDetail = new Permissions();
					foreach (var item in PermissionList)
					{
						hasPermission = DefaultPermissionIdList.Contains(item.Id) ? true : false;
						if (!hasPermission)
							hasPermission = UserPermissionIdList.Contains(item.Id) ? true : false;

						permissionsDetail = new Permissions();
						permissionsDetail.PermissionId = item.Id;
						permissionsDetail.PermissionName = item.PermissionName;
						permissionsDetail.PermissionCode = item.PermissionCode;
						permissionsDetail.HasPermission = hasPermission;

						permissions.Add(permissionsDetail);
					}
				}
			}
			catch { throw; }
			return permissions;
		}

		public List<MenuDetail> GetMenuList()
		{
			List<MenuDetail> MenuList = new List<MenuDetail>();

			//MenuDetail details = new MenuDetail();
			//details.Id = 1;
			//details.Name = "Dashboard";
			//details.Navigate = "/dashboard";
			//details.AngleIcon = "Angleleft";
			//details.Icon = "DashbordIcon";
			//details.ParentId = 0;
			//details.LevelId = 1;
			//details.PermissionCode = "Dashboard";
			//details.Active = details.Name.ToLower();
			//MenuList.Add(details);

			//details = new MenuDetail();
			//details.Id = 2;
			//details.Name = "Masters";
			//details.Navigate = "";
			//details.AngleIcon = "Angleleft";
			//details.Icon = "";
			//details.ParentId = 0;
			//details.LevelId = 1;
			//details.PermissionCode = "";
			//details.Active = details.Name.ToLower();
			//MenuList.Add(details);

			//details = new MenuDetail();
			//details.Id = 3;
			//details.Name = "User";
			//details.Navigate = "/Masters/User";
			//details.AngleIcon = "Angleleft";
			//details.Icon = "";
			//details.ParentId = 2;
			//details.LevelId = 2;
			//details.PermissionCode = "Aging_User_View";
			//details.Active = details.Name.ToLower();
			//MenuList.Add(details);

			//details = new MenuDetail();
			//details.Id = 4;
			//details.Name = "Permissions";
			//details.Navigate = "";
			//details.AngleIcon = "Angleleft";
			//details.Icon = "";
			//details.ParentId = 2;
			//details.LevelId = 2;
			//details.PermissionCode = "";
			//details.Active = details.Name.ToLower();
			//MenuList.Add(details);

			//details = new MenuDetail();
			//details.Id = 5;
			//details.Name = "Default Permissions";
			//details.Navigate = "";
			//details.AngleIcon = "Angleleft";
			//details.Icon = "";
			//details.ParentId = 4;
			//details.LevelId = 3;
			//details.PermissionCode = "Default_Permission_View";
			//details.Active = details.Name.ToLower();
			//MenuList.Add(details);

			//details = new MenuDetail();
			//details.Id = 6;
			//details.Name = "User Permissions";
			//details.Navigate = "";
			//details.AngleIcon = "Angleleft";
			//details.Icon = "";
			//details.ParentId = 4;
			//details.LevelId = 3;
			//details.PermissionCode = "";
			//details.Active = details.Name.ToLower();
			//MenuList.Add(details);

			//details = new MenuDetail();
			//details.Id = 7;
			//details.Name = "Scheduling";
			//details.Navigate = "";
			//details.AngleIcon = "Angleleft";
			//details.Icon = "SchedulingIcon";
			//details.ParentId = 0;
			//details.LevelId = 1;
			//details.PermissionCode = "";
			//details.Active = details.Name.ToLower();
			//MenuList.Add(details);

			//details = new MenuDetail();
			//details.Id = 8;
			//details.Name = "Appointment";
			//details.Navigate = "scheduling/scheduling-patient";
			//details.AngleIcon = "Angleleft";
			//details.Icon = "";
			//details.ParentId = 7;
			//details.LevelId = 2;
			//details.PermissionCode = "Scheduling_Appointment";
			//details.Active = details.Name.ToLower();
			//MenuList.Add(details);

			//details = new MenuDetail();
			//details.Id = 9;
			//details.Name = "Access";
			//details.Navigate = "/newScheduling";
			//details.AngleIcon = "Angleleft";
			//details.Icon = "";
			//details.ParentId = 7;
			//details.LevelId = 2;
			//details.PermissionCode = "Scheduling_Accept_Reject";
			//details.Active = details.Name.ToLower();
			//MenuList.Add(details);

			//var json = JsonConvert.SerializeObject(MenuList);
			var Path = _commonHelper.GetFilePaths("Utilities", CommonConstant.Json, false, "MenuData");
			var JsonData = _commonHelper.ReadJsonFile(Path.FilePhysicalPath);
			MenuList = JsonConvert.DeserializeObject<List<MenuDetail>>(JsonData);
			return MenuList;
		}

		public List<SiderbarResDTO> GetSidebarList(int UserId)
		{
			List<SiderbarResDTO> siderbarResDTOs = new List<SiderbarResDTO>();
			try
			{
				var MenuList = GetMenuList();
				SiderbarResDTO siderbar = new SiderbarResDTO();
				var Level1List = MenuList.Where(x => x.ParentId == 0).ToList();
				//int UserId = _commonHelper.GetLoggedInUserId();
				var PermissionCodeList = GetPermissionsByUserId(UserId).Where(x => x.HasPermission == true).Select(x => x.PermissionCode).ToList();

				MenuList = MenuList.Where(x => PermissionCodeList.Contains(x.PermissionCode) || string.IsNullOrWhiteSpace(x.PermissionCode)).ToList();
				foreach (var level1Item in Level1List)
				{
					var Level2List = MenuList.Where(x => x.ParentId == level1Item.Id && x.ParentId != 0).ToList();
					var Level2PermissionCodeList = Level2List.Select(x => x.PermissionCode).ToList();
					if (PermissionCodeList.Contains(level1Item.PermissionCode) || PermissionCodeList.Where(x => Level2PermissionCodeList.Contains(x)).Count() > 0 || Level2PermissionCodeList.Contains(""))
					{
						SiderbarResDTO level1 = new SiderbarResDTO();
						level1.Id = level1Item.Id;
						level1.Name = level1Item.Name;
						level1.Navigate = level1Item.Navigate;
						level1.AngleIcon = level1Item.AngleIcon;
						level1.Icon = level1Item.Icon;
						level1.ParentId = level1Item.ParentId;
						level1.PermissionCode = level1Item.PermissionCode;
						level1.Active = level1Item.Active;
						level1.Childs = new List<SiderbarResDTO>();
						siderbarResDTOs.Add(level1);

						foreach (var level2Item in Level2List)
						{
							var Level3List = MenuList.Where(x => x.ParentId == level2Item.Id && x.ParentId != 0 && x.ParentId != level2Item.ParentId).ToList();
							var Level3PermissionCodeList = Level3List.Select(x => x.PermissionCode).ToList();
							if (PermissionCodeList.Contains(level2Item.PermissionCode) || PermissionCodeList.Where(x => Level3PermissionCodeList.Contains(x)).Count() > 0 || Level3PermissionCodeList.Contains(""))
							{
								SiderbarResDTO level2 = new SiderbarResDTO();
								level2.Id = level2Item.Id;
								level2.Name = level2Item.Name;
								level2.Navigate = level2Item.Navigate;
								level2.AngleIcon = level2Item.AngleIcon;
								level2.Icon = level2Item.Icon;
								level2.ParentId = level2Item.ParentId;
								level2.PermissionCode = level2Item.PermissionCode;
								level2.Active = level2Item.Active;
								level2.Childs = new List<SiderbarResDTO>();
								level1.Childs.Add(level2);

								foreach (var level3Item in Level3List)
								{
									var Level4List = MenuList.Where(x => x.ParentId == level3Item.Id && x.ParentId != 0 && x.ParentId != level3Item.ParentId).ToList();
									var Level4PermissionCodeList = Level4List.Select(x => x.PermissionCode).ToList();
									if (PermissionCodeList.Contains(level3Item.PermissionCode) || PermissionCodeList.Where(x => Level4PermissionCodeList.Contains(x)).Count() > 0 || Level4PermissionCodeList.Contains(""))
									{
										SiderbarResDTO level3 = new SiderbarResDTO();
										level3.Id = level3Item.Id;
										level3.Name = level3Item.Name;
										level3.Navigate = level3Item.Navigate;
										level3.AngleIcon = level3Item.AngleIcon;
										level3.Icon = level3Item.Icon;
										level3.ParentId = level3Item.ParentId;
										level3.PermissionCode = level3Item.PermissionCode;
										level3.Active = level3Item.Active;
										level3.Childs = new List<SiderbarResDTO>();
										level2.Childs.Add(level3);

										foreach (var level4Item in Level4List)
										{
											var Level5List = MenuList.Where(x => x.ParentId == level4Item.Id && x.ParentId != 0 && x.ParentId != level4Item.ParentId).ToList();
											var Level5PermissionCodeList = Level5List.Select(x => x.PermissionCode).ToList();
											if (PermissionCodeList.Contains(level4Item.PermissionCode) || PermissionCodeList.Where(x => Level5PermissionCodeList.Contains(x)).Count() > 0 || Level5PermissionCodeList.Contains(""))
											{
												SiderbarResDTO level4 = new SiderbarResDTO();
												level4.Id = level4Item.Id;
												level4.Name = level4Item.Name;
												level4.Navigate = level4Item.Navigate;
												level4.AngleIcon = level4Item.AngleIcon;
												level4.Icon = level4Item.Icon;
												level4.ParentId = level4Item.ParentId;
												level4.PermissionCode = level4Item.PermissionCode;
												level4.Active = level4Item.Active;
												level4.Childs = new List<SiderbarResDTO>();
												level3.Childs.Add(level4);

												foreach (var level5Item in Level5List)
												{
													if (PermissionCodeList.Contains(level5Item.PermissionCode))
													{
														SiderbarResDTO level5 = new SiderbarResDTO();
														level5.Id = level5Item.Id;
														level5.Name = level5Item.Name;
														level5.Navigate = level5Item.Navigate;
														level5.AngleIcon = level5Item.AngleIcon;
														level5.Icon = level5Item.Icon;
														level5.ParentId = level5Item.ParentId;
														level5.PermissionCode = level5Item.PermissionCode;
														level5.Active = level5Item.Active;
														level5.Childs = new List<SiderbarResDTO>();
														level4.Childs.Add(level5);
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}

			}
			catch { throw; }
			return siderbarResDTOs;
		}

		public CommonResponse ForgotPassword(ForgotPasswordReqDTO forgotReqDTO)
		{
			CommonResponse commonResponse = new();
			try
			{
				var baseURL = _iConfiguration.GetSection("ConnectionStrings:ForgetPasswordLinkBaseURL").Value;

				var ISExistmail = _commonRepo.getAllUsers().Where(x => x.Email == forgotReqDTO.Email).FirstOrDefault();
				if (ISExistmail != null)
				{
					var userid = ISExistmail.Id;

					var datetimevalue = _commonHelper.GetCurrentDateTime().ToString("ddMMyyyyhhmmsstt");
					baseURL += "?q=" + userid + "&d=" + datetimevalue;

					var ImagePath = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot", "EmailTemplate", "logo.jpg");
					var emailTemplatePath = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot", "EmailTemplate", "EmailTemplate.html");
					StreamReader str = new StreamReader(emailTemplatePath);
					string MailText = str.ReadToEnd();
					str.Close();

					var htmlBody = MailText.Replace("[Resetlink]", "<a target='_blank' href='" + baseURL + "'>Reset Password</a>").Replace("[Username]", ISExistmail.FirstName + " " + ISExistmail.LastName);
					htmlBody = htmlBody.Replace("logo.png", ImagePath);
					SendEmailRequestModel sendEmailRequestModel = new SendEmailRequestModel();
					sendEmailRequestModel.ToEmail = ISExistmail.Email;
					sendEmailRequestModel.Body = htmlBody;
					sendEmailRequestModel.Subject = "Reset Password Link";

					var EmailSend = _commonHelper.SendEmail(sendEmailRequestModel);
					var IsLinkSave = AddResetPasswordLink(userid, baseURL);

					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Message = "Password Reset Link Has Been Sent To Your Email!";
				}
				else
				{
					commonResponse.Status = false;
					commonResponse.StatusCode = HttpStatusCode.BadRequest;
					commonResponse.Message = "Email Not Found!";
				}
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		public CommonResponse ResetPassword(ResetPasswordReqDTO resetPasswordReqDTO)
		{
			CommonResponse commonResponse = new();
			try
			{
				var Id = resetPasswordReqDTO.UserId;
				int userId = Convert.ToInt32(Id);
				var IsExistId = _commonRepo.getAllUsers().Where(x => x.Id == userId && x.Email == resetPasswordReqDTO.Email).FirstOrDefault();
				if (IsExistId != null)
				{
					IsExistId.Password = resetPasswordReqDTO.NewPassword; 

					_dbContext.Entry(IsExistId).State = EntityState.Modified;
					_dbContext.SaveChanges();

					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Message = "Reset Password Sucessfully!";
				}
				else
				{
					commonResponse.Status = false;
					commonResponse.StatusCode = HttpStatusCode.BadRequest;
					commonResponse.Message = "Wrong Credentials.!";
				}

			}
			catch (Exception)
			{
				throw;

			}
			return commonResponse;
		}

		private bool AddResetPasswordLink(int Id, string baseURL)
		{
			int id = Convert.ToInt32(Id);
			LinkMst linkMst = new LinkMst();
			linkMst.UserId = id;
			linkMst.IsClicked = false;
			linkMst.ResetPasswordLink = baseURL;
			linkMst.CreatedDate = _commonHelper.GetCurrentDateTime();
			linkMst.ExpiredDate = _commonHelper.GetCurrentDateTime().AddDays(1);
			linkMst.IsActive = true;
			linkMst.IsDelete = false;
			linkMst.CreatedBy = _commonHelper.GetLoggedInUserId();
			linkMst.UpdatedBy = _commonHelper.GetLoggedInUserId();
			linkMst.UpdatedDate = DateTime.Now;
			_dbContext.LinkMsts.Add(linkMst);
			_dbContext.SaveChanges();

			return true;
		}

		public CommonResponse CheckResetPasswordLink(CheckResetPasswordLinkReqDTO checkResetPasswordLinkReqDTO)
		{
			CommonResponse commonResponse = new();
			try
			{
				if (!string.IsNullOrEmpty(checkResetPasswordLinkReqDTO.Id) && !string.IsNullOrEmpty(checkResetPasswordLinkReqDTO.Link) && !string.IsNullOrEmpty(checkResetPasswordLinkReqDTO.SecurityCode))

				{
					var Id = checkResetPasswordLinkReqDTO.Id;
					int userId = Convert.ToInt32(Id);
					var IsExistLink = _commonRepo.linkList().Where(x => x.UserId == userId && x.ResetPasswordLink == checkResetPasswordLinkReqDTO.Link && x.IsClicked == false).FirstOrDefault();
					int LinkExpiryMin = Convert.ToInt32(_iConfiguration["EmailExpiryTime:ExpiryMinutes"]);

					if (IsExistLink != null)
					{
						if (IsExistLink.ExpiredDate <= _commonHelper.GetCurrentDateTime())
						{
							commonResponse.Message = "Link is expried";
						}
						else
						{
							var datetime = checkResetPasswordLinkReqDTO.SecurityCode;
							DateTime date = DateTime.ParseExact(datetime, "ddMMyyyyhhmmsstt", null);
							date = date.AddHours(LinkExpiryMin);
							if (_commonHelper.GetCurrentDateTime() <= date)
							{
								IsExistLink.IsClicked = true;
								_dbContext.Entry(IsExistLink).State = EntityState.Modified;
								_dbContext.SaveChanges();
								commonResponse.Status = true;
								commonResponse.StatusCode = HttpStatusCode.OK;
								commonResponse.Message = "Link is valid.";
							}
							else
							{
								commonResponse.Message = "Link is expried";
							}
						}
					}
					else
					{
						commonResponse.Message = "Link is expried";
					}
				}
				else
				{
					commonResponse.Message = "Link is Expried";
				}
			}
			catch (Exception)
			{
				throw;
			}
			return commonResponse;
		}

		public CommonResponse ChangePassword(ChangePasswordReqDTO changePasswordReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			ChangePasswordResDTO changePasswordResDTO = new ChangePasswordResDTO();
			try
			{
				var response = _commonRepo.getAllUsers().FirstOrDefault(x => x.Id == changePasswordReqDTO.UserId && x.Password ==  changePasswordReqDTO.OldPassword);
				if (response != null)
				{
					response.Password = changePasswordReqDTO.ConfirmPassword;
					response.UpdatedBy = _commonHelper.GetLoggedInUserId();
					response.UpdatedDate = DateTime.Now;

					_dbContext.Entry(response).State = EntityState.Modified;
					_dbContext.SaveChanges();

					changePasswordResDTO.NewPassword = response.Password;

					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Message = "Password changed successfully !";
				}
				else
				{
					commonResponse.Message = "Old password is incorrect !";
					commonResponse.StatusCode = HttpStatusCode.BadRequest;
				}
			}
			catch { throw; }
			return commonResponse;
		}
	}
}
