using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebAPI.ViewModels.ResViewModels
{
    public class DeleteLevelResViewModel
    {
        [BsonId]
        [BsonIgnoreIfNull]
        [BsonRepresentation(BsonType.ObjectId)]

        public string? id { get; set; }
    }
}
