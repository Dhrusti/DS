using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class LoginResponseDTO
    {
        public string Message { set; get; }
        public HttpStatusCode StatusCode { set; get; }
        public List<UserMstDTO> Data { set; get; }
        //public int UserId { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public DateTime DOB { get; set; }
        //public string Address { get; set; }
        //public string PostalCode { get; set; }
        //public decimal Mobile { get; set; }
        //public string EmailId { get; set; }
        //public string UserName { get; set; }
        //public string Role { get; set; }
        //public DateTime? FirstTimeLogin { get; set; }
        //public string token { get; set; }
        //public string refreshtoken { get; set; }
    }
}
