using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataLayer.Entities
{
    public class FirstLevelMst
    {
        [BsonId]
        [BsonIgnoreIfNull]
        [BsonRepresentation(BsonType.ObjectId)]
        // [BsonElement("_id")]
        public string? LevelFirstId { get; set; }

        public string? Code { get; set; }

        public string? Name { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public int CreatedBy { get; set; }

        public int UpdatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
