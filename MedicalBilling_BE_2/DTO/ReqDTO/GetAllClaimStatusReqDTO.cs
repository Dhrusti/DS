using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReqDTO
{
    public class GetAllClaimStatusReqDTO
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public bool OrderBy { get; set; }
        public string? GlobalSearch { get; set; }
    }
}
