using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebAPI.ViewModels.ReqViewModels
{
    public class GetLevelTypesReqViewModel
    {
        [BsonId]
        [BsonIgnoreIfNull]
        [BsonRepresentation(BsonType.ObjectId)]

        public string? ParentLevelTypeId { get; set; }
        public int LevelId { get; set; }
    }
}
