using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DTO.ReqDTO
{
	public class AddRefereceDocumentCategoryReqDTO
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string RefDocCatId { get; set; }
		public string RefDocCatName { get; set; }
	}
}
