using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using websitesach.Models;
using PagedList;
using PagedList.Mvc;
using System.Threading.Tasks;

namespace websitesach.Controllers
{
    public class TimKiemController : Controller
    {
        // GET: TimKiem
        nhasachEntities4 db = new nhasachEntities4();


        //[HttpGet]
        //public async Task<ActionResult> KetQuaTimKiem(int? page, string stukhoa)
        //{

        //    ViewBag.TuKhoa = " ";
        //    List<Sach> lstKQTK = db.Saches.ToList();
        //    int pageNumber = (page ?? 1);
        //    int pageSize = 8;
        //    return View(db.Saches.OrderBy(n => n.TenSach).ToPagedList(pageNumber, pageSize));
        //    if (lstKQTK.Count == 0)
        //    {
        //        ViewBag.ThongBao = "Không Tìm Thấy Sản Phẩm Nào!";
        //        return View(db.Saches.OrderBy(n => n.TenSach).ToPagedList(pageNumber, pageSize));
        //    }
        //    ViewBag.ThongBao = "Đã Tìm Thấy " + lstKQTK.Count + " Sản Phẩm";
        //    return View(db.Saches.OrderBy(n => n.TenSach).ToPagedList(pageNumber, pageSize));
        //    return View(lstKQTK.OrderBy(n => n.TenSach).ToPagedList(pageNumber, pageSize));
        //}
        //[HttpPost]
        //public ActionResult KetQuaTimKiem(FormCollection f, string txttimkiem, int? page)
        //{

        //    //string stukhoa = f["txttimkiem"].ToString();
        //    ViewBag.TuKhoa = txttimkiem;
        //    List<Sach> lstKQTK = db.Saches.Where(n => n.TenSach.Contains(txttimkiem)).ToList();
        //    int pageNumber = (page ?? 1);
        //    int pageSize = 8;
        //    if (lstKQTK.Count == 0)
        //    {
        //        ViewBag.ThongBao = "Không Tìm Thấy Sản Phẩm Nào!";
        //        return View(db.Saches.OrderBy(n => n.TenSach).ToPagedList(pageNumber,pageSize));
        //     }
        //    ViewBag.ThongBao = "Đã Tìm Thấy " +lstKQTK.Count+" Sản Phẩm";
        //    return View(lstKQTK.OrderBy(n => n.TenSach).ToPagedList(pageNumber, pageSize));
        //}


        [HttpGet]
        public ActionResult KetQuaTimKiem(int? page, string stukhoa)
        {

            ViewBag.TuKhoa =stukhoa;
            List<Sach> lstKQTK = db.Saches.ToList();
            int pageNumber = (page ?? 1);
            int pageSize = 8;
            //return View(db.Saches.OrderBy(n => n.TenSach).ToPagedList(pageNumber, pageSize));
            if (lstKQTK.Count == 0)
            {
                ViewBag.ThongBao = "Không Tìm Thấy Sản Phẩm Nào!";
                return View(db.Saches.OrderBy(n => n.TenSach).ToPagedList(pageNumber, pageSize));
            }
            ViewBag.ThongBao = "Đã Tìm Thấy " + lstKQTK.Count + " Sản Phẩm";
            //return View(db.Saches.OrderBy(n => n.TenSach).ToPagedList(pageNumber, pageSize));
            return View(lstKQTK.OrderBy(n => n.TenSach).ToPagedList(pageNumber, pageSize));
        }
        [HttpPost]
        public ActionResult KetQuaTimKiem(FormCollection f, string txttimkiem, int? page)
        {

            string stukhoa = f["txttimkiem"].ToString();
            ViewBag.TuKhoa = stukhoa;
            List<Sach> lstKQTK = db.Saches.Where(n => n.TenSach.Contains(txttimkiem)).ToList();
            int pageNumber = (page ?? 1);
            int pageSize = 8;
            if (lstKQTK.Count == 0)
            {
                ViewBag.ThongBao = "Không Tìm Thấy Sản Phẩm Nào!";
                return View(db.Saches.OrderBy(n => n.TenSach).ToPagedList(pageNumber, pageSize));
            }
            ViewBag.ThongBao = "Đã Tìm Thấy " + lstKQTK.Count + " Sản Phẩm";
            return View(lstKQTK.OrderBy(n => n.TenSach).ToPagedList(pageNumber, pageSize));
        }

    }
}