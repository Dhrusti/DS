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
    public class StateBLL
    {
        private readonly RegionDbContext _context;
        private readonly IConfiguration _configuration;
        public StateBLL(RegionDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public CommonResponse StateswithPaginationBLL(StatePaginationDTO statesPaginationDTO)
        {
            CommonResponse commonResponse = new CommonResponse();

            try
            {
                var stateList = _context.StateMsts.OrderBy(x => x.StateName).ToList();
                if (statesPaginationDTO.CountryMainId > 0)
                {
                    stateList = stateList.Where(x => x.CountryMainId == statesPaginationDTO.CountryMainId).ToList();
                }
                else
                {
                    stateList = stateList.Where(x => x.CountryMainId == statesPaginationDTO.CountryMainId).ToList();
                }
                if (statesPaginationDTO.PageNumber > 0 || statesPaginationDTO.PageSize > 0)
                {
                    stateList = stateList.OrderBy(on => on.CountryMainId)
                        .Skip((statesPaginationDTO.PageNumber - 1) * statesPaginationDTO.PageSize)
                        .Take(statesPaginationDTO.PageSize)
                        .ToList();
                }
                if (!statesPaginationDTO.OrderByAsc)
                {
                    stateList = stateList.OrderByDescending(x => x.StateName).ToList();
                }
                if (stateList.Count > 0)
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
                


                    commonResponse.Data = stateList.Adapt<List<StateDTO>>();
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
