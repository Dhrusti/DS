﻿using System;
using System.Collections.Generic;

namespace DataLayer.Entities;

public partial class GenderMst
{
    public int GenderId { get; set; }

    public string Gender { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int CreatedBy { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }
}
