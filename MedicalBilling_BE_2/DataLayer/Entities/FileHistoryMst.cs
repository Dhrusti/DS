﻿using System;
using System.Collections.Generic;

namespace DataLayer.Entities;

public partial class FileHistoryMst
{
    public int Id { get; set; }

    public int FileCategoryId { get; set; }

    public string FileName { get; set; } = null!;

    public string FileExtension { get; set; } = null!;

    public string FileSize { get; set; } = null!;

    public string FilePath { get; set; } = null!;

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool IsActive { get; set; }

    public bool IsDelete { get; set; }

    public int CreatedBy { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }
}
