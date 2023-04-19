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
    public class CityBLL
    {
        private readonly RegionDbContext _context;
        private readonly IConfiguration _configuration;

        public CityBLL(RegionDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public CommonResponse CitiesWithPaginationBLL(CityPaginationDTO cityPaginationDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                var cityList = _context.CityMsts.OrderBy(x => x.CityName).ToList();
                if (cityPaginationDTO.StateMainId > 0)
                {
                    cityList = cityList.Where(x => x.StateMainId == cityPaginationDTO.StateMainId).ToList();
                }
                else
                {
                    cityList = cityList.Where(x => x.StateMainId == cityPaginationDTO.StateMainId).ToList();
                }
                if (cityPaginationDTO.PageNumber > 0 || cityPaginationDTO.PageSize > 0)
                {
                    cityList = cityList.OrderBy(on => on.StateMainId)
                        .Skip((cityPaginationDTO.PageNumber - 1) * cityPaginationDTO.PageSize)
                        .Take(cityPaginationDTO.PageSize)
                        .ToList();
                }
                if (!cityPaginationDTO.OrderByAsc)
                {
                    cityList = cityList.OrderByDescending(x => x.CityName).ToList();
                }
                if (cityList.Count > 0)
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
                commonResponse.Data = cityList.Adapt<List<CityDTO>>();
            }
        
            catch(Exception ex)
            {
                commonResponse.Message = ex.Message;
                commonResponse.Data = ex.Data;
            }
            return commonResponse;
        }
    }
}
