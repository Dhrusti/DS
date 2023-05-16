using BussinessLayer;
using DTO.ReqDTO;
using Helper;
using ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Implementation
{
	public class DistanceImpl : IDistance
	{
		private readonly DistanceBLL _distanceBLL;

		public DistanceImpl(DistanceBLL distanceBLL)
		{
			_distanceBLL = distanceBLL;
		}

		public CommonResponse GetDistanceByZipCodes(GetDistanceByZipCodesReqDTO getDistanceByZipCodesReqDTO)
		{
			return _distanceBLL.GetDistanceByZipCodes(getDistanceByZipCodesReqDTO);
		}

	}
}
