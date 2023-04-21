using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DTO.ReqDTO
{
    public class GetLevelTypeReqDTO
    {
        [BsonId]
        [BsonIgnoreIfNull]
        [BsonRepresentation(BsonType.ObjectId)]

        public string? ParentLevelTypeId { get; set; }
        public int LevelId { get; set; }
    }
}
