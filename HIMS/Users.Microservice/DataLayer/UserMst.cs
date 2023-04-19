using System;
using System.Collections.Generic;

namespace DataLayer
{
    public partial class UserMst
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreateBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
