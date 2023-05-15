using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.ResDTOs
{
	public class GetAllStateResDTO
	{
		public int StateId { get; set; }
		public int CountryId { get; set; }
		public string StateName { get; set; } = null!;
	}
}
