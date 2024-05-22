using System;
using System.Collections.Generic;

namespace DataLayer.Entities;

public partial class UserStatusMst
{
    public int Id { get; set; }

    public bool IsActive { get; set; }

    public bool IsDelete { get; set; }

    public int CreatedBy { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public string UserStatus { get; set; } = null!;
}
