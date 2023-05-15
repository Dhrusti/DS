using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.ResDTOs
{
	public class GetAllCountryResDTO
	{
		public int CountryId { get; set; }
		public string? CountryName { get; set; }

		public string? Iso2 { get; set; }

		public string? DialingCode { get; set; }
	}
}
