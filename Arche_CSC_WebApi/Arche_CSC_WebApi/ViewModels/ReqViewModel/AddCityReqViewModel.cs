using System.ComponentModel.DataAnnotations;

namespace Arche_CSC_WebApi.ViewModels.ReqViewModel
{
	public class AddCityReqViewModel
	{
		[Required(ErrorMessage = "Field is required !")]
		public int StateId { get; set; }
		[Required(ErrorMessage = "Field is required !")]
		[StringLength(20, MinimumLength = 2, ErrorMessage = "Minimum length is 2 and Maximum is 20")]
		public string CityName { get; set; } = null!;
	}
}
