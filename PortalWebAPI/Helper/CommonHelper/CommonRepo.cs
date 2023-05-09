using DataLayer.Entities;
using Helpers.CommonModels;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using System.Threading.Tasks;

namespace Helper.CommonHelper
{
    public class CommonRepo
    {
        private readonly IConfiguration _iConfiguration;
        private readonly CommonHelpers _commonHelpers;
        private readonly DBContext _dbContext;

        public CommonRepo(IConfiguration iConfiguration, CommonHelpers commonHelpers, DBContext dbContext)
        {
            _iConfiguration = iConfiguration;
            _commonHelpers = commonHelpers;
            _dbContext = dbContext;
        }

        public async Task<int> AddLogAsync(string connectionString, string logType, string vDoc_description, string vDoc_type)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                MySqlConnection con = new MySqlConnection(connectionString);
                MySqlCommand cmd = new MySqlCommand();

                cmd.CommandText = logType == CommonConstants.ActivityLog ? "Portal_EventLogging" : logType == CommonConstants.ExceptionLog ? "Portal_ErrorLogging" : "N/A";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@vEventLocation", "API");
                cmd.Parameters["@vEventLocation"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("@vEventDescription", vDoc_description);
                cmd.Parameters["@vEventDescription"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("@vEventLog", vDoc_type);
                cmd.Parameters["@vEventLog"].Direction = ParameterDirection.Input;

                await con.OpenAsync();
                cmd.Connection = con;
                var isSuccess = await cmd.ExecuteNonQueryAsync();
                await con.CloseAsync();

                return isSuccess;
            }
            catch { throw; }
        }
    }
}
