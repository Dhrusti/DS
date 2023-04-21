using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WebAPI.ViewModels.ReqViewModels
{
	public class AddCostCenterReqViewModel
	{
		public string CostCenterMstName { get; set; }
	}
}
