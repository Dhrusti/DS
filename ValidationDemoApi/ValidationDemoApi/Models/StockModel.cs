namespace ValidationDemoApi.Models
{
   
   public class Datum
    {
        public decimal? open { get; set; }
        public decimal? high { get; set; }
        public decimal? low { get; set; }
        public decimal? close { get; set; }
        public decimal? volume { get; set; }
        public decimal? adj_high { get; set; }
        public decimal? adj_low { get; set; }
        public decimal? adj_close { get; set; }
        public decimal? adj_open { get; set; }
        public decimal? adj_volume { get; set; }
        public decimal? split_factor { get; set; }
        public decimal? dividend { get; set; }
        public string? symbol { get; set; }
        public string? exchange { get; set; }
        public DateTime date { get; set; }
    }

    public class Pagination
    {
        public int? limit { get; set; }
        public int? offset { get; set; }
        public int? count { get; set; }
        public int? total { get; set; }
    }

    public class StockModel
    {
        public Pagination pagination { get; set; }
        public List<Datum> data { get; set; }
    }
}

