using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.ResDTOs
{
	public class GetAllStatesByCountryIdResDTO
	{
		public int StateId { get; set; }
		public string StateName { get; set; } = null!;
	}
}
