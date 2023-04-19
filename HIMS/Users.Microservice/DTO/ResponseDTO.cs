using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ResponseDTO
    {
        public string Message { set; get; }
        public HttpStatusCode StatusCode { set; get; }
        public dynamic Data { set; get; }
    }
}
