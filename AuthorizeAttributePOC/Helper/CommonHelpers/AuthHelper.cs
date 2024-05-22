using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper.CommonModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static Helper.CommonModels.CommonEnums;

namespace Helper.CommonHelpers
{
    public class AuthHelper
    {
        private readonly DBContext _dbContext;
        private readonly CommonRepo _commonRepo;
        private readonly IConfiguration _configuration;
        private readonly CommonHelper _commonHelper;

        public AuthHelper(DBContext dbContext, CommonRepo commonRepo, IConfiguration configuration, CommonHelper commonHelper)
        {
            _dbContext = dbContext;
            _commonRepo = commonRepo;
            _configuration = configuration;
            _commonHelper = commonHelper;
        }
        public async Task<CommonResponse> Login(LoginReqDTO request)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                LoginResDTO res = new LoginResDTO();
                DateTime currentDateTime = _commonHelper.GetCurrentDateTime();

                var userDetail = await (from user in _commonRepo.UserMstList().Where(x => x.Email == request.EmailId.Trim() && x.Password == request.Password.Trim() && x.UserStatusId != 3)
                                        join userStatus in _commonRepo.UserStatusMstList().Where(x => x.UserStatus != UserStatus.Suspend.ToString())
                                        on user.UserStatusId equals userStatus.Id
                                        select user).FirstOrDefaultAsync();

                if (userDetail != null)
                {
                    var tokenString = await GenerateToken(userDetail.Id.ToString(), userDetail.UserType);
                    string refreshtokenstring = await GenerateRefreshToken();

                    using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        UserTokenMst tokenDetail = await _commonRepo.UserTokenMstList().FirstOrDefaultAsync(x => x.UserId == userDetail.Id);

                        if (tokenDetail != null)
                        {
                            tokenDetail.Token = tokenString;
                            tokenDetail.TokenExpiryTime = currentDateTime.AddMinutes(Convert.ToInt32(_configuration["JsonWebTokenKeys:TokenExpiryMinutes"]));
                            tokenDetail.RefreshToken = refreshtokenstring;
                            tokenDetail.RefreshTokenExpiryTime = currentDateTime.AddMinutes(Convert.ToInt32(_configuration["JsonWebTokenKeys:RefreshTokenExpiryMinutes"]));
                            //tokenDetail.ForMobile = request.ForMobile;
                            _dbContext.Entry(tokenDetail).State = EntityState.Modified;
                            await _dbContext.SaveChangesAsync();

                        }
                        else
                        {
                            tokenDetail = new UserTokenMst();
                            tokenDetail.CreatedDate = currentDateTime;
                            tokenDetail.UpdatedDate = currentDateTime;
                            tokenDetail.UserId = userDetail.Id;
                            //tokenDetail.ForMobile = request.ForMobile;
                            tokenDetail.Token = tokenString;
                            tokenDetail.TokenExpiryTime = currentDateTime.AddMinutes(Convert.ToInt32(_configuration["JsonWebTokenKeys:TokenExpiryMinutes"]));
                            tokenDetail.RefreshToken = refreshtokenstring;
                            tokenDetail.RefreshTokenExpiryTime = currentDateTime.AddMinutes(Convert.ToInt32(_configuration["JsonWebTokenKeys:RefreshTokenExpiryMinutes"]));
                            await _dbContext.UserTokenMsts.AddAsync(tokenDetail);
                            await _dbContext.SaveChangesAsync();

                        }

                        //res.IsWebLoggedInFirstTime = userDetail.WebLastLogin == null;

                        //if (!request.ForMobile)
                        //{
                        //    userDetail.WebLastLogin = currentDateTime;
                        //}
                        //else
                        //{
                        //    await _commonRepo.UserMstList().Where(x => x.DeviceId == request.DeviceToken).ForEachAsync(x => x.DeviceId = null);
                        //    await _dbContext.SaveChangesAsync();

                        //    userDetail.DeviceId = request.DeviceToken;
                        //    userDetail.MobileLastLogin = currentDateTime;
                        //}
                        //_dbContext.Entry(userDetail).State = EntityState.Modified;
                        //await _dbContext.SaveChangesAsync();

                        //var userWiseNotSeenCount = await _commonRepo.NotificationReceiverDetailList()
                        //    .CountAsync(n => n.Status == CommonEnums.RequestTypeStatus.Pending.ToString() && n.ReceiverUserId == userDetail.Id);



                        //if (userDetail.UserType == CommonEnums.UserType.Admin.ToString())
                        //{
                        //    res.AdminPermission = await _commonRepo.PermissionMstList().Select(x => new { x.Id, x.PermissionTitle, x.Status }).ToListAsync();
                        //}
                        //else if ((userDetail.UserType == CommonEnums.UserType.Tutor.ToString() ||
                        //    userDetail.UserType == CommonEnums.UserType.Student.ToString()) && request.ForMobile == true)
                        //{
                        //    _commonHelper.SendPushNotification(new List<string> { userDetail.DeviceId }, new PushyNotificationRequestModel()
                        //    {
                        //        Title = "Welcome",
                        //        Message = $"Welcome back {userDetail.FirstName} {userDetail.LastName}",
                        //        Description = $"{userDetail.FirstName}, We're glad to see you again!"
                        //    });
                        //}

                        scope.Complete();

                        res.UserId = userDetail.Id;
                        res.Token = tokenString;
                        res.RefreshToken = refreshtokenstring;
                        res.RoleType = userDetail.UserType;
                        res.FullName = $"{userDetail.FirstName} {userDetail.LastName}";
                        res.TokenExpiryTime = tokenDetail.TokenExpiryTime;
                        //res.PendingNotificationCount = userWiseNotSeenCount;

                        response.Data = res;
                        response.Status = true;
                        response.Message = CommonConstant.LoggedInSuccessfully;
                        response.StatusCode = HttpStatusCode.OK;
                    }
                }
                else
                {
                    response.Message = "Email or Password is invalid!";
                    response.StatusCode = HttpStatusCode.BadRequest;
                }
            }
            catch { throw; }
            return response;
        }

        public async Task<string> GenerateToken(string UserId, string Role)
        {
            DateTime currentDateTime = _commonHelper.GetCurrentDateTime();

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JsonWebTokenKeys:IssuerSigningKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                          new Claim(ClaimTypes.Name,UserId),
                          new Claim(ClaimTypes.Role,Role),
            };
            var token = new JwtSecurityToken(_configuration["JsonWebTokenKeys:ValidIssuer"],
                _configuration["JsonWebTokenKeys:ValidAudience"],
                claims,

                expires: currentDateTime.AddMinutes(Convert.ToInt32(_configuration["JsonWebTokenKeys:TokenExpiryMinutes"])),
                signingCredentials: credentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }

        public async Task<string> GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            string refreshtokenstring = "";

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                refreshtokenstring = Convert.ToBase64String(randomNumber);
            }
            return refreshtokenstring;
        }
    }
}
