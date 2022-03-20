using Assignment_NET.Data;
using Assignment_NET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment_NET.Controllers
{
    public class BagCartController : Controller
    {
        private ShopDataContext db = new ShopDataContext();
        // GET: BagCart
        public ActionResult Index()
        {
            List<OrderDetail> giohang = Session["giohang"] as List<OrderDetail>;
            return View(giohang);
        }
        public RedirectToRouteResult ThemVaoGio(int ProductId)
        {
            if (Session["giohang"] == null) // Nếu giỏ hàng chưa được khởi tạo
            {
                Session["giohang"] = new List<OrderDetail>();  // Khởi tạo Session["giohang"] là 1 List<OrderDetail>
            }

            List<OrderDetail> giohang = Session["giohang"] as List<OrderDetail>;  // Gán qua biến giohang dễ code

            // Kiểm tra xem sản phẩm khách đang chọn đã có trong giỏ hàng chưa

            if (giohang.FirstOrDefault(m => m.ProductId == ProductId) == null) // ko co sp nay trong gio hang
            {
                Product sp = db.Products.Find(ProductId);  // tim sp theo sanPhamID

                OrderDetail newItem = new OrderDetail()
                {
                    ProductId = ProductId,
                    TenSanPham = sp.productName,
                    Quantity = 1,
                    Images = sp.Images,
                    UnitPrice = sp.productPrice

                };  // Tạo ra 1 OrderDetail mới

                giohang.Add(newItem);  // Thêm OrderDetail vào giỏ 
            }
            else
            {
                // Nếu sản phẩm khách chọn đã có trong giỏ hàng thì không thêm vào giỏ nữa mà tăng số lượng lên.
                OrderDetail cardItem = giohang.FirstOrDefault(m => m.ProductId == ProductId);
                cardItem.Quantity++;
            }

            // Action này sẽ chuyển hướng về trang chi tiết sp khi khách hàng đặt vào giỏ thành công. Bạn có thể chuyển về chính trang khách hàng vừa đứng bằng lệnh return Redirect(Request.UrlReferrer.ToString()); nếu muốn.
            return RedirectToAction("ChiTiet", "SanPham", new { id = ProductId });
        }

        public RedirectToRouteResult SuaSoLuong(int ProductId, int soluongmoi)
        {
            // tìm carditem muon sua
            List<OrderDetail> giohang = Session["giohang"] as List<OrderDetail>;
            OrderDetail itemSua = giohang.FirstOrDefault(m => m.ProductId == ProductId);
            if (itemSua != null)
            {
                itemSua.Quantity = soluongmoi;
            }
            return RedirectToAction("~/Home/Index");

        }
        public RedirectToRouteResult XoaKhoiGio(int ProductId)
        {
            List<OrderDetail> giohang = Session["giohang"] as List<OrderDetail>;
            OrderDetail itemXoa = giohang.FirstOrDefault(m => m.ProductId == ProductId);
            if (itemXoa != null)
            {
                giohang.Remove(itemXoa);
            }
            return RedirectToAction("~/Home/Index");
        }

    }
}