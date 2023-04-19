using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RegionWebAPI.ViewModel.ResViewModel;
using ServiceLayer.Interface;

namespace RegionWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly RegionDbContext _context;
        private readonly ICity _icity;

        public CityController(RegionDbContext context, ICity icity)
        {
            _context = context;
            _icity = icity;
        }

        [HttpPost("CitiesWithPagination")]
        public CommonResponse CitiesWithPagination(CityPaginationDTO cityPaginationDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _icity.ImplCitiesWithPagination(cityPaginationDTO);
                List<CityDTO> cityDTO = commonResponse.Data ?? new List<CityDTO>();
                commonResponse.Data = cityDTO.Adapt<List<CityModel>>();
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
