using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using websitesach.Models;

namespace websitesach.Controllers
{
    public class GioHangController : Controller
    {
        nhasachEntities4 db = new nhasachEntities4();

        // GET: GioHang
        public ActionResult GioHang(int MaKH)
        {
            // Lấy danh sách mục trong giỏ hàng từ cơ sở dữ liệu
            //var cartItems = db.Carts.ToList();

            var cartItems = db.Carts.Where(c => c.MaKH == MaKH).ToList();

            // Tính tổng số lượng và tổng tiền
            int totalQuantity = cartItems.Sum(c => c.SoLuong ?? 0);
            decimal totalPrice = cartItems.Sum(c => c.ThanhTien ?? 0);

            // Truyền dữ liệu đến view
            ViewBag.TotalQuantity = totalQuantity;
            ViewBag.TotalPrice = totalPrice;

            return View(cartItems);
        }
        public ActionResult GioHangpartial(int MaKH)
        {
            var cartItems = db.Carts.Where(c => c.MaKH == 4).ToList();



            return View(cartItems);
        }

        // Thêm sản phẩm vào giỏ hàng

        public ActionResult Themgiohang(int masp, decimal giaban, string strURL)
        {
            //var cartItem = db.Carts.Where(c => c.MaKH == MaKH).SingleOrDefault(c => c.MaSach == maSach);
            var cartItem = db.Carts.SingleOrDefault(n => n.MaSach == masp);


            if (cartItem != null)
            {
                // Nếu sản phẩm đã tồn tại trong giỏ hàng, chỉ cập nhật số lượng
                cartItem.SoLuong++;
            }
            else
            {
                KhachHang x = Session["User"] as KhachHang;
                // Nếu sản phẩm chưa tồn tại trong giỏ hàng, tạo một mục mới
                var newCartItem = new Cart
                {
                    MaSach = masp,
                    MaKH = x.MaKH,
                    SoLuong = 1,
                    ThanhTien = giaban
                    // Thêm các trường khác tùy thuộc vào cấu trúc của bảng giỏ hàng
                };
                db.Carts.Add(newCartItem);
            }

            db.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
            ViewBag.ThongBao = " Thêm Vào Gior Hàng Thành Công!";
            return RedirectToAction("Index", "Home");
        }
        // Cập nhật số lượng của một sản phẩm trong giỏ hàng

        public ActionResult Suagiohang(int masp, FormCollection f)
        {
            //KhachHang x = Session["User"] as KhachHang;
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == masp);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            Cart sp = db.Carts.SingleOrDefault(n => n.MaSach == masp);
            if (sp != null)
            {
                sp.SoLuong = int.Parse(f["txtsl"].ToString());

            }
            return View("GioHang");
        }

        // Xóa một sản phẩm khỏi giỏ hàng
        public ActionResult Xoagiohang(int masp)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == masp);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            Cart cartItem = db.Carts.SingleOrDefault(c => c.MaSach == masp);

            if (cartItem != null)
            {
                db.Carts.Remove(cartItem);
                db.SaveChanges();
            }


            return RedirectToAction("Index");
        }
        public ActionResult Dathang(int masp, FormCollection f)
        {
            string chon = f["chon"].ToString();
            DonHang dh = db.DonHangs.SingleOrDefault(n => n.MaSach== masp);
            if (dh == null)
            {
                ViewBag.ThongBaogh = "Sản phẩm không tồn tại!";
            }
            else if (string.IsNullOrEmpty(chon))
            {
                ViewBag.ThongBaogh = "Chưa có sản phẩm được chọn!";
            }
            else
            {
                dh.SoLuong = int.Parse(chon);
                // Các cập nhật thông tin khác của đơn hàng

                // Cập nhật cơ sở dữ liệu
                db.SaveChanges();

                // Thông báo đặt hàng thành công (có thể thay đổi theo nhu cầu của bạn)
                ViewBag.ThongBaogh = "Đặt hàng thành công!";
            }
            return View();
        }
    }
}
