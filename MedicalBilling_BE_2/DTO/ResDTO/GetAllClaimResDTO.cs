using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResDTO
{
    public class GetAllClaimResDTO
    {
        public int TotalCount { get; set; }

        public List<ClaimList> claimLists { get; set; }

    }

    public class ClaimList
    {
        public int OrganizationId { get; set; }

        public int CompanyId { get; set; }

        public int DepartmentId { get; set; }

        public int PayerId { get; set; }

        public int PatientId { get; set; }

        public int PolicyId { get; set; }

        public string? ClaimCode { get; set; }

        public int ClaimStatusId { get; set; }

        public DateTime? LastBillDate { get; set; }

        public bool IsActive { get; set; }

    }
}
