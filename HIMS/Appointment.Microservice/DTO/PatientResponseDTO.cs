using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PatientResponseDTO1
    {
        public string Message { set; get; }
        public HttpStatusCode StatusCode { set; get; }
        public List<PatientDetailsResponseDTO> Data { set; get; }
    }
    public class PatientDetailsResponseDTO
    {
        public int? PatientId { get; set; }
        public string? PatientCode { get; set; }
        public int? UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public DateTime? Dob { get; set; }
        public string? Email { get; set; }
        public string? MobileNo { get; set; }
        public string? AlternateMobileNo { get; set; }
        public string? Address { get; set; }
        public string? StreetLandMark { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? Pincode { get; set; }
        public string? Image { get; set; }
        public string? ImagePath { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
