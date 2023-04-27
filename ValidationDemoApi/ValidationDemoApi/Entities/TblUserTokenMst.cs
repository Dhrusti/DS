using System;
using System.Collections.Generic;

namespace ValidationDemoApi.Entities
{
    public partial class TblUserTokenMst
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? TokenCreated { get; set; }
        public DateTime? TokenExpires { get; set; }
    }
}
