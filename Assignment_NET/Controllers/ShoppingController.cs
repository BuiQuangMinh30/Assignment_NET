using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Assignment_NET.Data;
using Assignment_NET.Models;

namespace Assignment_NET.Controllers
{
    public class ShoppingController : Controller
    {
        private ShopDataContext db = new ShopDataContext();

        // GET: Shopping
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        
    }
}
