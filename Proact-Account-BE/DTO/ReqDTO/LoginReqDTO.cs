namespace DTO.ReqDTO
{
    public class LoginReqDTO
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; } = false;
    }
}
