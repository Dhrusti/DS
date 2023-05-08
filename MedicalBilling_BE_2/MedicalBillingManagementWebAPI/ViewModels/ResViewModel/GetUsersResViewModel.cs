﻿namespace MedicalBillingManagementWebAPI.ViewModels.ResViewModel
{
	public class GetUsersResViewModel
	{
		public int UserId { get; set; }

		public string FirstName { get; set; } = null!;

		public string LastName { get; set; } = null!;

		public string UserName { get; set; } = null!;

		public DateTime Dob { get; set; }

		public string MobileNo { get; set; } = null!;

		public string Email { get; set; } = null!;

		public bool IsActive { get; set; }

		public int RoleId { get; set; }
		public string RoleName { get; set; }
	}
}
