using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebAPI.ViewModels.ReqViewModels
{
    public class DeleteLevelReqViewModel
    {
        [BsonId]
        [BsonIgnoreIfNull]
        [BsonRepresentation(BsonType.ObjectId)]

        public string? id { get; set; }
        public int LevelId { get; set; }
    }
}
