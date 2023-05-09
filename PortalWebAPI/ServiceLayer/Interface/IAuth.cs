using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs.ReqDTOs;
using Helpers.CommonModels;

namespace ServiceLayer.Interface
{
	public interface IAuth
	{
		public Task<CommonResponse> LoginAsync(LogInReqDTO request);
	}
}
