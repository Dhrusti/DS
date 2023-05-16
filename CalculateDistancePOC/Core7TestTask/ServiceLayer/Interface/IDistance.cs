﻿using DTO.ReqDTO;
using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface
{
	public interface IDistance
	{
		public CommonResponse GetDistanceByZipCodes(GetDistanceByZipCodesReqDTO getDistanceByZipCodesReqDTO);
	}
}
