using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment_NET.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string TenSanPham { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double Total
        {
            get
            {
                return Quantity * UnitPrice;
            }    
            }
        public string Images { get; set; }

    }
}