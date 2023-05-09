using DataLayer.Entities;
using DTOs.ReqDTOs;
using DTOs.ResDTOs;
using Helper.CommonHelper;
using Helpers.CommonModels;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using nClam;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class FilesBLL
    {
        private readonly IConfiguration _iConfiguration;
        private readonly CommonHelpers _commonHelpers;
        private readonly DBContext _dbContext;

        public FilesBLL(IConfiguration iConfiguration, CommonHelpers commonHelpers, DBContext dbContext)
        {
            _iConfiguration = iConfiguration;
            _commonHelpers = commonHelpers;
            _dbContext = dbContext;
        }

        public async Task<CommonResponse> UploadFileAsync(UploadFileReqDTO request)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                if (request != null && request.File != null)
                {
                    var fileStorageData = await GetDataFrom_SP("Portal_API_Storage");
                    if (fileStorageData != null && fileStorageData.Tables[0].Rows.Count > 0)
                    {
                        UploadFileResDTO uploadFileResDTO = new UploadFileResDTO();
                        uploadFileResDTO.FileDetails = new List<UploadFileResDTO.FileDetail>();
                        string[] supportedFileFormatList = _iConfiguration.GetSection("FileTypes").Value.ToLower().ToString().Split(',');

                        FileInfo fileInfo = null;
                        int copiedFilesCount = 0;
                        string unsupportedFileMessage = string.Empty;
                        string virusFileMessage = string.Empty;
                        bool isClean = false;

                        for (int i = 0; i < request.File.Count; i++)
                        {
                            var item = request.File[i];
                            if (item != null)
                            {
                                fileInfo = new FileInfo(item.FileName);
                                string absolutePath = null;
                                string relativePath = null;
                                if (supportedFileFormatList.Contains(fileInfo.Extension.ToLower()))
                                {
                                    var maxInByte = Convert.ToInt32(_iConfiguration.GetSection("FileSize:MaxInByte").Value);
                                    var minInByte = Convert.ToInt32(_iConfiguration.GetSection("FileSize:MinInByte").Value);
                                    if (item.Length < maxInByte && minInByte < item.Length)
                                    {
                                        var ms = new MemoryStream();
                                        item.OpenReadStream().CopyTo(ms);
                                        byte[] fileBytes = ms.ToArray();

                                        string filePath = GenerateFilePath(request.sRef_number, request.sDoc_type);
                                        string fileName = $"{request.sRef_number}_({request.sDoc_type})_{_commonHelpers.GetCurrentDateTime():yyyyMMdd_HHmmss_fff}{fileInfo.Extension.ToLower()}";

                                        if (_iConfiguration.GetSection("FileSaveInProjectFolder").Value.ToLower() == "true")
                                        {
                                            absolutePath = Path.Combine(_commonHelpers.GetRelativePath(), "wwwroot", filePath);
                                        }
                                        else
                                        {
                                            absolutePath = _iConfiguration.GetSection("FileUploadPath").Value;
                                        }
                                        if (!Directory.Exists(absolutePath))
                                        {
                                            Directory.CreateDirectory(absolutePath);
                                        }
                                        absolutePath = Path.Combine(absolutePath, fileName);
                                        relativePath = Path.Combine(filePath, fileName);

                                        using (var stream = new FileStream(absolutePath, FileMode.Create))
                                        {
                                            //code to scanfile 
                                            try
                                            {

                                                //this._logger.LogInformation("ClamAV scan begin for file {0}", file.FileName);
                                                var clam = new ClamClient(this._iConfiguration["ClamAVServer:URL"], Convert.ToInt32(this._iConfiguration["ClamAVServer:Port"]));

                                                var scanResult = await clam.SendAndScanFileAsync(fileBytes);

                                                switch (scanResult.Result)
                                                {

                                                    case ClamScanResults.Clean:
                                                        isClean = true;
                                                        // this._logger.LogInformation("The file is clean! ScanResult:{1}", scanResult.RawResult);
                                                        break;
                                                    case ClamScanResults.VirusDetected:
                                                        // this._logger.LogError("Virus Found! Virus name: {1}", scanResult.InfectedFiles.FirstOrDefault().VirusName);
                                                        break;
                                                    case ClamScanResults.Error:
                                                        //  this._logger.LogError("An error occured while scaning the file! ScanResult: {1}", scanResult.RawResult);
                                                        break;
                                                    case ClamScanResults.Unknown:
                                                        //   this._logger.LogError("Unknown scan result while scaning the file! ScanResult: {0}", scanResult.RawResult);
                                                        break;
                                                }
                                            }
                                            catch { throw; }

                                            if (isClean && item.Length > 0)
                                            {
                                                item.CopyTo(stream);

                                                var connectionStringData = fileStorageData.Tables[0].Rows[0];

                                                int isSuccess = await SaveData_SP(
                                                    $"server={connectionStringData.ItemArray[2]};user={connectionStringData.ItemArray[3]};database=portalwebapi_db;port=3306;password={connectionStringData.ItemArray[4]}",
                                                    request.sRef_number,
                                                    request.vDoc_description,
                                                    request.sDoc_type,
                                                    fileName,
                                                    request.vStorageID,
                                                    fileInfo.Extension.Remove(0).ToUpper(),
                                                    filePath
                                                    );

                                                if (isSuccess >= 0)
                                                {
                                                    copiedFilesCount++;
                                                    uploadFileResDTO.FileDetails.Add(new UploadFileResDTO.FileDetail { FileName = item.FileName, FilePath = relativePath });
                                                }
                                            }
                                            else
                                            {
                                                virusFileMessage = virusFileMessage == string.Empty ? item.FileName : virusFileMessage + $", {item.FileName}";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        response.Message = $"The file size must not exceed {maxInByte / 1024} KB.";
                                    }
                                }
                                else
                                {
                                    unsupportedFileMessage = unsupportedFileMessage == string.Empty ? item.FileName : unsupportedFileMessage + $", {item.FileName}";
                                }
                            }
                        }

                        if (request.File.Count == copiedFilesCount)
                        {
                            response.Status = true;
                            response.StatusCode = System.Net.HttpStatusCode.OK;
                            response.Message = copiedFilesCount > 1 ? "All files uploaded successfully!" : "File uploaded successfully!";
                            response.Data = uploadFileResDTO;
                        }
                        else if (unsupportedFileMessage != string.Empty || virusFileMessage != string.Empty)
                        {
                            if (unsupportedFileMessage != string.Empty && virusFileMessage != string.Empty)
                            {
                                response.Status = true;
                                response.StatusCode = System.Net.HttpStatusCode.OK;
                                if (copiedFilesCount >= 1)
                                {
                                    response.Status = true;
                                    response.Message = $"{unsupportedFileMessage} : file(s) not supported,{Environment.NewLine}{virusFileMessage} : file(s) contains virus, Other file(s) uploaded successfully!";
                                    response.StatusCode = System.Net.HttpStatusCode.OK;
                                    response.Data = uploadFileResDTO;
                                }
                                else
                                {
                                    response.Message = $"{unsupportedFileMessage} : file(s) not supported,{Environment.NewLine}{virusFileMessage} : file(s) contains virus!";
                                }

                            }
                            else if (unsupportedFileMessage != string.Empty)
                            {
                                if (copiedFilesCount >= 1)
                                {
                                    response.Status = true;
                                    response.StatusCode = System.Net.HttpStatusCode.OK;
                                    response.Message = $"{unsupportedFileMessage} : file(s) not supported, Other file(s) uploaded successfully!";
                                    response.Data = uploadFileResDTO;
                                }
                                else
                                {
                                    response.Message = $"{unsupportedFileMessage} : file(s) not supported!";
                                }
                            }
                            else if (virusFileMessage != string.Empty)
                            {
                                if (copiedFilesCount >= 1)
                                {
                                    response.Status = true;
                                    response.StatusCode = System.Net.HttpStatusCode.OK;
                                    response.Message = $"{virusFileMessage} :  file(s) contains virus, Other file(s) uploaded successfully!";
                                    response.Data = uploadFileResDTO;
                                }
                                else
                                {
                                    response.Message = $"{virusFileMessage} :  file(s) contains virus!";
                                }
                            }
                        }
                    }
                    else
                    {
                        response.StatusCode = System.Net.HttpStatusCode.NotFound;
                        response.Message = "File storage location data not found!";
                    }
                }
                else
                {
                    response.Message = CommonConstants.Please_Select_Files;
                }
            }
            catch { throw; }

            return response;
        }

        public async Task<CommonResponse> DownloadFileAsync(string vRef_number)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                var fileStorageData = await GetDataFrom_SP("Portal_API_Storage");
                if (fileStorageData != null && fileStorageData.Tables[0].Rows.Count > 0)
                {
                    Dictionary<string, string> parameters = new Dictionary<string, string>
                    {{ "vRef_number", vRef_number }};

                    var connectionStringData = fileStorageData.Tables[0].Rows[0];

                    string connectionString = $"server={connectionStringData.ItemArray[2]};user={connectionStringData.ItemArray[3]};database=portalwebapi_db;port=3306;password={connectionStringData.ItemArray[4]}";

                    var dataSet = await GetDataFrom_SP("Portal_API_Document_Load", connectionString, parameters);

                    if (dataSet != null)
                    {
                        List<docimg_document> data = new List<docimg_document>();
                        data = ConvertDataTable<docimg_document>(dataSet.Tables[0]);

                        if (data != null && data.Count > 0)
                        {
                            response.Status = true;
                            response.StatusCode = System.Net.HttpStatusCode.OK;
                            response.Message = CommonConstants.Data_Found_Successfully;
                            response.Data = data;
                        }
                        else
                        {
                            response.StatusCode = System.Net.HttpStatusCode.NotFound;
                            response.Message = CommonConstants.Data_Not_Found;
                        }
                    }
                    else
                    {
                        response.StatusCode = System.Net.HttpStatusCode.NotFound;
                        response.Message = CommonConstants.Data_Not_Found;
                    }
                }
                else
                {
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    response.Message = "File storage location data not found!";
                }
            }
            catch { throw; }

            return response;
        }


        #region Private functions

        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName] != DBNull.Value ? dr[column.ColumnName] : null, null);
                    else
                        continue;
                }
            }
            return obj;
        }

        private async Task<DataSet> GetDataFrom_SP(string storedProcedure, string? connectionString = null, Dictionary<string, string>? parameters = null)
        {
            MySqlConnection con = new MySqlConnection();

            con.ConnectionString = connectionString != null ? connectionString : _iConfiguration.GetConnectionString("DBConnection");

            MySqlCommand cmd = new MySqlCommand();

            if (parameters != null)
                foreach (KeyValuePair<string, string> item in parameters)
                {
                    cmd.Parameters.AddWithValue("@" + item.Key, item.Value);
                    cmd.Parameters["@" + item.Key].Direction = ParameterDirection.Input;
                }

            cmd.Connection = con;
            cmd.CommandText = storedProcedure;
            cmd.CommandType = CommandType.StoredProcedure;

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            await con.OpenAsync();
            await adapter.FillAsync(ds);
            await con.CloseAsync();

            return ds;
        }

        private string GenerateFilePath(string sRef_number, string sDoc_type)
        {
            DateTime dateTime = _commonHelpers.GetCurrentDateTime();
            return $"{dateTime.Year}\\{dateTime:MM}\\{dateTime:dd}\\{sRef_number}\\{sDoc_type}\\{sRef_number}_{dateTime:ddMMyyyyHHmmss_fff}";
        }

        private async Task<int> SaveData_SP(string connectionString, string vRef_number, string vDoc_description, string vDoc_type, string vFilename, int? vStorageID, string vFile_ext, string vInternal_filename)
        {
            try
            {
                MySqlConnection con = new MySqlConnection(connectionString);
                MySqlCommand cmd = new MySqlCommand();

                cmd.CommandText = "Portal_API_Document_Insert";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@vRef_number", vRef_number);
                cmd.Parameters["@vRef_number"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("@vDoc_description", vDoc_description);
                cmd.Parameters["@vDoc_description"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("@vDoc_type", vDoc_type);
                cmd.Parameters["@vDoc_type"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("@vFilename", vFilename);
                cmd.Parameters["@vFilename"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("@vStorageID", vStorageID);
                cmd.Parameters["@vStorageID"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("@vFile_ext", vFile_ext);
                cmd.Parameters["@vFile_ext"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("@vInternal_filename", vInternal_filename);
                cmd.Parameters["@vInternal_filename"].Direction = ParameterDirection.Input;

                await con.OpenAsync();
                cmd.Connection = con;
                var isSuccess = await cmd.ExecuteNonQueryAsync();
                await con.CloseAsync();

                return isSuccess;
            }
            catch { throw; }
        }

        private async Task<dynamic> GetTable_Data(string vRef_number)
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = "server=localhost;user=root;database=portalwebapi_db;port=3306;password=sa@123";


            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = conn;
            cmd.CommandText = "Portal_API_Document_Load";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@vRef_number", vRef_number);
            cmd.Parameters["@vRef_number"].Direction = ParameterDirection.Input;

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            await conn.OpenAsync();
            await adapter.FillAsync(ds);
            await conn.CloseAsync();

            return ds;
        }


        #endregion
    }
}
