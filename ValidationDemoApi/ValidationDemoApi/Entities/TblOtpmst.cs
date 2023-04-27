using System;
using System.Collections.Generic;

namespace ValidationDemoApi.Entities
{
    public partial class TblOtpmst
    {
        public int Otpid { get; set; }
        public string ContactNumber { get; set; } = null!;
        public string OneTimePassword { get; set; } = null!;
        public DateTime? Otpcreated { get; set; }
        public DateTime? Otpexpires { get; set; }
    }
}
