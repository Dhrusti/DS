using DTO.ResDTO;

namespace MedicalBillingManagementWebAPI.ViewModels.ResViewModel
{
    public class LogInResViewModel
    {
        public UserDetail UserDetail { get; set; }
        public RoleDetail RoleDetail { get; set; }
        public List<Permissions> Permissions { get; set; }
		public List<SiderbarResDTO> SidebarList { get; set; }
		public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
    public class UserDetail
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }

    public class RoleDetail
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }

    public class Permissions
    {
        public int PermissionId { get; set; }
        public string PermissionName { get; set; }
        public string PermissionCode { get; set; }
        public bool HasPermission { get; set; }
    }

}
