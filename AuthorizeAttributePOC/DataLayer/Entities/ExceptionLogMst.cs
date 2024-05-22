using System;
using System.Collections.Generic;

namespace DataLayer.Entities;

public partial class ExceptionLogMst
{
    public int Id { get; set; }

    public DateTime ExecutionDate { get; set; }

    public string? Apiurl { get; set; }

    public string? MethodType { get; set; }

    public string? Message { get; set; }

    public string? StackTrace { get; set; }
}
