using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using websitesach.Models;
using PagedList;
using PagedList.Mvc;
using System.Web.UI;
using System.IO;
using Microsoft.Win32.SafeHandles;
using System.Text.RegularExpressions;
using System.Data.Entity;

namespace websitesach.Controllers
{

    public class QuanliSPController : Controller
    {
        nhasachEntities4 db = new nhasachEntities4();
        // GET: QuanliSP
        
        public ActionResult Index(int? page)
        {
            
            int pagenumber = (page ?? 1);
            int pagesize = 10;

            return View(db.DonHangs.OrderBy(n => n.MaDonHang).ToPagedList(pagenumber, pagesize));
          
        }
        // CHỦ ĐỀ

        public ActionResult ChuDe()
        {
            return View(db.ChuDes.ToList());
        }
        [HttpGet]
        public ActionResult ThemMoi()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemMoi(ChuDe cd)
        {
            if (ModelState.IsValid) // Kiểm tra tính hợp lệ của dữ liệu đầu vào.
            {
              
                    db.ChuDes.Add(cd);
                    db.SaveChanges();
                
               
                    return RedirectToAction("ChuDe"); // Chuyển hướng đến action "ChuDe" sau khi thêm mới thành công.
                
            }
            return View(cd); // Nếu dữ liệu không hợp lệ, hiển thị lại trang với lỗi.


        }
        // sửa chủ đề
        [HttpGet]
        public ActionResult SuaChuDe(int MaChuDe)
        {
            ChuDe cd = db.ChuDes.SingleOrDefault(n => n.MaChuDe== MaChuDe);
            if (cd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(cd);
        }
        [HttpPost]
        public ActionResult SuaChuDe(ChuDe cd)
        {
            if (ModelState.IsValid)
            {
                // cập nhật trong model
                db.Entry(cd).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ChuDe");
            }
            return View(cd);
        }
        // xóa chủ đề
        [HttpGet]
        public ActionResult XoaChuDe(int MaChuDe)
        {
            ChuDe cd = db.ChuDes.SingleOrDefault(n => n.MaChuDe == MaChuDe);
            if (cd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(cd);
        }
        [HttpPost]
        public ActionResult XacNhanXoaChuDe(int MaChuDe)
        {
            ChuDe cd = db.ChuDes.SingleOrDefault(n => n.MaChuDe == MaChuDe);
            if (cd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.ChuDes.Remove(cd);
            db.SaveChanges();
            ViewBag.ThongBao = "Xóa Thành Công!";
            return RedirectToAction("ChuDe");
        }

            // SÁCH

            public ActionResult Sach(int? page)
        {
            
            int pagenumber = (page ?? 1);
            int pagesize = 10;
            
            return View(db.Saches.OrderBy(n => n.MaSach).ToPagedList(pagenumber, pagesize));
        }
        [HttpGet]
       
        public ActionResult ThemMoiSach()
        {
            //đưa dữ liệu vào dropdowlist
            ViewBag.MaChuDe = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaChuDe", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            ViewBag.MaTacGia = new SelectList(db.TacGias.ToList().OrderBy(n => n.TenTacGia), "MaTacGia", "TenTacGia");


            return View();
        }
        [HttpPost]
        public ActionResult ThemMoiSach(Sach sach, HttpPostedFileBase fileupload)

        {
            //đưa dữ liệu vào dropdowlist
            ViewBag.MaChuDe = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaChuDe", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            ViewBag.MaTacGia = new SelectList(db.TacGias.ToList().OrderBy(n => n.TenTacGia), "MaTacGia", "TenTacGia");
            // thêm vào csdl
            if(fileupload== null)
            {
                ViewBag.ThongBao = "Chọn Hình Ảnh";
                return View();
            }
            if (ModelState.IsValid)
            {
                //lưu tên file
                var filename = Path.GetFileName(fileupload.FileName);
                //lưu đg dẫn của file
                var path = Path.Combine(Server.MapPath("~/Hinhanhsanpham"), filename);
                if (System.IO.File.Exists(path))
                {
                    ViewBag.ThongBao = "hình ảnh đã tồn tại";
                }
                else
                {
                    fileupload.SaveAs(path);
                }
                sach.AnhBia=fileupload.FileName;
                db.Saches.Add(sach);
                db.SaveChanges();
                return RedirectToAction("Sach");
            }
           

            return View();
        }
        // sửa sách
        [HttpGet]
        public ActionResult SuaSach(int MaSach)
        {
            ViewBag.MaChuDe = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaChuDe", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            ViewBag.MaTacGia = new SelectList(db.TacGias.ToList().OrderBy(n => n.TenTacGia), "MaTacGia", "TenTacGia");
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            if(sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }
        [HttpPost]
        
        public ActionResult SuaSach(Sach sach)
        {

            // thêm vào csdl
            if (ModelState.IsValid)
            {
                // cập nhật trong model
                db.Entry(sach).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Sach");
            }
            // đưa dữ liệu vào drop
            ViewBag.MaChuDe = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaChuDe", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            ViewBag.MaTacGia = new SelectList(db.TacGias.ToList().OrderBy(n => n.TenTacGia), "MaTacGia", "TenTacGia");
            return View();
        }
        // xóa sách
        [HttpGet]
        public ActionResult XoaSach(int MaSach)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }
        [HttpPost]
        public ActionResult XacNhanXoaSach(int MaSach)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.Saches.Remove(sach);
            db.SaveChanges();
            ViewBag.ThongBao="Xóa Thành Công!";
            return RedirectToAction("Sach");
        }

            // khách hàng
            public ActionResult KhachHang()
        {
            return View(db.KhachHangs.ToList());
        }

        // thêm sửa xóa
        [HttpGet]
        public ActionResult ThemMoiKH()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemMoiKH(KhachHang kh)
        {
            if (ModelState.IsValid) // Kiểm tra tính hợp lệ của dữ liệu đầu vào.
            {

                db.KhachHangs.Add(kh);
                db.SaveChanges();


                return RedirectToAction("KhachHang"); // Chuyển hướng đến action "ChuDe" sau khi thêm mới thành công.

            }
            return View(kh); // Nếu dữ liệu không hợp lệ, hiển thị lại trang với lỗi.


        }
        // sửa chủ đề
        [HttpGet]
        public ActionResult SuaKH(int MaKH)
        {
            KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.MaKH == MaKH);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(kh);
        }
        [HttpPost]
        public ActionResult SuaKH(KhachHang kh)
        {
            if (ModelState.IsValid)
            {
                // cập nhật trong model
                db.Entry(kh).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("KhachHang");
            }
            return View(kh);
        }
        // xóa chủ đề
        [HttpGet]
        public ActionResult XoaKH(int MaKH)
        {
            KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.MaKH == MaKH);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(kh);
        }
        [HttpPost]
        public ActionResult XacNhanXoaKH(int MaKH)
        {
            KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.MaKH == MaKH);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.KhachHangs.Remove(kh);
            db.SaveChanges();
            ViewBag.ThongBao = "Xóa Thành Công!";
            return RedirectToAction("KhachHang");
        }

        // TÁC giả
        public ActionResult TacGia()
        {
            return View(db.TacGias.ToList());
        }
        // thêm sửa xóa
        [HttpGet]
        public ActionResult ThemMoiTG()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemMoiTG(TacGia tg)
        {
            if (ModelState.IsValid) // Kiểm tra tính hợp lệ của dữ liệu đầu vào.
            {

                db.TacGias.Add(tg);
                db.SaveChanges();


                return RedirectToAction("TacGia"); // Chuyển hướng đến action "ChuDe" sau khi thêm mới thành công.

            }
            return View(tg); // Nếu dữ liệu không hợp lệ, hiển thị lại trang với lỗi.


        }
        // sửa TG
        [HttpGet]
        public ActionResult SuaTG(int MaTacGia)
        {
            TacGia tg = db.TacGias.SingleOrDefault(n => n.MaTacGia == MaTacGia);

            if (tg == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(tg);
        }
        [HttpPost]
        public ActionResult SuaTG(TacGia tg)
        {
            if (ModelState.IsValid)
            {
                // cập nhật trong model
                db.Entry(tg).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("TacGia");
            }
            return View(tg);
        }
        // xóa TG
        [HttpGet]
        public ActionResult XoaTG(int MaTacGia)
        {
            TacGia tg = db.TacGias.SingleOrDefault(n => n.MaTacGia == MaTacGia);

            if (tg == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(tg);
        }
        [HttpPost]
        public ActionResult XacNhanXoaTG(int MaTacGia)
        {
            TacGia tg = db.TacGias.SingleOrDefault(n => n.MaTacGia == MaTacGia);
            if (tg == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.TacGias.Remove(tg);
            db.SaveChanges();
            ViewBag.ThongBao = "Xóa Thành Công!";
            return RedirectToAction("TacGia");
        }
        //Đơn Hàng
        public ActionResult DonHang(int? page)
        {

            int pagenumber = (page ?? 1);
            int pagesize = 10;

            return View(db.DonHangs.OrderBy(n => n.MaDonHang).ToPagedList(pagenumber, pagesize));
            
        }

        // thêm sửa xóa
        [HttpGet]
        public ActionResult ThemMoiDH()
        {
            ViewBag.MaKH = new SelectList(db.KhachHangs.ToList().OrderBy(n => n.MaKH), "MaKH", "TenTaiKhoan");
            ViewBag.MaSach = new SelectList(db.Saches.ToList().OrderBy(n => n.MaSach), "MaSach", "TenSach");

            return View();
        }
        [HttpPost]
        public ActionResult ThemMoiDH(DonHang dh)
        {
            ViewBag.MaKH = new SelectList(db.KhachHangs.ToList().OrderBy(n => n.MaKH), "MaKH", "TenTaiKhoan");
            ViewBag.MaSach = new SelectList(db.Saches.ToList().OrderBy(n => n.MaSach), "MaSach", "TenSach");
            if (ModelState.IsValid) // Kiểm tra tính hợp lệ của dữ liệu đầu vào.
            {

                db.DonHangs.Add(dh);
                db.SaveChanges();


                return RedirectToAction("DonHang"); // Chuyển hướng đến action "ChuDe" sau khi thêm mới thành công.

            }
            return View(dh); // Nếu dữ liệu không hợp lệ, hiển thị lại trang với lỗi.


        }
        // sửa DH
        [HttpGet]
        public ActionResult SuaDH(int MaDonHang)
        {
            ViewBag.MaKH = new SelectList(db.KhachHangs.ToList().OrderBy(n => n.MaKH), "MaKH", "TenTaiKhoan");
            ViewBag.MaSach = new SelectList(db.Saches.ToList().OrderBy(n => n.MaSach), "MaSach", "TenSach");

            DonHang dh= db.DonHangs.SingleOrDefault(n => n.MaDonHang == MaDonHang);

            if (dh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(dh);
        }
        [HttpPost]
        public ActionResult SuaDH(DonHang dh)
        {
            if (ModelState.IsValid)
            {
                // cập nhật trong model
                db.Entry(dh).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DonHang");
            }

            ViewBag.MaKH = new SelectList(db.KhachHangs.ToList().OrderBy(n => n.MaKH), "MaKH", "TenTaiKhoan");
            ViewBag.MaSach = new SelectList(db.Saches.ToList().OrderBy(n => n.MaSach), "MaSach", "TenSach");
            return View(dh);
        }

        // xóa DH
        [HttpGet]
        public ActionResult XoaDH(int MaDonHang)
        {
            DonHang dh = db.DonHangs.SingleOrDefault(n => n.MaDonHang == MaDonHang);

            if (dh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(dh);
        }
        [HttpPost]
        public ActionResult XacNhanXoaDH(int MaDonHang)
        {
            DonHang dh = db.DonHangs.SingleOrDefault(n => n.MaDonHang == MaDonHang);
            if (dh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.DonHangs.Remove(dh);
            db.SaveChanges();
            ViewBag.ThongBao = "Xóa Thành Công!";
            return RedirectToAction("DonHang");
        }

     
      

        //Nhà Xuất Bản
        public ActionResult NhaXuatBan(int? page)
        {
            int pagenumber = (page ?? 1);
            int pagesize = 10;

            return View(db.NhaXuatBans.OrderBy(n => n.MaNXB).ToPagedList(pagenumber, pagesize));
          
        }

        // thêm sửa xóa
        [HttpGet]
        public ActionResult ThemMoiNXB()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemMoiNXB(NhaXuatBan nxb)
        {
            if (ModelState.IsValid) // Kiểm tra tính hợp lệ của dữ liệu đầu vào.
            {

                db.NhaXuatBans.Add(nxb);
                db.SaveChanges();


                return RedirectToAction("NhaXuatBan"); // Chuyển hướng đến action "ChuDe" sau khi thêm mới thành công.

            }
            return View(nxb); // Nếu dữ liệu không hợp lệ, hiển thị lại trang với lỗi.


        }
        // sửa TG
        [HttpGet]
        public ActionResult SuaNXB(int MaNXB)
        {
            NhaXuatBan nxb = db.NhaXuatBans.SingleOrDefault(n => n.MaNXB == MaNXB);

            if (nxb == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(nxb);
        }
        [HttpPost]
        public ActionResult SuaNXB(NhaXuatBan nxb)
        {
            if (ModelState.IsValid)
            {
                // cập nhật trong model
                db.Entry(nxb).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("NhaXuatBan");
            }
            return View(nxb);
        }
        // xóa TG
        [HttpGet]
        public ActionResult XoaNXB(int MaNXB)
        {
            NhaXuatBan nxb = db.NhaXuatBans.SingleOrDefault(n => n.MaNXB == MaNXB);


            if (nxb == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(nxb);
        }
        [HttpPost]
        public ActionResult XacNhanXoaNXB(int MaNXB)
        {
            
            NhaXuatBan nxb = db.NhaXuatBans.SingleOrDefault(n => n.MaNXB == MaNXB);

            if (nxb == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.NhaXuatBans.Remove(nxb);
            db.SaveChanges();
            ViewBag.ThongBao = "Xóa Thành Công!";
            return RedirectToAction("NhaXuatBan");
        }
        // thông báo
     

    }
}