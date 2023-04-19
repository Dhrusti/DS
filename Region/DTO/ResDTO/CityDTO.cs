using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResDTO
{
    public class CityDTO
    {
        public int CityMainId { get; set; }
        public int? CityId { get; set; }
        public int? StateMainId { get; set; }
        public string? CityName { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
    }
}
