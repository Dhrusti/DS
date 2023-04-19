using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReqDTO
{
    public class CityPaginationDTO
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; }
        public bool OrderByAsc { get; set; }
        public int StateMainId { get; set; } = 1;
    }
}
