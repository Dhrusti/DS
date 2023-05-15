using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
	public class CommonHelper
	{
		public string TrimSpacesBetweenString(string s)
		{
			var mystring = s.Split(new string[] { " " }, StringSplitOptions.None);
			string result = string.Empty;
			foreach (var mstr in mystring)
			{
				var ss = mstr.Trim();
				if (!string.IsNullOrEmpty(ss))
				{
					result = result + ss + " ";
				}
			}
			return result.Trim();

		}
	}
}
