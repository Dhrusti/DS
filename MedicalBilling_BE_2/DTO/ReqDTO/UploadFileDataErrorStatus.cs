using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReqDTO
{
	public class UploadFileDataErrorStatus
	{
		public bool Status { get; set; } = false;
		public int RowNumber { get; set; }
		public int ColumnNumber { get; set; }
		public string ColumnName { get; set; } = String.Empty;
		public string Description { get; set; } = String.Empty;
	}
}
