namespace ValidationDemoApi.Models
{
   

   
    public class Datum1
    {
        public string name { get; set; }
        public string symbol { get; set; }
        public bool has_intraday { get; set; }
        public bool has_eod { get; set; }
        public object country { get; set; }
        public StockExchange stock_exchange { get; set; }
    }

    public class Pagination1
    {
        public int limit { get; set; }
        public int offset { get; set; }
        public int count { get; set; }
        public int total { get; set; }
    }

    public class SymbolStockModel
    {
        public Pagination1 pagination { get; set; }
        public List<Datum1> data { get; set; }
    }

    public class StockExchange
    {
        public string name { get; set; }
        public string acronym { get; set; }
        public string mic { get; set; }
        public string country { get; set; }
        public string country_code { get; set; }
        public string city { get; set; }
        public string website { get; set; }
    }


}
