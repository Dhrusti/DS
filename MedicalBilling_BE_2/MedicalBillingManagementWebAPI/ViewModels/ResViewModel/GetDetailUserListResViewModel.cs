using DTO.ResDTO;

namespace MedicalBillingManagementWebAPI.ViewModels.ResViewModel
{
	public class GetDetailUserListResViewModel
	{
		public List<GetDetailUser> GetDetailUserList { get; set; }
		public int TotalCount { get; set; }
	}
}
