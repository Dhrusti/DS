namespace MedicalBillingManagementWebAPI.ViewModels.ReqViewModel
{
	public class AddUserReqViewModel
	{
		public int RoleId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public DateTime DOB { get; set; }
		public string Mobile { get; set; }
		public string Email { get; set; }
		public bool IsActive { get; set; }
	}
}
