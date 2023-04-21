using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WebAPI.ViewModels.ReqViewModels
{
	public class UpdateCostCenterReqViewModel
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }
		public string CostCenterMstName { get; set; }
	}
}
