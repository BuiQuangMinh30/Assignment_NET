using Assignment_NET.Data;
using Assignment_NET.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace Assignment_NET.Controllers
{
    public class HomeController : Controller
    {
        private ShopDataContext db;
        private List<ShoppingCartModel> listShoppingCarts;


        public HomeController()
        {
            db = new ShopDataContext();
            listShoppingCarts = new List<ShoppingCartModel>();
        }

        [HttpPost]
        public JsonResult Index(int Id)
        {

            ShoppingCartModel objShoppingCartModal = new ShoppingCartModel();
            var objItem = db.Products.Where(s => s.Id == Id).FirstOrDefault();
            if (Session["CartCounter"] != null)
            {
                listShoppingCarts = Session["CartItem"] as List<ShoppingCartModel>;
            }
            if (listShoppingCarts.Any(model => model.Id == Id))
            {
                objShoppingCartModal = listShoppingCarts.Single(model => model.Id == Id);
                objShoppingCartModal.Quantity += +1;
                objShoppingCartModal.Total = objShoppingCartModal.Quantity * objShoppingCartModal.UnitPrice;
            }
            else
            {
                objShoppingCartModal.Id = Id;
                objShoppingCartModal.Images = objItem.Images;
                objShoppingCartModal.ItemName = objItem.productName;
                objShoppingCartModal.Quantity = 1;
                objShoppingCartModal.Total = objItem.productPrice;
                objShoppingCartModal.UnitPrice = objItem.productPrice;
                listShoppingCarts.Add(objShoppingCartModal);
            }
            Session["CartCounter"] = listShoppingCarts.Count;
            Session["CartItem"] = listShoppingCarts;
            return Json(new { Success = true, Counter = listShoppingCarts.Count }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ShoppingCart()
        {
            ShoppingCartModel objShoppingCartModal = new ShoppingCartModel();
            listShoppingCarts = Session["CartItem"] as List<ShoppingCartModel>;
            objShoppingCartModal.Total = objShoppingCartModal.Quantity * objShoppingCartModal.UnitPrice;
            ViewData["SumTotal"] = objShoppingCartModal.Total;

            return View(listShoppingCarts);
        }
        [HttpPost]
        public ActionResult AddOrder()
        {
            int OrderId = 0;
            listShoppingCarts = Session["CartItem"] as List<ShoppingCartModel>;
            Order order = new Order()
            {
                OrderDate = DateTime.Now,
                OrderNumber = String.Format("{0:ddmmyyyHHmmss}", DateTime.Now)
            };
            db.Orders.Add(order);
            db.SaveChanges();
            OrderId = order.OrderId;
            foreach (var item in listShoppingCarts)
            {
                OrderDetail orderDetails = new OrderDetail();
                //orderDetails.Total = item.Total;
                orderDetails.ProductId = item.Id;
                orderDetails.OrderId = OrderId;
                orderDetails.Quantity = item.Quantity;
                orderDetails.UnitPrice = item.UnitPrice;
                db.OrderDetails.Add(orderDetails);
                db.SaveChanges();
            }
            Session["CartCounter"] = null;
            Session["CartItem"] = null;
            return RedirectToAction("Index");
        }

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var products = db.Products.AsQueryable(); //đặt truy vấn khác nhau
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.productName.Contains(searchString.Trim()));

            }

            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(s => s.productName);
                    break;
                default:
                    products = products.OrderBy(s => s.productName);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(products.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult IndexAjax(string sortOrder, string currentFilter, string searchString, int? page)
        {

            var products = db.Products.AsQueryable(); //đặt truy vấn khác nhau
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.productName.Contains(searchString.Trim()));

            }
            return PartialView("IndexAjax", products.ToList());
        }

        public RedirectToRouteResult Edit(int Id, int Quantity)
        //{
        //    ShoppingCartModel objShoppingCartModal = new ShoppingCartModel();
        //    var objItem = db.Products.Where(s => s.Id == Id).FirstOrDefault();
        //    if (Session["CartCounter"] != null)
        //    {
        //        listShoppingCarts = Session["CartItem"] as List<ShoppingCartModel>;
        //    }
        //    if (listShoppingCarts.Any(model => model.Id == Id))
        //    {
        //        objShoppingCartModal = listShoppingCarts.Single(model => model.Id == Id);
        //        objShoppingCartModal.Quantity += Quantity;
        //        objShoppingCartModal.Total = objShoppingCartModal.Quantity * objShoppingCartModal.UnitPrice;
        //    }
        //    List<ShoppingCartModel> giohang = Session["CartItem"] as List<ShoppingCartModel>;
        //    ShoppingCartModel itemSua = giohang.FirstOrDefault(m => m.Id == Id);

        //    return RedirectToAction("ShoppingCart");
        {
            List<ShoppingCartModel> giohang = Session["CartItem"] as List<ShoppingCartModel>;
            ShoppingCartModel itemSua = giohang.FirstOrDefault(m => m.Id == Id);
            if (itemSua != null)
            {
                itemSua.Quantity = Quantity;
                itemSua.Total = itemSua.Quantity * itemSua.UnitPrice;
            }
            return RedirectToAction("ShoppingCart");

        } 
        public RedirectToRouteResult XoaKhoiGio(int Id)
        {
           
            List<ShoppingCartModel> giohang = Session["CartItem"] as List<ShoppingCartModel>;
            ShoppingCartModel itemXoa = giohang.FirstOrDefault(m => m.Id == Id);
            if (itemXoa != null)
            {
                giohang.Remove(itemXoa);
                Session["CartCounter"] = giohang.Count ;
            }
            
            return RedirectToAction("ShoppingCart");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}