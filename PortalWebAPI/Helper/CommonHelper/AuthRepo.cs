using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Helper.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Helper.CommonHelper
{
	public class AuthRepo
	{
		IConfiguration _iConfiguration;
		private CommonHelpers _commonHelpers;

		public AuthRepo(IConfiguration iConfiguration, CommonHelpers commonHelpers)
		{
			_iConfiguration = iConfiguration;
			_commonHelpers = commonHelpers;
		}

		public TokenModel CreateJWT(int UserName, bool IsRefreshTokenExpired)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var secreatekey = _iConfiguration["JsonWebTokenKeys:IssuerSigningKey"].ToString();
			var ValidIssuer = _iConfiguration["JsonWebTokenKeys:ValidIssuer"].ToString();
			//var ValidAudience = _iConfiguration["JsonWebTokenKeys:ValidAudience"].ToString();
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secreatekey));
			var claims = new Claim[] {
			 new Claim(ClaimTypes.NameIdentifier,UserName.ToString())
		           //,
		           //new Claim(ClaimTypes.NameIdentifier,ECode.ToString())
		          };
			var signingcredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			int TokenExpiryMin = Convert.ToInt32(_iConfiguration["JsonWebTokenKeys:TokenExpiryMin"]);

			var newJwtToken = new JwtSecurityToken(
				  issuer: ValidIssuer,
				  //audience: ValidAudience,
				  notBefore: _commonHelpers.GetCurrentDateTime(),
				  expires: _commonHelpers.GetCurrentDateTime().AddMinutes(TokenExpiryMin),
				  signingCredentials: signingcredentials,
				  claims: claims
			);

			var writetoken = tokenHandler.WriteToken(newJwtToken);
			var RefreshToken = CreateRefreshToken();

			TokenModel model = new TokenModel();

			model.Token = writetoken;
			model.RefreshToken = RefreshToken;


			//var status = updatetoken(UserName, writetoken, RefreshToken, IsRefreshTokenExpired);
			//if (status != null)
			//{
			//	model.Token = status.Token;                     //writetoken;
			//	model.RefreshToken = status.RefreshToken;       // RefreshToken;
			//}
			return model;
		}

		public string CreateRefreshToken()
		{
			var randomNumber = new byte[32];
			using (var rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(randomNumber);
				return Convert.ToBase64String(randomNumber);
			}
		}

		//private UserTokenMst updatetoken(int UserName, string token, string RefreshToken, bool IsRefreshTokenExpired)
		//{
		//	UserTokenMst userTokenMst = new UserTokenMst();
		//	try
		//	{
		//		UserTokenMst tokenmst = new UserTokenMst();
		//		var UserToken = _dbContext.UserTokenMsts.FirstOrDefault(x => x.UserId == Convert.ToInt32(UserName));
		//		var refreshTokenExpiryDays = Convert.ToInt32(_iConfiguration["JsonWebTokenKeys:RefreshTokenexpiryDays"].ToString());

		//		if (UserToken != null)
		//		{
		//			tokenmst = UserToken;
		//			if (IsRefreshTokenExpired)
		//			{
		//				tokenmst.RefreshToken = RefreshToken;
		//				tokenmst.UpdatedDate = _commonHelpers.GetCurrentDateTime();
		//				tokenmst.ExpiredOn = _commonHelpers.GetCurrentDateTime().AddDays(refreshTokenExpiryDays);
		//			}
		//			else
		//			{
		//				//tokenmst.Token = token;
		//				tokenmst.UpdatedDate = _commonHelpers.GetCurrentDateTime();
		//			}
		//		}
		//		else
		//		{
		//			tokenmst.UserId = Convert.ToInt32(UserName);
		//			tokenmst.RefreshToken = RefreshToken;
		//			tokenmst.CreatedDate = _commonHelpers.GetCurrentDateTime();
		//			tokenmst.UpdatedDate = _commonHelpers.GetCurrentDateTime();
		//			tokenmst.ExpiredOn = _commonHelpers.GetCurrentDateTime().AddDays(refreshTokenExpiryDays);
		//		}
		//		tokenmst.Token = token;

		//		if (UserToken != null)
		//		{
		//			_dbContext.Entry(tokenmst).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
		//			_dbContext.SaveChanges();
		//		}
		//		else
		//		{
		//			_dbContext.UserTokenMsts.Add(tokenmst);
		//			_dbContext.SaveChanges();
		//		}
		//		userTokenMst = tokenmst;
		//	}
		//	catch (Exception ex)
		//	{

		//	}
		//	return userTokenMst;
		//}
	}
}
