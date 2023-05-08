using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReqDTO
{
    public class AddClaimStatusReqDTO
    {
        public int OrganizationId { get; set; }

        public int CompanyId { get; set; }

        public int DepartmentId { get; set; }

        public string ClaimStatusName { get; set; } = null!;

        public bool IsActive { get; set; }

    }
}
