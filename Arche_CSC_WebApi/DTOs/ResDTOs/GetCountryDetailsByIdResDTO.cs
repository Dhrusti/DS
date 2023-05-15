using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.ResDTOs
{
	public class GetCountryDetailsByIdResDTO
	{
		public string CountryName { get; set; } = null!;

		public string Iso2 { get; set; } = null!;

		public string DialingCode { get; set; } = null!;
	}
}
