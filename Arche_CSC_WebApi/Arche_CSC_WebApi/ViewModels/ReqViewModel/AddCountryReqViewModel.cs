using System.ComponentModel.DataAnnotations;

namespace Arche_CSC_WebApi.ViewModels.ReqViewModel
{
	public class AddCountryReqViewModel
	{
		[Required(ErrorMessage = "Field is required !")]
		[StringLength(20, MinimumLength = 2, ErrorMessage = "Minimum length is 2 and Maximum is 20")]
		public string? CountryName { get; set; }

		[Required(ErrorMessage = "Field is required !")]
		public string? Iso2 { get; set; }

		[Required(ErrorMessage = "Field is required !")]
		public string? DialingCode { get; set; }
	}
}
