using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResDTO
{
	public class SiderbarResDTO
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Navigate { get; set; }
		public string AngleIcon { get; set; }
		public string Icon { get; set; }
		public int ParentId { get; set; }
		public int LevelId { get; set; }
		public string PermissionCode { get; set; }
		//public bool HasPermission { get; set; }
		public string Active { get; set; }

		public List<SiderbarResDTO> Childs { get; set; }
    }

	public class MenuDetail
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Navigate { get; set; }
        public string AngleIcon { get; set; }
        public string Icon { get; set; }
        public int ParentId { get; set; }
        public int LevelId { get; set; }
		public string PermissionCode { get; set; }
		public string Active { get; set; }

		//public bool HasPermission { get; set; }
	}
}
