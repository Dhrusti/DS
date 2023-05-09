using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Helper.CommonHelper
{
    public class CommonHelpers
    {
        private IConfiguration _configuration { get; }
        private IHostingEnvironment _hostingEnvironment { get; }
        private readonly IHttpContextAccessor _iHttpContextAccessor;

        public CommonHelpers(IHttpContextAccessor iHttpContextAccessor, IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _iHttpContextAccessor = iHttpContextAccessor;
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
        }

        public string GetIPAddress()
        {
            return Convert.ToString(_iHttpContextAccessor.HttpContext.Connection.RemoteIpAddress);
        }

        public string GetRelativePath()
        {
            return Convert.ToString(_hostingEnvironment.ContentRootPath);
        }

        public DateTime GetCurrentDateTime()
        {
            return DateTime.UtcNow;
        }

        public async Task AddActivityLog(string apiUrl, string methodType, string request, string requestResult)
        {
            try
            {
                bool APILogSwitch = Convert.ToBoolean(_configuration["CommonSwitches:APILogSwitch"].ToString());
                if (APILogSwitch)
                {
                    string logText = apiUrl + " (" + methodType + ") - Request : ( " + requestResult + " ) - Response : ( " + request + " ).";
                    AddLog(logText, CommonConstants.ActivityLog);
                }
            }
            catch (Exception) { throw; }
        }

        public void AddExceptionLog(string exceptionText)
        {
            try
            {
                bool ExceptionLogSwitch = Convert.ToBoolean(_configuration["CommonSwitches:ExceptionLogSwitch"].ToString());
                if (ExceptionLogSwitch)
                {
                    AddLog(exceptionText, CommonConstants.ExceptionLog);
                }
            }
            catch { throw; }
        }

        public void AddLog(string text, string? logType = null)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                logType = logType != null ? logType : "";
                string logFileName = GetCurrentDateTime().ToString("dd/MM/yyyy").Replace('/', '_').ToString() + ".log";
                var filePath = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot", "Logs", logType);
                var exists = Directory.Exists(filePath);
                if (!exists)
                {
                    Directory.CreateDirectory(filePath);
                }
                filePath = Path.Combine(filePath, logFileName);
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    text = GetCurrentDateTime().ToString() + " : " + text + "\n";
                    //writer.WriteLine(string.Format(text, GetCurrentDateTime().ToString("dd/MM/yyyy hh:mm:ss tt")));
                    writer.WriteLine(text);
                    writer.Close();
                }
            }
        }
    }
}
