using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DTO.ResDTO
{
    public class DeleteLevelResDTO
    {
        [BsonId]
        [BsonIgnoreIfNull]
        [BsonRepresentation(BsonType.ObjectId)]

        public string? id { get; set; }
    }
}
