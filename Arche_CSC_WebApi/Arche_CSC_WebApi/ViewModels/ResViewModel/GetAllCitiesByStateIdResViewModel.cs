namespace Arche_CSC_WebApi.ViewModels.ResViewModel
{
	public class GetAllCitiesByStateIdResViewModel
	{
		public int CityId { get; set; }

		public int StateId { get; set; }

		public string CityName { get; set; } = null!;
	}
}
