using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResDTO
{
    public class GetAllPayerResDTO
    {
        public int Id { get; set; }

        public string PayerName { get; set; } = null!;

        public string? PayerCode { get; set; }

    }
}
