using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReqDTO
{
    public class CountryPaginationFeaturesDTO
    {
        public int CountryId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public bool OrderByAsc { get; set; } 
    }
}
