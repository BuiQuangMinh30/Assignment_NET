using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment_NET.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string categoryName { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}