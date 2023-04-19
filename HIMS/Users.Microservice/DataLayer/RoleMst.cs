using System;
using System.Collections.Generic;

namespace DataLayer
{
    public partial class RoleMst
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreateBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
