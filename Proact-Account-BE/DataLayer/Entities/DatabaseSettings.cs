using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
	public class DatabaseSettings
	{
		public string? ConnectionString { get; set; }
		public string? DatabaseName { get; set; }
		public string? levelfirstmst { get; set; }
		public string? VoucherCollectionName { get; set; }
		public string? CostCenterCollectionName { get; set; }
		public string? RefereceDocumentCategoryName { get; set; }
	}
}
