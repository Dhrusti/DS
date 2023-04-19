using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;
using DTO.ReqDTO;
using Helper;
using ServiceLayer.Interface;

namespace ServiceLayer.Implementation
{
    public class ImplCountry : ICountry
    {
        private readonly CountryBLL _countryBLL;
        public ImplCountry(CountryBLL countryBLL)
        {
            _countryBLL = countryBLL;
        }

        public CommonResponse ImplCountrieswithPagination(CountryPaginationFeaturesDTO countrypaginationFeaturesDTO)
        {
            return _countryBLL.CountrieswithPaginationBLL(countrypaginationFeaturesDTO);
        }

    }
}
