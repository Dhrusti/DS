using DataLayer.Entities;
using Helper.CommonModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Helper.CommonHelpers
{
    public class CommonHelper
    {
        public const string DATE_FORMAT = "dd/MM/yyyy";
        private IConfiguration _configuration { get; }
        private IHostingEnvironment _hostingEnvironment { get; }
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DBContext _dbContext;
        //private readonly IHubContext<SignalRHub> _hubContext;
        private readonly CommonRepo _commonRepo;

        // ConcurrentDictionary is thread-safe
        public static ConcurrentDictionary<int, string> ConnectedIds = new ConcurrentDictionary<int, string>();

        public CommonHelper(IConfiguration configuration, IHostingEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor, DBContext dBContext, CommonRepo commonRepo)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dBContext;
            //_hubContext = hubContext;
            _commonRepo = commonRepo;
        }
        #region DateTime

        public DateTime GetCurrentDateTime()
        {
            return DateTime.UtcNow;
        }

        public string GetDateInString(DateTime date)
        {
            return date.ToString(DATE_FORMAT).Replace("-", "/");
        }

        public DateTime GetDateFromString(string date)
        {
            return DateTime.ParseExact(date, DATE_FORMAT, CultureInfo.InvariantCulture);
        }

        public string GetAge(DateTime dateTime)
        {
            DateTime birthDate = Convert.ToDateTime(dateTime);
            int age = (int)Math.Floor((GetCurrentDateTime() - birthDate).TotalDays / 365.25D);
            return age.ToString();
        }

        public DateTime GetConvertedDateTimeByTimeZone(DateTime dateTime, string? timeZone = null)
        {
            /*timeZone ??= GetLoggedInUsersTimeZoneAsync();
            return timeZone == CommonEnums.TimeZone.China.ToString() ? dateTime.AddHours(8) : dateTime;*/

            /// Working Code for Daylight Saving and Multiple TimeZones.
            timeZone ??= GetLoggedInUsersTimeZoneAsync();
            switch (timeZone)
            {
                case CommonConstant.UK:
                    timeZone = CommonConstant.GMT_STANDARD_TIME;
                    break;
                case CommonConstant.CHINA:
                    timeZone = CommonConstant.CHINA_STANDARD_TIME;
                    break;
                case CommonConstant.INDIA:
                    timeZone = CommonConstant.INDIA_STANDARD_TIME;
                    break;
                case CommonConstant.UTC:
                    timeZone = CommonConstant.UTC;
                    break;
                default:
                    timeZone = CommonConstant.GMT_STANDARD_TIME;
                    break;
            }
            TimeZoneInfo timeZoneInfo;
            try
            {
                timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
            }
            catch (Exception ex)
            {
                AddExceptionLog(ex, logInDB: true);
                return dateTime;
            }

            return TimeZoneInfo.ConvertTimeFromUtc(dateTime, timeZoneInfo);
        }

        public DateTime GetUTCDateTimeByTimeZone(DateTime dateTime, string? timeZone = null)
        {
            //timeZone ??= GetLoggedInUsersTimeZoneAsync();
            //return timeZone == CommonEnums.TimeZone.China.ToString() ? dateTime.AddHours(-8) : dateTime;

            // Working Code for Daylight Saving and Multiple TimeZones.
            timeZone ??= GetLoggedInUsersTimeZoneAsync();

            switch (timeZone)
            {
                case CommonConstant.UK:
                    timeZone = CommonConstant.GMT_STANDARD_TIME;
                    break;
                case CommonConstant.CHINA:
                    timeZone = CommonConstant.CHINA_STANDARD_TIME;
                    break;
                case CommonConstant.INDIA:
                    timeZone = CommonConstant.INDIA_STANDARD_TIME;
                    break;
                case CommonConstant.UTC:
                    timeZone = CommonConstant.UTC;
                    break;
                default:
                    timeZone = CommonConstant.GMT_STANDARD_TIME;
                    break;
            }

            TimeZoneInfo timeZoneInfo;
            try
            {
                timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
            }
            catch (Exception ex)
            {
                AddExceptionLog(ex, logInDB: true);
                return dateTime;
            }
            // Convert the time to UTC
            DateTime localTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Unspecified);
            return TimeZoneInfo.ConvertTimeToUtc(localTime, timeZoneInfo);
        }

        public List<DateTime> GetDateOfEachDayInCurrentYear(string time, string day)
        {
            // Parse the input time
            if (!DateTime.TryParseExact(time, "HH:mm", null, System.Globalization.DateTimeStyles.None, out var parsedTime))
            {
                throw new ArgumentException("Invalid time format. Please provide time in HH:mm format.");
            }

            // Parse the input day
            if (!Enum.TryParse<DayOfWeek>(day, true, out var parsedDay))
            {
                throw new ArgumentException("Invalid day. Please provide a valid day of the week (e.g., Monday).");
            }

            var result = new List<DateTime>();
            var currentDate = GetCurrentDateTime(); // Start from the current date
            int currentYear = currentDate.Year;

            // Loop through the entire year
            while (currentDate.Year == currentYear)
            {
                if (currentDate.DayOfWeek == parsedDay)
                {
                    // Combine the parsed time with the current date
                    var fullDateTime = currentDate.Date.Add(parsedTime.TimeOfDay);
                    result.Add(fullDateTime);
                }

                // Move to the next day
                currentDate = currentDate.AddDays(1);
            }

            return result;
        }

        public List<DateTime> GetDatesByDayOfWeek(DateTime startDateTime, DateTime endDateTime, string day)
        {
            List<DateTime> dates = new List<DateTime>();
            DayOfWeek dayOfWeek = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), day);

            for (DateTime date = startDateTime; date <= endDateTime; date = date.AddDays(1))
            {
                if (date.DayOfWeek == dayOfWeek)
                {
                    dates.Add(date);
                }
            }

            return dates;
        }

        public DateTime GetWeekStartDate(int year, int week)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int day = (int)jan1.DayOfWeek - 1;
            int delta = (day < 4 ? -day : 7 - day) + 7 * (week - 1);
            return jan1.AddDays(delta);
        }

        public DateTime GetWeekEndDate(int year, int week)
        {
            return GetWeekStartDate(year, week).AddDays(6);
        }
        #endregion

        #region Authentication
        public int GetLoggedInUserIdAsync(bool? calledFromSignalRHub = false)
        {
            ClaimsPrincipal claimsPrincipal = GetLoggedInUserDataAsync(calledFromSignalRHub);

            var UserId = claimsPrincipal.Claims.Where(x => x.Type == ClaimTypes.Name).Select(x => x.Value).FirstOrDefault() ?? "0";

            return Convert.ToInt32(UserId);
        }

        public string GetLoggedInUsersRoleAsync(bool? calledFromSignalRHub = false)
        {
            ClaimsPrincipal claimsPrincipal = GetLoggedInUserDataAsync(calledFromSignalRHub);

            var roleName = claimsPrincipal.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).FirstOrDefault();

            return Convert.ToString(roleName);
        }

        public string GetLoggedInUsersTimeZoneAsync(bool? calledFromSignalRHub = false)
        {
            ClaimsPrincipal claimsPrincipal = GetLoggedInUserDataAsync(calledFromSignalRHub);

            var timeZone = claimsPrincipal.Claims.Where(x => x.Type == ClaimTypes.Country).Select(x => x.Value).FirstOrDefault();

            return Convert.ToString(timeZone);
        }

        public ClaimsPrincipal GetLoggedInUserDataAsync(bool? calledFromSignalRHub = false)
        {
            string accessToken = string.Empty;
            if (calledFromSignalRHub == true)
            {
                accessToken = _httpContextAccessor.HttpContext.Request.Query["access_token"].ToString();
            }
            else
            {
                accessToken = Convert.ToString(_httpContextAccessor.HttpContext.Request.Headers["Authorization"]) ?? "";
            }

            if (string.IsNullOrEmpty(accessToken)) { return new ClaimsPrincipal(); }
            accessToken = accessToken.Replace("Bearer ", "").Trim();

            SecurityToken validatedToken;
            return GetUserIdFromToken(accessToken, out validatedToken);
        }

        private ClaimsPrincipal GetUserIdFromToken(string token, out SecurityToken validatedToken)
        {
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal();
            var tokenHandler = new JwtSecurityTokenHandler();
            var secreatekey = Convert.ToString(_configuration["JsonWebTokenKeys:IssuerSigningKey"]);
            var ValidIssuer = Convert.ToString(_configuration["JsonWebTokenKeys:ValidIssuer"]);
            var ValidAudience = Convert.ToString(_configuration["JsonWebTokenKeys:ValidAudience"]);
            var RefreshTokenExpiryDays = Convert.ToInt32(Convert.ToString(_configuration["JsonWebTokenKeys:RefreshTokenexpiryDays"]));

            claimsPrincipal = tokenHandler.ValidateToken(token,
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = ValidIssuer,
                ValidateAudience = true,
                ValidAudience = ValidAudience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secreatekey)),
                ClockSkew = TimeSpan.FromDays(RefreshTokenExpiryDays),
                ValidateLifetime = false
            }, out validatedToken);

            return claimsPrincipal;
        }

        #endregion

        #region Logs

        public void AddLog(string logType, string text)
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

        public void AddLogAsync(string logType, string text)
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
                    writer.WriteLineAsync(text).ConfigureAwait(false);
                    //writer.Close();
                }
            }
        }

        public bool AddLogInDB(string logType, string? apiUrl = null, string? methodType = null, string? request = null, string? requestResult = null, Exception? exception = null)
        {
            if (logType == CommonConstant.ActivityLog)
            {
                ActivityLogMst activityLogMst = new ActivityLogMst()
                {
                    ExecutionDate = GetCurrentDateTime(),
                    Apiurl = apiUrl,
                    MethodType = methodType,
                    Request = request,
                    Response = requestResult
                };

                _dbContext.ActivityLogMsts.Add(activityLogMst);
                _dbContext.SaveChanges();

                return true;
            }
            else if (logType == CommonConstant.ExceptionLog)
            {
                ExceptionLogMst exceptionLogMst = new ExceptionLogMst()
                {
                    ExecutionDate = GetCurrentDateTime(),
                    Apiurl = apiUrl,
                    MethodType = methodType,
                    Message = exception.Message.ToString(),
                    StackTrace = Convert.ToString(exception.StackTrace)
                };

                _dbContext.ExceptionLogMsts.Add(exceptionLogMst);
                _dbContext.SaveChanges();

                return true;
            }
            return false;
        }

        public string ReadJsonFile(string FilePath)
        {
            string FileText = File.ReadAllText(FilePath);
            return FileText;
        }

        public bool AddJsonData(dynamic model, string FilePath)
        {
            var jsonString = JsonConvert.SerializeObject(model, Formatting.Indented);
            File.WriteAllText(FilePath, jsonString);
            return true;
        }

        public void AddActivityLog(string apiUrl, string methodType, string request, string requestResult)
        {
            try
            {
                bool APILogSwitch = Convert.ToBoolean(_configuration["CommonSwitches:APILogSwitch"]);
                if (APILogSwitch)
                {
                    bool LogsInDB = Convert.ToBoolean(_configuration["CommonSwitches:LogsInDB"]);
                    if (LogsInDB)
                    {
                        AddLogInDB(CommonConstant.ActivityLog, apiUrl, methodType, request, requestResult);
                    }
                    else
                    {
                        string logText = apiUrl + " (" + methodType + ") - Request : ( " + request + " ) - Response : ( " + requestResult + " ).";
                        AddLog(CommonConstant.ActivityLog, logText);
                    }
                }
            }
            catch { throw; }
        }

        public void AddExceptionLog(Exception exception, bool? logInDB = true, string? apiUrl = null, string? methodType = null)
        {
            try
            {
                bool ExceptionLogSwitch = Convert.ToBoolean(_configuration["CommonSwitches:ExceptionLogSwitch"]);
                if (ExceptionLogSwitch)
                {
                    bool LogsInDB = Convert.ToBoolean(_configuration["CommonSwitches:LogsInDB"]);
                    if (LogsInDB && Convert.ToBoolean(logInDB))
                    {
                        AddLogInDB(CommonConstant.ExceptionLog, apiUrl: apiUrl, methodType: methodType, exception: exception);
                    }
                    else
                    {
                        AddLog(CommonConstant.ExceptionLog, exception.ToString());
                    }
                }
            }
            catch { throw; }
        }

        #endregion

        #region Crytography

        public async Task<string> EncryptString(string plainText)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["EncryptionKeys:EncryptionSecurityKey"].ToString());
            var iv = Encoding.UTF8.GetBytes(_configuration["EncryptionKeys:EncryptionSecurityIV"].ToString());
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
            {
                throw new ArgumentNullException("plainText");
            }
            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            byte[] encrypted;
            // Create a RijndaelManaged object
            // with the specified key and IV.
            using (var rijAlg = new RijndaelManaged())
            {
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;

                rijAlg.Key = key;
                rijAlg.IV = iv;

                // Create a decrytor to perform the stream transform.
                var encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for encryption.
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            // Return the encrypted bytes from the memory stream.

            return Convert.ToBase64String(encrypted);
            //return encrypted;
        }

        public async Task<string> DecryptString(string cipherText)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["EncryptionKeys:EncryptionSecurityKey"].ToString());
            var iv = Encoding.UTF8.GetBytes(_configuration["EncryptionKeys:EncryptionSecurityIV"].ToString());
            var encrypted = Convert.FromBase64String(cipherText);
            // Check arguments.
            if (encrypted == null || encrypted.Length <= 0)
            {
                throw new ArgumentNullException("cipherText");
            }
            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (var rijAlg = new RijndaelManaged())
            {
                //Settings
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;
                rijAlg.Key = key;
                rijAlg.IV = iv;

                // Create a decrytor to perform the stream transform.
                var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                try
                {
                    // Create the streams used for decryption.
                    using (var msDecrypt = new MemoryStream(encrypted))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                // Read the decrypted bytes from the decrypting stream
                                // and place them in a string.
                                plaintext = srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
                catch
                {
                    plaintext = "keyError";
                }
            }
            return plaintext;
        }

        #endregion
    }
}
