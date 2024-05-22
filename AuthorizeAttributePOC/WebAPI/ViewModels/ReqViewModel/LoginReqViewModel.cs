using System.ComponentModel.DataAnnotations;

namespace WebAPI.ViewModels.ReqViewModel
{
    public class LoginReqViewModel
    {
        [Required(ErrorMessage = "Email Id is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Id")]
        [StringLength(50, ErrorMessage = "Email Id can't be longer than 50 characters")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
