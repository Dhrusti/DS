using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResDTO
{
    public class UpdateClaimStatusResDTO
    {
        public int ClaimStatusId { get; set; }

        public string ClaimStatusName { get; set; } = null!;
    }
}
