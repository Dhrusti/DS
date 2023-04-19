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
    public class ImplCity : ICity
    {
        private readonly CityBLL _cityBLL;

        public ImplCity (CityBLL cityBLL)
        {
            _cityBLL = cityBLL;
        }

        public CommonResponse ImplCitiesWithPagination(CityPaginationDTO cityPaginationDTO)
        {
            return _cityBLL.CitiesWithPaginationBLL(cityPaginationDTO);
        }
    }
}
