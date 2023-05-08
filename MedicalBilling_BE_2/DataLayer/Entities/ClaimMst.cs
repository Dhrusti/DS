using System;
using System.Collections.Generic;

namespace DataLayer.Entities;

public partial class ClaimMst
{
    public int Id { get; set; }

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

    public bool IsDelete { get; set; }

    public int CreatedBy { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }
}
