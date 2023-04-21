using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataLayer.Entities
{
	public class RefereceDocumentCategoryMst
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string RefDocCatId { get; set; }
		public string RefDocCatName { get; set;}
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public int CreatedBy { get; set; }
		public int UpdatedBy { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime UpdatedDate { get; set; }
	}
}
