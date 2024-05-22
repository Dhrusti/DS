namespace WebAPI.ViewModels.ResViewModel
{
    public class LoginResViewModel
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
