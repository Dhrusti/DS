using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReqDTO
{
    public class StatePaginationDTO
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; }
        public bool OrderByAsc { get; set; }
        public int CountryMainId { get; set; } = 1;
    }
}
