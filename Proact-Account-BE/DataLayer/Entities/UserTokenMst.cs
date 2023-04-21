using System;
using System.Collections.Generic;

namespace DataLayer.Entities;

public partial class UserTokenMst
{
    
    public int Id { get; set; }

    public string UserId { get; set; } = null!;

    public string Token { get; set; } = null!;

    public string RefreshToken { get; set; } = null!;

    public DateTime TockenExpiredOn { get; set; }

    public DateTime RefreshTockenExpiredOn { get; set; }

    public bool? IsActive { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }
}
