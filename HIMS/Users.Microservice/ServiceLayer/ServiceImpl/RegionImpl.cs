using BusinessLayer;
using DTO;
using ServiceLayer.IService;

namespace ServiceLayer.ServiceImpl
{
    public class RegionImpl : IRegion
    {
        private readonly RegionBLL _regionBLL;
        public RegionImpl(RegionBLL regionBLL)
        {
            _regionBLL = regionBLL;
        }

        public async Task<ResponseDTO> GetAllCountry()
        {
            return await _regionBLL.GetAllCountry();
        }
        public async Task<ResponseDTO> GetCountryByNameAsync(string CountryName)
        {
            return await _regionBLL.GetCountryByNameAsync(CountryName);
        }

        public async Task<ResponseDTO> GetAllStateByCountryAsync(int CountryId)
        {
            return await _regionBLL.GetAllStateByCountryAsync(CountryId);
        }

        public async Task<ResponseDTO> GetStateByStateAsync(int StateId)
        {
            return await _regionBLL.GetStateByStateAsync(StateId);
        }
        public async Task<ResponseDTO> GetStateByNameAsync(string StateName, int CountryId)
        {
            return await _regionBLL.GetStateByNameAsync(StateName, CountryId);
        }

        public async Task<ResponseDTO> GetCityByStateAsync(int StateId)
        {
            return await _regionBLL.GetCityByStateAsync(StateId);
        }

        public async Task<ResponseDTO> GetCityByCityIdAsync(int CityId)
        {
            return await _regionBLL.GetCityByCityIdAsync(CityId);
        }
        public async Task<ResponseDTO> GetCityByNameAsync(string CityName, int StateId)
        {
            return await _regionBLL.GetCityByNameAsync(CityName, StateId);
        }
    }
}
