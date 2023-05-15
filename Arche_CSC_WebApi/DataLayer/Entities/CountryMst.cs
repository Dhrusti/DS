using System;
using System.Collections.Generic;

namespace DataLayer.Entities;

public partial class CountryMst
{
    public int CountryId { get; set; }

    public string CountryName { get; set; } = null!;

    public string Iso2 { get; set; } = null!;

    public string DialingCode { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int CreatedBy { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }
}
