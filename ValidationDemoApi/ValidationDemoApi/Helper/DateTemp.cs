using System.Globalization;
using ValidationDemoApi.Models;

namespace ValidationDemoApi.Helper
{
    public class DateTemp
    {
        public string DateConvert(string dateOnly)

        {
            string[] formats = {"dd/MM/yyyy", "dd-MMM-yyyy", "yyyy-MM-dd",
                   "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy","MM-dd-yyyy","MM/dd/yyyy"};
            string converted = DateTime.ParseExact(dateOnly, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("MM-dd-yyyy");
            return converted;
        }
    }
}
