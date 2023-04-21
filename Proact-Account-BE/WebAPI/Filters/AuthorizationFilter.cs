using Helpers.CommonHelpers;
using Helpers.CommonModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Text;

namespace WebAPI.Filters
{
    public class AuthorizationFilter : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly IConfiguration _configuration;
        private readonly CommonHelper _commonHelper;
        public AuthorizationFilter(IConfiguration configuration, CommonHelper commonHelper)
        {
            _configuration = configuration;
            _commonHelper = commonHelper;
        }

        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                bool isAllEncrypted = Convert.ToBoolean(_configuration["CommonSwitches:AllApiEncryptionSwitch"].ToString());
                var request = context.HttpContext.Request;
                var data = context.RouteData.Values["action"] as string;    // To get method name of current request.
                if (isAllEncrypted)
                {
                    if (!string.IsNullOrEmpty(data) && data != "GetEncryption" && data != "GetDecryption")
                    {
                        using (var reader = new StreamReader(request.Body))
                        {
                            var json = reader.ReadToEndAsync();
                            if (!string.IsNullOrEmpty(json.Result))
                            {
                                //1.get the value in perticular model
                                CommonResponse commonResponse = JsonConvert.DeserializeObject<CommonResponse>(json.Result);

                                //2.modify the value
                                //var decriptedFromJavascript = new AuthRepo(_configuration).DecryptString(commonResponse.Data);
                                var decriptedFromJavascript = await _commonHelper.DecryptStringAsync(commonResponse.Data);

                                //var decriptedFromJavascript = "{\"id\":0,\"name\":\"string\",\"data\":\"string\"}";
                                byte[] bytes = Encoding.ASCII.GetBytes(decriptedFromJavascript);
                                //3. add the value and update request
                                request.Body = new MemoryStream(bytes);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                // new CommonHelper(_configuration,IHostingEnvironment ).AddLog("Exception :: " + ex.ToString());
            }
        }
    }
}
