using System.ComponentModel.DataAnnotations;

namespace Arche_CSC_WebApi.ViewModels.ReqViewModel
{
	public class AddStateReqViewModel
	{
		[Required(ErrorMessage = "Field is required !")]
		public int CountryId { get; set; }
		[Required(ErrorMessage = "Field is required !")]
		[StringLength(20, MinimumLength = 2, ErrorMessage = "Minimum length is 2 and Maximum is 20")]
		public string StateName { get; set; } = null!;
		[Required(ErrorMessage = "Field is required !")]
		[StringLength(20, MinimumLength = 2, ErrorMessage = "Minimum length is 2 and Maximum is 20")]
		public string Iso2 { get; set; } = null!;
	}
}
