﻿using Helper.CommonHelper;
using System.Net;

namespace Helpers.CommonModels
{
    public class CommonResponse
    {
        public bool Status { get; set; } = false;
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.InternalServerError;
        public string Message { get; set; } = CommonConstants.Something_Went_Wrong_Please_Try_Again;
        public dynamic Data { get; set; } = null;
    }
}
