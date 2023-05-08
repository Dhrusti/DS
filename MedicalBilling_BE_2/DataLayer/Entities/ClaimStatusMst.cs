﻿using System;
using System.Collections.Generic;

namespace DataLayer.Entities;

public partial class ClaimStatusMst
{
    public int Id { get; set; }

    public int OrganizationId { get; set; }

    public int CompanyId { get; set; }

    public int DepartmentId { get; set; }

    public string ClaimStatusName { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsDelete { get; set; }

    public int CreatedBy { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }
}