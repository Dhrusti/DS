﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResDTO
{
    public class GetDetailOrganizationListResDTO
    {
        public int TotalCount { get; set; }
        public List<OrganizationList> organizationList { get; set; }

        public class OrganizationList
        {
            public int SrNo { get; set; }
            public int OrganizationId { get; set; }
            public string OrganizationName { get; set; } = null!;

            public string OrganizationDisplayName { get; set; } = null!;

            public string? Address { get; set; }

            public string? Phone { get; set; }

            public string? Mobile { get; set; }

            public string? Email { get; set; }

            public string? Website { get; set; }

            public string? FaxNo { get; set; }

            public string? ZipCode { get; set; }

            public string? Npi { get; set; }

            public string? Bcn { get; set; }

            public string? Sonarx { get; set; }

            public string? TaxId { get; set; }

            public bool IsActive { get; set; }

        }

    }
}
