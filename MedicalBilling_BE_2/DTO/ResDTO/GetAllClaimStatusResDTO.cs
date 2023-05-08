using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResDTO
{
    public class GetAllClaimStatusResDTO
    {
        public int TotalCount { get; set; }

        public List<ClaimStatusList> ClaimStatusLists { get; set; }


    }
    public class ClaimStatusList
    {
        public int ClaimStatusId { get; set; }
        public int OrganizationId { get; set; }

        public int CompanyId { get; set; }

        public int DepartmentId { get; set; }

        public string ClaimStatusName { get; set; } = null!;

        public bool IsActive { get; set; }
    }
}
