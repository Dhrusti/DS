using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WebAPI.ViewModels.ResViewModels
{
	public class DeleteVoucherNameResViewModel
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }
		public string VoucherName { get; set; }
	}
}
