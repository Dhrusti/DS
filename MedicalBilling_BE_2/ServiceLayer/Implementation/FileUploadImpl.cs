using BussinessLayer;
using DTO.ReqDTO;
using Helper.Models;
using ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Implementation
{
	public class FileUploadImpl : IFileUpload
	{
		private readonly FileUploadBLL _fileUploadBLL;

		public FileUploadImpl(FileUploadBLL fileUploadBLL)
		{
			_fileUploadBLL = fileUploadBLL;
		}

		public CommonResponse UploadFileData(UploadFileDataReqDTO uploadFileDataReqDTO)
		{
			return _fileUploadBLL.UploadFileData(uploadFileDataReqDTO);
		}
	}
}
