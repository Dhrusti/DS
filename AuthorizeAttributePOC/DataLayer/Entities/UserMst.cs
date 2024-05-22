using System;
using System.Collections.Generic;

namespace DataLayer.Entities;

public partial class UserMst
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string UserType { get; set; } = null!;

    public int UserStatusId { get; set; }

    public string Address { get; set; } = null!;

    public int DepartmentId { get; set; }

    public int DesignationId { get; set; }

    public string ContactNo { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsDelete { get; set; }

    public int CreatedBy { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }
}
