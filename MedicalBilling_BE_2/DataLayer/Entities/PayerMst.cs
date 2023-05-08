using System;
using System.Collections.Generic;

namespace DataLayer.Entities;

public partial class PayerMst
{
    public int Id { get; set; }

    public string PayerName { get; set; } = null!;

    public string? PayerCode { get; set; }

    public string? Componant { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Mobile { get; set; }

    public string? Email { get; set; }

    public string? Website { get; set; }

    public bool IsActive { get; set; }

    public bool IsDelete { get; set; }

    public int CreatedBy { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }
}
