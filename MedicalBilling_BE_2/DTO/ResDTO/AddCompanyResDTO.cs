﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResDTO
{
    public class AddCompanyResDTO
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; } = null!;

        public string CompanyDisplayName { get; set; } = null!;
    }
}
