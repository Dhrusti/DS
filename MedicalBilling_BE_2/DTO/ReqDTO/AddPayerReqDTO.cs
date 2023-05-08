using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReqDTO
{
    public class AddPayerReqDTO
    {
        public string PayerName { get; set; } = null!;

        public string? PayerCode { get; set; }

        public string? Componant { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }

        public string? Mobile { get; set; }

        public string? Email { get; set; }

        public string? Website { get; set; }

    }
}
