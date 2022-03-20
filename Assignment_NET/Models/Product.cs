using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Assignment_NET.Models
{
    public partial class Product
    {
        public Product()
        {
            Images = "~/Content/Images/vay_1.jpg";
        }
        public int Id { get; set; }
        public string productName { get; set; }
        public string Images { get; set; }
        public int categoryId { get; set; }
        public double productPrice { get; set; }

        public virtual Category category { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageUpload { get; set; }
    }
}