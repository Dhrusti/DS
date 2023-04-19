using System;
using System.Collections.Generic;

namespace DataLayer
{
    public partial class RegionsMst
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; } = null!;
        public int StateId { get; set; }
        public string StateName { get; set; } = null!;
        public int CityId { get; set; }
        public string CityName { get; set; } = null!;
    }
}
