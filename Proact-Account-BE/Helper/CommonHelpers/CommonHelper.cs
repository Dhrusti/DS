using DataLayer.Entities;
using DTO.ReqDTO;
using Helpers.CommonModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Helpers.CommonHelpers
{
    public class CommonHelper
    {
        public const string DATE_FORMAT = "dd/MM/yyyy";
        private IConfiguration _configuration { get; }
        private IHostingEnvironment _hostingEnvironment { get; }
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly MongoClient _db;
        private readonly IMongoCollection<FirstLevelMst> _firstCollection;
        private readonly IMongoCollection<AllLevelMst> _secondCollection;
        private readonly IMongoCollection<AllLevelMst> _thirdCollection;
        private readonly IMongoCollection<AllLevelMst> _forthCollection;
        private readonly IMongoCollection<AllLevelMst> _fifthCollection;

        public CommonHelper(IConfiguration configuration, IHostingEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _db = new MongoClient(_configuration["DatabaseConnection:ConnectionString"]);
            var MongoDataBase = _db.GetDatabase(_configuration["DatabaseConnection:DatabaseName"]);
            _firstCollection = MongoDataBase.GetCollection<FirstLevelMst>(_configuration["DatabaseConnection:CollectionNameFirst"]);
            _secondCollection = MongoDataBase.GetCollection<AllLevelMst>(_configuration["DatabaseConnection:CollectionNameSecond"]);
            _thirdCollection = MongoDataBase.GetCollection<AllLevelMst>(_configuration["DatabaseConnection:CollectionNameThird"]);
            _forthCollection = MongoDataBase.GetCollection<AllLevelMst>(_configuration["DatabaseConnection:CollectionNameForth"]);
            _fifthCollection = MongoDataBase.GetCollection<AllLevelMst>(_configuration["DatabaseConnection:CollectionNameFifth"]);
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

        public CommonResponse UploadFile(IFormFile file, string subDirectory, string fileName)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                string savePath = string.Empty;
                string CurrentDirectory = Directory.GetCurrentDirectory();
                subDirectory = subDirectory ?? string.Empty;
                var target = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot", "Files", subDirectory);


                Directory.CreateDirectory(target);
                savePath = Path.Combine("Files", subDirectory, fileName);
                var filePath = Path.Combine(target, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                response.Status = true;
                response.Message = "File Uploaded";
                response.Data = savePath;
            }
            catch (Exception ex)
            {
                response.Data = ex;
                response.Message = ex.Message;
            }
            return response;
        }

        public CommonResponse UploadFile(string file, string subDirectory, string fileName)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                string savePath = string.Empty;
                string CurrentDirectory = Directory.GetCurrentDirectory();
                subDirectory = subDirectory ?? string.Empty;
                var target = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot", "Files", subDirectory);

                Directory.CreateDirectory(target);
                savePath = Path.Combine("Files", subDirectory, fileName);
                var filePath = Path.Combine(target, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    //file.CopyTo(stream);
                    byte[] byteArray = Convert.FromBase64String(file.Split(',')[1]);
                    stream.Write(byteArray, 0, byteArray.Length);
                }

                response.StatusCode = HttpStatusCode.OK;
                response.Status = true;
                response.Message = "File Uploaded";
                response.Data = savePath;
            }
            catch (Exception ex)
            {
                response.Data = ex;
                response.Message = ex.Message;
            }
            return response;
        }

        public CommonResponse SendEmail(SendEmailRequest model)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                if (!string.IsNullOrWhiteSpace(model.ToEmail))
                {
                    MailMessage mail = new MailMessage();

                    //get configration from appsettings.json
                    string FromEmail = _configuration.GetSection("SiteEmailConfigration:FromEmail").Value;
                    string Host = _configuration.GetSection("SiteEmailConfigration:Host").Value;
                    int Port = Convert.ToInt32(_configuration.GetSection("SiteEmailConfigration:Port").Value);
                    bool EnableSSL = Convert.ToBoolean(_configuration.GetSection("SiteEmailConfigration:EnableSSL").Value);
                    string Password = _configuration.GetSection("SiteEmailConfigration:MailPassword").Value;
                    bool EmailEnable = Convert.ToBoolean(_configuration.GetSection("SiteEmailConfigration:EmailEnable").Value);
                    if (EmailEnable)
                    {
                        mail.From = new MailAddress(FromEmail, "Walt Capital Management");
                        mail.To.Add(new MailAddress(model.ToEmail));
                        mail.Subject = model.Subject;
                        mail.Body = model.Body;
                        mail.IsBodyHtml = true;
                        //if (model.Attachment != null)
                        //{

                        //    string path = Convert.ToString(model.Attachment);
                        //    Attachment attachment = new Attachment(path);
                        //    mail.Attachments.Add(attachment);
                        //}

                        if (model.Attachment != null)
                        {
                            mail.Attachments.Add(new Attachment(model.Attachment));
                        }

                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = Host;
                        smtp.Port = Port;
                        smtp.EnableSsl = EnableSSL;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential(FromEmail, Password);
                        try
                        {
                            smtp.Send(mail);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }

                    response.Status = true;
                    response.Message = "Success.";
                }
                else
                {
                    response.Message = "Receiver Email Id Not Provided.";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        public async Task<string> EncryptStringAsync(string plainText)
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

        public async Task<string> DecryptStringAsync(string cipherText)
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

        public string GetPhysicalRootPath()
        {
            string directoryPath = "/files";
            var physicalRootPath = _hostingEnvironment.WebRootPath + directoryPath;
            return physicalRootPath;
        }

        public string GetRelativeRootPath()
        {
            string directoryPath = "/files";
            string relativeRootPath = _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host + directoryPath;
            return relativeRootPath;
        }

        public FilePaths GetFilePaths(string ModuleName, string extension, bool isTempFolder, string? fileName = "")
        {
            FilePaths filePaths = new FilePaths();
            var rootPath = _hostingEnvironment.WebRootPath;
            string directoryPath = "/files";
            if (isTempFolder)
            {
                directoryPath += "/temp";
            }
            string fileNameOnly = "_" + Guid.NewGuid() + "." + extension.ToString();
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                fileNameOnly = fileName + "." + extension.ToString();
            }

            directoryPath += "/" + ModuleName + "/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/";
            if (!Directory.Exists(rootPath + directoryPath))
            {
                Directory.CreateDirectory(rootPath + directoryPath);
            }

            var fileNameFull = directoryPath + fileNameOnly;

            filePaths.FilePhysicalPath = rootPath + fileNameFull;
            filePaths.FileRelativePath = _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host + fileNameFull;
            filePaths.DirectoryPhysicalPath = rootPath + directoryPath;
            filePaths.DirectoryRelativePath = _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host + directoryPath;
            return filePaths;
        }

        public string GetMimeType(string fileName)
        {
            string mimeType = "application/unknown";
            string ext = Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
                mimeType = regKey.GetValue("Content Type").ToString();

            mimeType = "data:" + mimeType + ";base64,";
            return mimeType;
        }

        public DateTime GetCurrentDateTime()
        {
            return DateTime.Now;
        }

        public string GetUpperCaseFirstAll(string value)
        {
            return value != null ? Regex.Replace(value, @"(^\w)|(\s\w)", m => m.Value.ToUpper()) : string.Empty;
        }
        public async Task<string> GenerateCode(CodeGenerateReqDTO codeGenerateReqModel)
        {
            string generatedCode = string.Empty;
            if (codeGenerateReqModel.LevelId >= 1 && codeGenerateReqModel.LevelId <= 5)
            {
                string lastCode = await FindLastCode(codeGenerateReqModel);
                generatedCode = lastCode.All(char.IsDigit) == true ? await GenerateNewCode(codeGenerateReqModel.LevelId, lastCode) : "Last code is not valid";
            }
            else
            {
                generatedCode = "Enter valid level";
            }

            return generatedCode;
        }
        public async Task<string> FindLastCode(CodeGenerateReqDTO codeGenerateReqModel)
        {
            string generatedCode = string.Empty, lastCode = string.Empty;
            try
            {

                switch (codeGenerateReqModel.LevelId)
                {
                    case 1:
                        var document = _firstCollection.Find(Builders<FirstLevelMst>.Filter.Empty).SortByDescending(x => x.LevelFirstId).FirstOrDefault();

                        lastCode = document == null ? "000000000000000" : document.Code;

                        break;
                    case 2:
                        var document2 = _secondCollection.Find(Builders<AllLevelMst>.Filter.Empty).SortByDescending(x => x.id).FirstOrDefault();

                        lastCode = document2 == null ? "000000000000000" : document2.Code;
                        break;
                    case 3:
                        var document3 = _thirdCollection.Find(Builders<AllLevelMst>.Filter.Empty).SortByDescending(x => x.id).FirstOrDefault();

                        lastCode = document3 == null ? "000000000000000" : document3.Code;
                        break;
                    case 4:
                        var document4 = _forthCollection.Find(Builders<AllLevelMst>.Filter.Empty).SortByDescending(x => x.id).FirstOrDefault();

                        lastCode = document4 == null ? "000000000000000" : document4.Code;
                        break;
                    case 5:
                        var document5 = _fifthCollection.Find(Builders<AllLevelMst>.Filter.Empty).SortByDescending(x => x.id).FirstOrDefault();

                        lastCode = document5 == null ? "000000000000000" : document5.Code;
                        break;

                    default:
                        break;
                }
            }
            catch { throw; }
            return generatedCode = lastCode;

        }
        private async Task<string> GenerateNewCode(int level, string lastCode)
        {
            string newCode = string.Empty;
            int chunkSize = 3;
            level--;
            string[] chunks = Enumerable.Range(0, lastCode.Length / chunkSize).Select(i => lastCode.Substring(i * chunkSize, chunkSize)).ToArray();
            for (int i = 0; i < chunks.Length; i++)
            {
                newCode += i == level ? (Convert.ToInt32(chunks[i]) + 1).ToString().PadLeft(3, '0') : chunks[i];
            }

            return newCode;

        }
    }
}