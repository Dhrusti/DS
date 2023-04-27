using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidationDemoApi.Models
{
    public class UserDataModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double NoOfOrders { get; set; }
        public double NoOfOrdersTotal { get; set; }
        public double NoOfOrdersNet { get; set; }
    }
}
