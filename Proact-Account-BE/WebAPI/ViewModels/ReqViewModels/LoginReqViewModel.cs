namespace WebAPI.ViewModels.ReqViewModels
{
    public class LoginReqViewModel
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; } = false;
    }
}
