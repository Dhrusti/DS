using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReqDTO
{
	public class AddFileHistoryReqDTO
	{
		public int FileCategoryId { get; set; }

		public string FileName { get; set; } = null!;

		public string FileExtension { get; set; } = null!;

		public string FileSize { get; set; } = null!;

		public string FilePath { get; set; } = null!;

		public DateTime? StartDate { get; set; }

		public DateTime? EndDate { get; set; }

		public bool IsActive { get; set; }

	}
}
