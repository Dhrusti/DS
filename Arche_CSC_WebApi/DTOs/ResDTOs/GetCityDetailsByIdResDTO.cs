using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.ResDTOs
{
	public class GetCityDetailsByIdResDTO
	{
		public int CityId { get; set; }

		public int StateId { get; set; }

		public string CityName { get; set; } = null!;
	}
}
