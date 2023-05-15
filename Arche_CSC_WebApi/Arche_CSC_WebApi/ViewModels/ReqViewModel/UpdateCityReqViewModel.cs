namespace Arche_CSC_WebApi.ViewModels.ReqViewModel
{
	public class UpdateCityReqViewModel
	{
		public int CityId { get; set; }

		public int StateId { get; set; }

		public string CityName { get; set; } = null!;
	}
}
