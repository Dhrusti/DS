using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.IService
{
    public interface IRegion
    {
        public Task<ResponseDTO> GetAllCountry();
        public Task<ResponseDTO> GetCountryByNameAsync(string CountryName);
        public Task<ResponseDTO> GetAllStateByCountryAsync(int CountryId);
        public Task<ResponseDTO> GetStateByStateAsync(int StateId);
        public Task<ResponseDTO> GetStateByNameAsync(string StateName, int CountryId);
        public Task<ResponseDTO> GetCityByStateAsync(int StateId);
        public Task<ResponseDTO> GetCityByCityIdAsync(int CityId);
        public Task<ResponseDTO> GetCityByNameAsync(string CityName, int StateId);

    }
}
