using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResDTO
{
    public class LoginResDTO
    {
        public int UserId { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string RoleType { get; set; }
        public string? ImagePath { get; set; }
        public string FullName { get; set; }
        public DateTime TokenExpiryTime { get; set; }
        public string TimeZone { get; set; }
        public bool IsWebLoggedInFirstTime { get; set; }
        public int PendingNotificationCount { get; set; }
        public dynamic AdminPermission { get; set; }
    }
}
