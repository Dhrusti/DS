using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessLayer;
using DTO.ReqDTO;
using Helper.Models;
using ServiceLayer.Interface;

namespace ServiceLayer.Implementation
{
	public class FileCategoryHistoryImpl : IFileCategoryHistory
	{
		private readonly FileCategoryHistoryBLL _fileCategoryHistoryBLL;
		public FileCategoryHistoryImpl(FileCategoryHistoryBLL fileCategoryHistoryBLL)
		{
			_fileCategoryHistoryBLL = fileCategoryHistoryBLL;
		}

		public CommonResponse UploadFileCategoryHistory(FileCategoryHistoryReqDTO fileCategoryHistoryReqDTO)
		{
			return _fileCategoryHistoryBLL.UploadFileCategoryHistory(fileCategoryHistoryReqDTO);
		}

		public CommonResponse GetAllFileCategoryHistory()
		{
			return _fileCategoryHistoryBLL.GetAllFileCategoryHistory();
		}

		public CommonResponse GetFileCategoryHistoryById(GetFileCategoryHistoryByIdReqDTO getAllFileCategoryHistoryReqDTO)
		{
			return _fileCategoryHistoryBLL.GetFileCategoryHistoryById(getAllFileCategoryHistoryReqDTO);
		}

		public CommonResponse UpdateFileCategoryHistory(UpdateFileCategoryHistoryReqDTO updateFileCategoryHistoryReqDTO)
		{
			return _fileCategoryHistoryBLL.UpdateFileCategoryHistory(updateFileCategoryHistoryReqDTO);
		}

		public CommonResponse DeleteFileCategoryHistory(DeleteFileCategoryHistoryReqDTO deleteFileCategoryHistoryReqDTO)
		{
			return _fileCategoryHistoryBLL.DeleteFileCategoryHistory(deleteFileCategoryHistoryReqDTO);
		}
	}
}
