using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using websitesach.Models;
namespace websitesach.Controllers
{
    public class ChudeController : Controller
    {
        // GET: Chude
        nhasachEntities4 db = new nhasachEntities4();
        public ActionResult ChuDepartial()
        {
            
            return PartialView(db.ChuDes.ToList());
        }
        public ActionResult chudesachpartial()
        {

            return PartialView(db.ChuDes.Take(6).ToList());
        }
        public  ViewResult sachtheochude(int MaChuDe)
        {
            //kiểm tra chủ đề tồn tại hay không
            ChuDe cd = db.ChuDes.SingleOrDefault(n => n.MaChuDe == MaChuDe);
            if(cd== null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //truy xuất danh sách các quyển sách theo chủ đề

            List<Sach> listsach = db.Saches.Where(n=>n.MaChuDe==MaChuDe).OrderBy(n=>n.GiaBan).ToList();
            if (listsach.Count == 0)
            {
                ViewBag.Sach = "không có sách nào thuộc thể loại này";
            }
            return View(listsach);
        }
       
    }
}