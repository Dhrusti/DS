using DataLayer;
using DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Json;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace BusinessLayer
{
    public class AuthenticationBLL
    {
        private readonly HIMSAuthenticationDBContext _context;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public AuthenticationBLL(HttpClient httpClient, IConfiguration configuration, HIMSAuthenticationDBContext context)
        {
            this._httpClient = httpClient;
            this._configuration = configuration;
            this._context = context;
        }

        public async Task<ResponseDTO> UserLoginAsync(LoginRequestDTO loginRequestDTO)
        {
            LoginResponseDTO loginResponseDTO = new LoginResponseDTO();
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                HttpResponseMessage Res = await _httpClient.GetAsync(_configuration["UsersMicroserviceBaseURL"] + "api/User/GetallUsersAsync");
                if (Res.IsSuccessStatusCode)
                {
                    Res = new HttpResponseMessage();
                    List<UserMstDTO> userMstDTO = new List<UserMstDTO>();
                    try
                    {
                        userMstDTO = JsonConvert.DeserializeObject<List<UserMstDTO>>(JObject.Parse(Res.Content.ReadAsStringAsync().Result)["data"].ToString());
                    } catch (Exception ex)
                    {
                        userMstDTO = new List<UserMstDTO>();
                    }
                }

                /*var userMstDTO = await _httpClient.GetFromJsonAsync<dynamic>(_configuration["UsersMicroserviceBaseURL"] + "api/User/GetallUsersAsync");
                var data = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(userMstDTO);
                var data1 = JsonSerializer.Deserialize<UserMstDTO>(data[2].Value);
                
                
                var d = JsonDocument.Parse(data);
                var a = d.RootElement.EnumerateObject();*/


                //var userMsts = userMstDTO != null ? userMstDTO["Array"].Values : null;


                //    Type ApplicantInfo = userMstDTO.GetType();
                //PropertyInfo[] properties = ApplicantInfo.GetProperties();


                //var userMstDTO = await _httpClient.GetFromJsonAsync<LoginResponseDTO>(_configuration["UsersMicroserviceBaseURL"] + "api/User/GetallUsersAsync");

                /*if (userMstDTO != null && userMstDTO.Data.Count > 0)
                {
                    var data = userMstDTO.Data.Where(x => x.UserName == loginRequestDTO.UserName && x.Password == loginRequestDTO.Password).SingleOrDefault();

                    if (data != null)
                    {
                        data.Token = CreateJWT(data.UserName);
                        data.RefreshToken = CreateRefreshToken();

                        responseDTO.Data = data;
                        responseDTO.StatusCode = HttpStatusCode.OK;
                        responseDTO.Message = "User Successfully Logged In!";


                        bool IsTokenSaved = AddToken(data.UserName, data.Token, data.RefreshToken, "");
                    }
                    else
                    {
                        responseDTO.Data = data;
                        responseDTO.StatusCode = HttpStatusCode.NotFound;
                        responseDTO.Message = "Please check Username and Password OR User does not exist!";
                    }
                }*/
            }
            catch (Exception ex)
            {
                responseDTO.Data = ex.ToString();
                responseDTO.Message = ex.Message;
                responseDTO.StatusCode = HttpStatusCode.ExpectationFailed;
            }
            return responseDTO;

        }

        public async Task<ResponseDTO> GenerateTokenFromRefreshTokenAsync(RefreshTokenRequestDTO loginRequestDTO)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            RefreshTokenResponseDTO refreshTokenResponseDTO = new RefreshTokenResponseDTO();

            string accessToken = loginRequestDTO.Token;
            string refreshToken = loginRequestDTO.RefreshToken;
            var principal = GetPrincipalFromExpiredToken(accessToken);
            var username = principal.Identity.Name; //this is mapped to the Name claim by default

            var loginResponseDTO = await _httpClient.GetFromJsonAsync<LoginResponseDTO>(_configuration["UsersMicroserviceBaseURL"] + "api/User/GetallUsersAsync");
            var userMstDTO = loginResponseDTO.Data.FirstOrDefault(x => x.UserName == username);;

            var newAccessToken = CreateJWT(username);
            var newRefreshToken = CreateRefreshToken();

            refreshTokenResponseDTO.Token = newAccessToken;
            refreshTokenResponseDTO.RefreshToken = newRefreshToken;

            responseDTO.StatusCode = HttpStatusCode.OK;
            responseDTO.Message = "Token generated successfully";
            responseDTO.Data = refreshTokenResponseDTO;

            bool IsTokenSaved = AddToken(userMstDTO.UserName, newAccessToken, newRefreshToken, "");
            return responseDTO;
        }

        /// <summary>
        /// Method create token based on username and retrun token
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private string CreateJWT(string userName)
        {
            var secreatekey = this._configuration.GetSection("JWTSetting:key").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secreatekey));
            var claims = new Claim[] {
             new Claim(ClaimTypes.Name,userName)
            };
            var signingcredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {

                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddSeconds(60),
                SigningCredentials = signingcredentials,


            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);


            return tokenHandler.WriteToken(token);
        }
        /// <summary>
        /// create refresh token
        /// </summary>
        /// <returns>refresh token</returns>
        private string CreateRefreshToken()
        {

            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        /// <summary>
        /// Get principal value from toekn 
        /// </summary>
        /// <param name="token">token</param>
        /// <returns></returns>
        /// <exception cref="SecurityTokenException"></exception>
        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var secreatekey = this._configuration.GetSection("JWTSetting:key").Value;
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secreatekey)),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }
        /// <summary>
        /// Generate token from token and refresh token
        /// </summary>
        /// <param name="request">request token and refresh token value</param>
        /// <returns>sucess message with taoken and refresh token</returns>
        public ResponseDTO GenerateTokenFromRefreshToken(RefreshTokenRequestDTO request)
        {
            ResponseDTO response = new ResponseDTO();
            RefreshTokenResponseDTO responseModel = new RefreshTokenResponseDTO();

            string accessToken = request.Token;
            string refreshToken = request.RefreshToken;
            var principal = GetPrincipalFromExpiredToken(accessToken);
            var username = principal.Identity.Name; //this is mapped to the Name claim by default
            //var user = this._context.Users.FirstOrDefault(x => x.UserName == username);
            var user = "ajay.zala@archesoftronix.com";

            var newAccessToken = CreateJWT(username);
            var newRefreshToken = CreateRefreshToken();
            responseModel.Token = newAccessToken;
            responseModel.RefreshToken = newRefreshToken;
            response.StatusCode = HttpStatusCode.OK;
            response.Message = "Token genrated successfully";
            response.Data = responseModel;

            bool IsTokenSaved = AddToken(user, newAccessToken, newRefreshToken, "");
            return response;
        }
        /// <summary>
        /// Save token value into database 
        /// </summary>
        /// <param name="UserId">User id</param>
        /// <param name="Token">Token</param>
        /// <param name="RefreshToken">Refresh token</param>
        /// <param name="IP">IP</param>
        /// <returns>true or false</returns>
        private bool AddToken(string UserName, string Token, string RefreshToken, string IP)
        {
            var IsTokenExists = this._context.TokenMsts.Where(x => x.UserName == UserName).FirstOrDefault();
            if (IsTokenExists == null)
            {
                TokenMst newToken = new TokenMst();
                newToken.UserName = UserName;
                newToken.Token = Token;
                newToken.RefreshToken = RefreshToken;
                newToken.CreateAt = DateTime.Now;
                newToken.UpdatedAt = DateTime.Now;
                newToken.ExpiredOn = DateTime.Now.AddDays(7);
                _context.TokenMsts.Add(newToken);
                _context.SaveChanges();
            }
            else
            {
                IsTokenExists.Token = Token;
                IsTokenExists.RefreshToken = RefreshToken;
                IsTokenExists.CreateAt = DateTime.Now;
                IsTokenExists.UpdatedAt = DateTime.Now;
                IsTokenExists.ExpiredOn = DateTime.Now.AddDays(7);
                _context.Entry(IsTokenExists).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
            }

            return true;
        }
    }
}
