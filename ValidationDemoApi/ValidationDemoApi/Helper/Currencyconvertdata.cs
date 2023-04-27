using Newtonsoft.Json;
using RestSharp;
using ValidationDemoApi.Entities;
using ValidationDemoApi.Models;

namespace ValidationDemoApi.Helper
{
    public class Currencyconvertdata
    {
        public string currencyconvert(CurrencyModel currencyModel)
        {
            double amount = currencyModel.Amount;
            string from = currencyModel.From;
            string to = currencyModel.To;

            var Url = "https://api.apilayer.com/fixer/convert?";
            var client = new RestClient(Url + "to="+to.ToUpper()+"&from="+from.ToUpper()+"&amount="+amount);

            var request = new RestRequest(Method.GET);
            request.AddHeader("apikey", "ZlUQAALrPOecw9xZa8ZDbqQSpfyE9jKd");

            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            CurrencyResModel model = new CurrencyResModel();
            string myDeserializedClass = JsonConvert.SerializeObject(response.Content).ToString() ?? response.Content;
            //var jsondata = JsonConvert.SerializeObject(response.Content);
            //model = JsonConvert.DeserializeObject<CurrencyResModel>(jsondata);
            return response.Content;
        }
        public StockModel StockData(string StockName)
        {
            StockModel model = new StockModel();
            var client = new RestClient("http://api.marketstack.com/v1/intraday?access_key=b6c23135c7662aaed443b2fdd87e80ce&symbols=" + StockName);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            string myDeserializedClass = JsonConvert.SerializeObject(response.Content).ToString() ?? response.Content;
            //var jsondata = JsonConvert.SerializeObject(response.Content);
            model = JsonConvert.DeserializeObject<StockModel>(response.Content);
            var res = model.data.Where(x=>x.date.Date == DateTime.Now.Date);
            model.data = res.ToList();

            return model;
        }
        public SymbolStockModel SymbolStockData()
        {
            SymbolStockModel model = new SymbolStockModel();
            var client = new RestClient("http://api.marketstack.com/v1/tickers?access_key=b6c23135c7662aaed443b2fdd87e80ce");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            string myDeserializedClass = JsonConvert.SerializeObject(response.Content).ToString() ?? response.Content;
            //var jsondata = JsonConvert.SerializeObject(response.Content);
            model = JsonConvert.DeserializeObject<SymbolStockModel>(response.Content);
          
            

            return model;
        }


    }
}
