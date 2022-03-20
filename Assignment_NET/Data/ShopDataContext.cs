using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Assignment_NET.Data
{
    public class ShopDataContext : DbContext
    {
        public ShopDataContext() : base("ConnectionString")
        {

        }

        public System.Data.Entity.DbSet<Assignment_NET.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<Assignment_NET.Models.Product> Products { get; set; }

        public System.Data.Entity.DbSet<Assignment_NET.Models.ShoppingCartModel> ShoppingCartModels { get; set; }
        
        public System.Data.Entity.DbSet<Assignment_NET.Models.OrderDetail> OrderDetails { get; set; }
        public System.Data.Entity.DbSet<Assignment_NET.Models.Order> Orders { get; set; }
    }
}