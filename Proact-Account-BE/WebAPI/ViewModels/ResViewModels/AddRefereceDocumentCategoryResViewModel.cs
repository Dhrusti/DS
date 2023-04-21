using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WebAPI.ViewModels.ResViewModels
{
	public class AddRefereceDocumentCategoryResViewModel
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string RefDocCatId { get; set; }
		public string RefDocCatName { get; set; }
	}
}
