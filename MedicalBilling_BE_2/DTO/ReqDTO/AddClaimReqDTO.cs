using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReqDTO
{
    public class AddClaimReqDTO
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
    }
}
