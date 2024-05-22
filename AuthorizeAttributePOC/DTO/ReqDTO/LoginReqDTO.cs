using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReqDTO
{
    public class LoginReqDTO
    {
        [Required(ErrorMessage = "Email Id is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Id")]
        [StringLength(50, ErrorMessage = "Email Id can't be longer than 50 characters")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
