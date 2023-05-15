using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.ReqDTOs
{
	public class UpdateCityReqDTO
	{
		public int CityId { get; set; }

		public int StateId { get; set; }

		public string CityName { get; set; } = null!;
	}
}
