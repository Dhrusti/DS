using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidationDemoApi.Models
{
    public class PortfolioComparisonModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double PortfolioRate { get; set; }
        public double SPRate { get; set; }
    }
}
