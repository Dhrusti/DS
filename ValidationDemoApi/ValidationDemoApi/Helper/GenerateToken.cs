using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System.Collections.Specialized;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using ValidationDemoApi.Entities;
using ValidationDemoApi.Models;



namespace ValidationDemoApi.Helper
{
    public class GenerateToken
    {
        private readonly IConfiguration _configuration;
        private readonly JWTokenDBContext _db;
        private readonly IMapper _mapper;
        public static string SessionIdToken = "session-id";
        UserToken token1 = new UserToken();
        public static RefTokenUsers user1 = new RefTokenUsers();

        public GenerateToken(JWTokenDBContext db, IMapper mapper, IConfiguration configuration)
        {
            _configuration = configuration;
            _db = db;
            _mapper = mapper;
        }

        //Create User 
        public UserModel Registration(UserModel user)
        {

            TblUserTokenMst tblUserTokenMst = new TblUserTokenMst();
            CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var usert = _db.TblUserTokenMsts.Where(x => x.Username == user.UserName).FirstOrDefault();
            if (usert == null)
            {
                user1.Username = user.UserName;
                user1.PasswordHash = passwordHash;
                user1.PasswordSalt = passwordSalt;

                tblUserTokenMst.Username = user.UserName;
                tblUserTokenMst.Password = user.Password;
                tblUserTokenMst.Token = "";
                tblUserTokenMst.RefreshToken = "";
                tblUserTokenMst.TokenCreated = user1.TokenCreated;
                tblUserTokenMst.TokenExpires = user1.TokenExpires;

                _db.TblUserTokenMsts.Add(tblUserTokenMst);
                _db.SaveChanges();

            }
            return user;
        }

        //This method of call from registration
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        //Login User
        public Response Login(UserModel user)
        {
            Response response = new Response();
            try
            {
                UserToken usert = new UserToken();
                TblUserTokenMst tblUserTokenMst = new TblUserTokenMst();
                var res = this._db.TblUserTokenMsts.Where(x => x.Username == user.UserName).FirstOrDefault();

                if (res != null)
                {
                    string token = CreateToken(user1);
                    var refreshToken = GenerateRefreshToken();
                    SetRefreshToken(refreshToken);

                    res.Token = token;
                    res.RefreshToken = refreshToken.Token.ToString();
                    res.TokenCreated = refreshToken.Created;
                    res.TokenExpires = refreshToken.Expires;
                    // res.Id = user.Id;
                    res.Username = user.UserName;
                    res.Password = user.Password;

                    this._db.Entry(res).State = EntityState.Modified;
                    _db.SaveChanges();

                    response.Data = res;
                    response.Message = "Success";
                    response.Status = true;
                    response.Code = 200;
                }
                else
                {
                    response.Message = "User Not Found";
                    response.Status = false;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Data = ex;
                response.Status = false;
            }
            return response;
        }

        //This method create token in as per login user
        private string CreateToken(RefTokenUsers user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, "Admin")
            };
            //----configuration to not get key then use this logic---
            // string token1 = "my top secret key";
            // var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(token1));

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
            _configuration.GetSection("AppSettings:KeyToken").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        //This method  GenerateRefreshToken token in as per login user
        private RefToken GenerateRefreshToken()
        {
            var refreshToken = new RefToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };
            return refreshToken;
        }

        //This method  SetRefreshToken token in as per login user        
        private void SetRefreshToken(RefToken newRefreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };
            //---Append cookies to get refreshToken----
            //HttpResponse httpResponse = new HttpResponse();
            // Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            user1.RefreshToken = newRefreshToken.Token;
            user1.TokenCreated = newRefreshToken.Created;
            user1.TokenExpires = newRefreshToken.Expires;
        }


        //This method is RefreshTokenUser Generate
        public Response RefreshTokenUser(RefrenceTokenModel refuser)
        {
            Response response = new Response();
            try
            {
                var res = this._db.TblUserTokenMsts.Where(x => x.RefreshToken == refuser.Refrencetoken && x.Token == refuser.Token).FirstOrDefault();

                if (res.RefreshToken != refuser.Refrencetoken)
                {
                    response.Message = "Invalid Refresh Token.";
                }
                else if (res.TokenExpires <= DateTime.Now)
                {
                    var newRefreshToken = GenerateRefreshToken();
                    SetRefreshToken(newRefreshToken);
                    res.RefreshToken = newRefreshToken.Token.ToString();
                    res.TokenExpires = newRefreshToken.Expires;
                }

                string token = CreateToken(user1);
                res.Token = token;

                _db.Entry(res).State = EntityState.Modified;
                _db.SaveChanges();

                refuser.Refrencetoken = res.RefreshToken;
                refuser.Token = res.Token;

                response.Data = refuser;
                response.Message = "Success";
                response.Status = true;
                response.Code = 200;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Data = ex;
                response.Code = 401;
            }
            return response;
        }



       // [HttpPost]
        //public HttpResponseMessage RefreshToken()
        //{


        //    HttpResponseMessage respMessage = new HttpResponseMessage();
        //    //respMessage.Content = new ObjectContent<string[]>(new string[] { "value1", "value2" }, new JsonMediaTypeFormatter());
        //    var se = new NameValueCollection();
        //    se["sessid"] = "123";
        //    se["3dstyle"] = "flat";
        //    se["theme"] = "Blue";
        //    var cookie = new CookieHeaderValue("session", se);
        //    cookie.Expires = DateTimeOffset.Now.AddDays(2);
        //    //cookie.Domain = RequestUri.host; //""//Request.RequestUri.Host;
        //    cookie.Path = "/";
        //    respMessage.Headers.AddCookies(new CookieHeaderValue[] { cookie });
        //    return respMessage;
        //}
    }
}
