using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValidationDemoApi.Entities;
using ValidationDemoApi.Helper;

namespace ValidationDemoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyConvertController : ControllerBase
    {
        private readonly Currencyconvertdata _currencyconvertdata;
        public CurrencyConvertController(Currencyconvertdata currencyconvertdata)
        {
            _currencyconvertdata = currencyconvertdata;
        }

        [HttpPost("CurrenConvert")]
        public IActionResult CurrencyConvert(CurrencyModel currencyModel)
        {
            var res = _currencyconvertdata.currencyconvert(currencyModel);
            return Ok(res);
        } 
        [HttpPost("StockData")]
        public IActionResult StockData(string StockName)
        {
            var res = _currencyconvertdata.StockData(StockName);
            return Ok(res);
        }
        [HttpPost("SymbolStockData")]
        public IActionResult SymbolStockData()
        {
            var res = _currencyconvertdata.SymbolStockData();
            return Ok(res);
        }
    }
}
