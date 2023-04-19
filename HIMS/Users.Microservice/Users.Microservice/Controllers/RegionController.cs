using DTO;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.IService;

namespace Users.Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IRegion _iRegion;
        public RegionController(IRegion iRegion)
        {
            this._iRegion = iRegion;
        }

        [HttpGet]
        [Route("GetAllCountry")]
        public async Task<ResponseDTO> GetAllCountry()
        {
            return await _iRegion.GetAllCountry();
        }

        [HttpGet]
        [Route("GetCountryByNameAsync")]
        public async Task<ResponseDTO> GetCountryByNameAsync(string CountryName)
        {
            return await _iRegion.GetCountryByNameAsync(CountryName);
        }

        [HttpGet]
        [Route("GetAllStateByCountryAsync")]
        public async Task<ResponseDTO> GetAllStateByCountryAsync(int CountryId)
        {
            return await _iRegion.GetAllStateByCountryAsync(CountryId);
        }

        [HttpGet]
        [Route("GetStateByNameAsync")]
        public async Task<ResponseDTO> GetStateByNameAsync(string StateName , int CountryId)
        {
            return await _iRegion.GetStateByNameAsync(StateName, CountryId);
        }

        [HttpGet]
        [Route("GetStateByStateIdAsync")]
        public async Task<ResponseDTO> GetStateByStateAsync(int StateId)
        {
            return await _iRegion.GetStateByStateAsync(StateId);
        }

        [HttpGet]
        [Route("GetCityByStateAsync")]
        public async Task<ResponseDTO> GetCityByStateAsync(int StateId)
        {
            return await _iRegion.GetCityByStateAsync(StateId);
        }

        [HttpGet]
        [Route("GetCityByCityIdAsync")]
        public async Task<ResponseDTO> GetCityByCityIdAsync(int CityId)
        {
            return await _iRegion.GetCityByCityIdAsync(CityId);
        }

        [HttpGet]
        [Route("GetCityByNameAsync")]
        public async Task<ResponseDTO> GetCityByNameAsync(string CityName, int StateId)
        {
            return await _iRegion.GetCityByNameAsync(CityName, StateId);
        }
    }
}
