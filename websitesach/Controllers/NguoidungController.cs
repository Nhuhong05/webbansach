using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using websitesach.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Web.Helpers;
using System.Data.Entity;

namespace websitesach.Controllers
{
    public class NguoidungController : Controller
    {
        nhasachEntities4 db = new nhasachEntities4();
        // GET: Nguoidung
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dangky()
        {
            return View();
        }
        [HttpPost]
        
        public ActionResult Dangky(KhachHang kh, string txttentk, string txtemail, string txtMK)
        {

            // chèn dữ kiệu vào bảng kh
            kh.TenTaiKhoan = txttentk;
            kh.Email = txtemail;
            kh.MatKhau = txtMK;
            db.KhachHangs.Add(kh);
            // lưu vào csdl
            db.SaveChanges();
            ViewBag.ThongBao = "Đăng Kí Thành công!";
            //return RedirectToAction("Dangnhap");

            return RedirectToAction("Index","Home");
        }

        public ActionResult Dangnhap()
        {

            return View();
        }



        [HttpPost]

        public ActionResult Dangnhap(KhachHang kh, string txttentk, string txtmk)
        {

            var staikhoan = kh.TenTaiKhoan;
            var smatkhau = kh.MatKhau;
            var ad =db.TaiKhoans.Where(x => x.TenTK == txttentk && x.MatKhau == txtmk).FirstOrDefault();
           
            var checkkh = db.KhachHangs.Where(x => x.TenTaiKhoan == txttentk && x.MatKhau == txtmk).FirstOrDefault();
            if (checkkh != null)
            {
                ViewBag.ThongBao = "Đăng Nhập Thành công";
                Session["User"] = checkkh;
                return RedirectToAction("Index", "Home");
            }
            if (ad != null)
            {
                return RedirectToAction("Index", "QuanliSP");
            }
            else
            {
                ViewBag.ThongBao = "Tên Tài Khoản Hoặc Mật Khẩu Không Đúng";
                return View();

            }

        }


        public ActionResult Dangxuat()
        {
            Session["User"] = null;
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult taikhoan(int MaKH)
        {
            KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.MaKH == MaKH);
            return View(kh);
        }
        [HttpPost]

        public ActionResult taikhoan(KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                // Cập nhật thông tin khách hàng trong cơ sở dữ liệu
                //db.Entry(khachHang).State = EntityState.Modified;
                db.Entry(khachHang).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                ViewBag.ThongBao = "Cập nhật thông tin thành công";
            }

            return View(khachHang);
        }
        public ActionResult Donhang(int MaDonHang)
        {
            //var dh = db.DonHangs.SingleOrDefault(n => n.MaDonHang == MaDonHang);
            var dh = db.DonHangs.Where(x => x.MaKH == MaDonHang).ToList();
            return View(dh);
        }
        public ActionResult Thongbao(int MaKH)
        {

            var tb = db.DonHangs.Where(x => x.MaKH == MaKH).ToList();
            return View(tb);
        }
        public PartialViewResult Thongbaopartial(int MaKH)
        {
            var tb = db.DonHangs.Where(x => x.MaKH == MaKH).ToList();
            return PartialView(tb);
        }

    }
}

