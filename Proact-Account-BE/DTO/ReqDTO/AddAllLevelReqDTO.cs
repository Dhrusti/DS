using MongoDB.Bson.Serialization.Attributes;

namespace DTO.ReqDTO
{
    public class AddAllLevelReqDTO
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string ParentLevelTypeId { get; set; }
        public bool IsFinalLevel { get; set; }
        public string? CreditOrDebit { get; set; }
        [BsonIgnore]
        public int LevelId { get; set; }

    }
}
