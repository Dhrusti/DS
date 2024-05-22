﻿namespace WebAPI.ViewModels.ReqViewModel
{
    public class AddEditUserReqViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public int UserStatusId { get; set; }
        public string Address { get; set; }
        public int DepartmentId { get; set; }
        public int DesignationId { get; set; }
        public string ContactNo { get; set; }
    }
}