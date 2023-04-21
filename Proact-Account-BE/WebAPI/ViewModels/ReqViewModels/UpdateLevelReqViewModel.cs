using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.ViewModels.ReqViewModels
{
    public class UpdateLevelReqViewModel
    {
        [BsonId]
        [BsonIgnoreIfNull]
        [BsonRepresentation(BsonType.ObjectId)]
        [Required]
        public string? id { get; set; }
        [Required]
        public string? Code { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string ParentLevelTypeId { get; set; }
        [Required]
        public bool IsFinalLevel { get; set; }
        [Required]
        public string? CreditOrDebit { get; set; }
        [BsonIgnore]
        [Required]
        public int LevelId { get; set; }
    }
}
