﻿using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class CityMst
    {
        public int CityMainId { get; set; }
        public int? CityId { get; set; }
        public int? StateMainId { get; set; }
        public string? CityName { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
    }
}
