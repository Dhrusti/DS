using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RegionWebAPI.ViewModel.ReqViewModel;
using RegionWebAPI.ViewModel.ResViewModel;
using ServiceLayer.Interface;

namespace RegionWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly RegionDbContext _context;
        private readonly ICountry _icountry;

        public CountryController(ICountry icountry, RegionDbContext context)
        {
            _icountry = icountry;
            _context = context;
        }

        [HttpPost("CountrieswithPagination")]
        public CommonResponse CountrieswithPagination(CountryPaginationFeaturesDTO countrypaginationFeaturesDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _icountry.ImplCountrieswithPagination(countrypaginationFeaturesDTO);
                List<CountryDTO> countryDTOs = commonResponse.Data ?? new List<CountryDTO>();
                commonResponse.Data = countryDTOs.Adapt<List<CountryModel>>();
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
