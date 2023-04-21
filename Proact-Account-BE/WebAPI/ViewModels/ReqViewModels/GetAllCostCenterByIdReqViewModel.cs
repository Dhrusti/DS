using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WebAPI.ViewModels.ReqViewModels
{
	public class GetAllCostCenterByIdReqViewModel
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }
	}
}
