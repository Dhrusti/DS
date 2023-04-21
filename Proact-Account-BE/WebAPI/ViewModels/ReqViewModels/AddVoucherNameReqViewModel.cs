using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.ViewModels.ReqViewModels
{
	public class AddVoucherNameReqViewModel
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }
		[Required(ErrorMessage = "Please enter valid VoucherName.")]
		[StringLength(20, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 20 characters.")]
		public string VoucherName { get; set; }
	}
}
