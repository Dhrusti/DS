using System;
using System.Collections.Generic;

namespace DataLayer.Entities;

public partial class CompanyMst
{
    public int Id { get; set; }

    public int OrganizationId { get; set; }

    public string CompanyName { get; set; } = null!;

    public string CompanyDisplayName { get; set; } = null!;

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

    public bool IsDelete { get; set; }

    public int CreatedBy { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }
}
