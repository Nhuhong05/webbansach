using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using websitesach.Models;
namespace websitesach.Controllers
{
    public class SachController : Controller
    {
        // GET: Sach
        nhasachEntities4 db = new nhasachEntities4();
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult sachyeuthichpartial()
        {
            
            var listsachyeuthich = db.Saches.Include(x => x.TacGia).OrderByDescending(n => n.YeuThich).Take(4).ToList();
            return PartialView(listsachyeuthich);
           

        }
        public PartialViewResult sachmoipartial()
        {

            var listsachmoi = db.Saches.Include(x => x.TacGia).OrderByDescending(n => n.Moi == 1).Take(5).ToList();
            return PartialView(listsachmoi);


        }
        public PartialViewResult bestsellerpartial()
        {
            var listbestseller = db.Saches.Include(x => x.TacGia).OrderByDescending(n => n.SLdaban).FirstOrDefault();
            return PartialView(listbestseller);
        }
       
        public ViewResult xemchitiet(int MaSach)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            Sach sach1 = db.Saches.Where(x => x.MaSach == MaSach).Include(x => x.ChuDe).Include(x => x.NhaXuatBan).FirstOrDefault();
            if (sach == null)
            {
                //trả về trang báo lỗi 404
                Response.StatusCode = 404;
                return null;
            }
            return View(sach1);
        }
        public PartialViewResult sanphamtuongtu(int MaChuDe)
        {
            var listsanphamtuongtu = db.Saches
    .Where(x => x.MaChuDe == MaChuDe)
    .Take(5)
    .ToList();

            return PartialView(listsanphamtuongtu);

        }
        public PartialViewResult sliderpartial()
        {
            var slider = db.Saches.ToList();
            return PartialView(slider);
        }

        public ViewResult SachMoi()
        {
            return View(db.Saches.Where(n => n.Moi == 1).ToList());
        }
        public ViewResult SachYeuThich()
        {
            var listsachyeuthich = db.Saches.Include(x => x.TacGia).OrderByDescending(n => n.YeuThich).ToList();
          
            return View(listsachyeuthich);
        }
    }
}