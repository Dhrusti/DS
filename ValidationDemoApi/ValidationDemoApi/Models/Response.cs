namespace ValidationDemoApi.Models
{
    public class Response
    {
        public string Message { get; set; } = string.Empty;
        public bool Status { get; set; } = false;
        public dynamic? Data { get; set; } = null;
        public int Code { get; set; }

    }
}
