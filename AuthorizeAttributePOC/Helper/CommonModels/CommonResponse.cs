using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Helper.CommonModels
{
    public class CommonResponse
    {
        public bool Status { get; set; } = false;
        public string Message { get; set; } = "Something went wrong!";
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.InternalServerError;
        public dynamic Data { get; set; } = null;
    }
}
