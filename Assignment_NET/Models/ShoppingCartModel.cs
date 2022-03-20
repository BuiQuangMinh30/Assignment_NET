using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment_NET.Models
{
    public class ShoppingCartModel
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double Total { get; set; }
        public string Images { get; set; }
    }
}
