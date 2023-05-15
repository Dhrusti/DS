using System;
using System.Collections.Generic;

namespace DataLayer.Entities;

public partial class CityMst
{
    public int CityId { get; set; }

    public int StateId { get; set; }

    public string CityName { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int CreatedBy { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }
}
