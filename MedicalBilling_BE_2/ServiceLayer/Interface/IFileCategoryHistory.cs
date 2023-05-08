using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.ReqDTO;
using Helper.Models;

namespace ServiceLayer.Interface
{
	public interface IFileCategoryHistory
	{
		public CommonResponse UploadFileCategoryHistory(FileCategoryHistoryReqDTO fileCategoryHistoryReqDTO);
		public CommonResponse GetAllFileCategoryHistory();
		public CommonResponse GetFileCategoryHistoryById(GetFileCategoryHistoryByIdReqDTO getAllFileCategoryHistoryReqDTO);
		public CommonResponse UpdateFileCategoryHistory(UpdateFileCategoryHistoryReqDTO updateFileCategoryHistoryReqDTO);
		public CommonResponse DeleteFileCategoryHistory(DeleteFileCategoryHistoryReqDTO deleteFileCategoryHistoryReqDTO);
	}
}
