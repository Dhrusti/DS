using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DTO.ReqDTO
{
    public class DeleteLevelReqDTO
    {
        [BsonId]
        [BsonIgnoreIfNull]
        [BsonRepresentation(BsonType.ObjectId)]

        public string? id { get; set; }
        public int LevelId { get; set; }
    }
}
