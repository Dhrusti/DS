using System;
using System.Collections.Generic;

namespace DataLayer
{
    public partial class TokenMst
    {
        public int TokenId { get; set; }
        public string UserName { get; set; } = null!;
        public string Token { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public DateTime CreateAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime ExpiredOn { get; set; }
    }
}
