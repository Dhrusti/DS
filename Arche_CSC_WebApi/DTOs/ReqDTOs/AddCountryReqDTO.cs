using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.ReqDTOs
{
	public class AddCountryReqDTO
	{
		public string? CountryName { get; set; }

		public string? Iso2 { get; set; }

		public string? DialingCode { get; set; }
	}
}
