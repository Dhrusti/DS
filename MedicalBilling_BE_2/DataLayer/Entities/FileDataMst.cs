using System;
using System.Collections.Generic;

namespace DataLayer.Entities;

public partial class FileDataMst
{
    public int Id { get; set; }

    public int FileCategoryHistoryId { get; set; }

    public int FileHistoryId { get; set; }

    public string PayerName { get; set; } = null!;

    public string? PayerCode { get; set; }

    public string? RenderingFullName { get; set; }

    public string? RefferingFullName { get; set; }

    public string PatientName { get; set; } = null!;

    public string? PatientCode { get; set; }

    public DateTime? PatientDob { get; set; }

    public string? MedicalRecordCode { get; set; }

    public string? Eaibcode { get; set; }

    public string? Componant { get; set; }

    public string? PayerPhone { get; set; }

    public string PolicyCode { get; set; } = null!;

    public string ClaimStatus { get; set; } = null!;

    public string? ClaimCode { get; set; }

    public DateTime? DateOfService { get; set; }

    public string? ServiceCpt { get; set; }

    public string? ServiceCode { get; set; }

    public string? Modifier { get; set; }

    public string? DiagnosisCode1 { get; set; }

    public string? DiagnosisCode2 { get; set; }

    public string? DiagnosisCode3 { get; set; }

    public string? DiagnosisCode4 { get; set; }

    public string? Cob { get; set; }

    public decimal? InsuranceAmount1 { get; set; }

    public decimal? InsuranceAmount2 { get; set; }

    public decimal? InsuranceAmount3 { get; set; }

    public decimal? InsuranceAmount4 { get; set; }

    public decimal ChargeAmount { get; set; }

    public decimal? LineItemAmount { get; set; }

    public DateTime? LastBillDate { get; set; }

    public bool IsActive { get; set; }

    public bool IsDelete { get; set; }

    public int CreatedBy { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }
}
