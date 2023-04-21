using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.ViewModels.ReqViewModels
{
	public class UpdateVoucherNameReqViewModel
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }

		[Required(ErrorMessage = "Please enter valid VoucherName.")]
		public string VoucherName { get; set; }
	}
}
