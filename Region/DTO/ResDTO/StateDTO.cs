using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResDTO
{
    public class StateDTO
    {
        public int StateMainId { get; set; }
        public int? StateId { get; set; }
        public int? CountryMainId { get; set; }
        public string? StateName { get; set; }
        public string? StateCode { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
    }
}
