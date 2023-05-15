namespace Arche_CSC_WebApi.ViewModels.ReqViewModel
{
	public class UpdateStateReqViewModel
	{
		public int StateId { get; set; }

		public int CountryId { get; set; }

		public string StateName { get; set; } = null!;

		public string Iso2 { get; set; } = null!;
	}
}
