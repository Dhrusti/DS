using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataLayer.Entities
{
	public class VoucherMst
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }
		public string VoucherName { get; set; }

		public bool IsActive { get; set; }
		public bool IsDelete { get; set; }

		public DateTime CreatedDate { get; set; }
		public DateTime UpdatedDate { get; set; }
		public int CreatedBy { get; set; }
		public int UpdatedBy { get; set; }

	}
}
