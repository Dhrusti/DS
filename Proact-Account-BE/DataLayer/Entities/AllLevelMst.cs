using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataLayer.Entities
{
    public class AllLevelMst
    {
        [BsonId]
        [BsonIgnoreIfNull]
        [BsonRepresentation(BsonType.ObjectId)]

        public string? id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string ParentLevelTypeId { get; set; }
        public bool IsFinalLevel { get; set; }
        public string? CreditOrDebit { get; set; }
        [BsonIgnore]
        public int LevelId { get; set; }
        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public int CreatedBy { get; set; }

        public int UpdatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
