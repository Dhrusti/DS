using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Helper.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Helper
{
	public class CommonHelper
	{
		public const string DATE_FORMAT = "dd/MM/yyyy";
		private IConfiguration _configuration { get; }
		private IHostingEnvironment _hostingEnvironment { get; }
		private readonly IHttpContextAccessor _httpContextAccessor;

		public CommonHelper(IConfiguration configuration, IHostingEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor)
		{
			_configuration = configuration;
			_hostingEnvironment = hostingEnvironment;
			_httpContextAccessor = httpContextAccessor;
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

		public string GetPhysicalRootPath()
		{
			string directoryPath = "\\Files";
			var physicalRootPath = _hostingEnvironment.WebRootPath + directoryPath;
			return physicalRootPath;
		}

		public string GetRelativeRootPath()
		{
			string directoryPath = "\\Files";
			string relativeRootPath = _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host + directoryPath;
			return relativeRootPath;
		}

		public DateTime GetCurrentDateTime()
		{
			return DateTime.Now;
		}

		public async Task AddActivityLog(string apiUrl, string methodType, string request, string requestResult)
		{
			try
			{
				bool APILogSwitch = Convert.ToBoolean(_configuration["CommonSwitches:APILogSwitch"].ToString());
				if (APILogSwitch)
				{
					string logText = apiUrl + " (" + methodType + ") - Request : ( " + requestResult + " ) - Response : ( " + request + " ).";
					AddLog(logText, CommonConstant.Activity_log);
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
					AddLog(exceptionText, CommonConstant.Exception_log);
				}
			}
			catch (Exception) { throw; }
		}

	}
}
