using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.ResDTOs
{
	public class GetStateDetailsByIdResDTO
	{
		public int CountryId { get; set; }

		public string StateName { get; set; } = null!;

		public string Iso2 { get; set; } = null!;
	}
}
