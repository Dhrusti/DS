using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Mapster;
using Microsoft.Extensions.Configuration;

namespace BusinessLayer
{
    public class CountryBLL
    {
        private readonly RegionDbContext _context;
        private readonly IConfiguration _configuration;
        public CountryBLL(RegionDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public CommonResponse CountrieswithPaginationBLL(CountryPaginationFeaturesDTO countrypaginationFeaturesDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                var countryList = _context.CountryMsts.OrderBy(x => x.CountryName).ToList();
                if (countrypaginationFeaturesDTO.CountryId > 0)
                {
                    countryList = countryList.Where(x => x.CountryMainId == countrypaginationFeaturesDTO.CountryId).ToList();
                }
                if (countrypaginationFeaturesDTO.PageNumber > 0 && countrypaginationFeaturesDTO.PageSize > 0)
                {
                    countryList = countryList.OrderBy(on => on.CountryMainId)
                        .Skip((countrypaginationFeaturesDTO.PageNumber - 1) * countrypaginationFeaturesDTO.PageSize)
                        .Take(countrypaginationFeaturesDTO.PageSize)
                        .ToList();
                }
                if (!countrypaginationFeaturesDTO.OrderByAsc)
                {
                    countryList = countryList.OrderByDescending(x => x.CountryName).ToList();
                }
                
                if (countryList.Count > 0)
                {
                    commonResponse.Status = true;
                    commonResponse.StatusCode = Convert.ToInt32(HttpStatusCode.OK);
                    commonResponse.Message = "Success.";
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = Convert.ToInt32(HttpStatusCode.NotFound);
                    commonResponse.Message = "Data not Found.";
                }
                commonResponse.Data = countryList.Adapt<List<CountryDTO>>();
            }
            catch (Exception ex)
            {
                commonResponse.Message = ex.Message;
                commonResponse.Data = ex.Data;
            }
            return commonResponse;
        }




    }
}
