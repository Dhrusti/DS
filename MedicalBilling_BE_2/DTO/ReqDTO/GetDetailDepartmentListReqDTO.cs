using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReqDTO
{
    public class GetDetailDepartmentListReqDTO
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public bool Orderby { get; set; }
        public string GlobalSearch { get; set; }
    }
}
