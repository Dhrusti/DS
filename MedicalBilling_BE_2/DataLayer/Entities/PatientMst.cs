using System;
using System.Collections.Generic;

namespace DataLayer.Entities;

public partial class PatientMst
{
    public int Id { get; set; }

    public int OrganizationId { get; set; }

    public int CompanyId { get; set; }

    public int DepartmentId { get; set; }

    public int PayerId { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Mobile { get; set; }

    public string? Email { get; set; }

    public string? RenderingFullName { get; set; }

    public string? RefferingFullName { get; set; }

    public string PatientName { get; set; } = null!;

    public string? PatientCode { get; set; }

    public DateTime? PatientDob { get; set; }

    public string? MedicalRecordCode { get; set; }

    public string? Eaibcode { get; set; }

    public bool IsActive { get; set; }

    public bool IsDelete { get; set; }

    public int CreatedBy { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }
}
