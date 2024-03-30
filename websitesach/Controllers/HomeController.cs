using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using websitesach.Models;
using PagedList.Mvc;
using PagedList;

namespace websitesach.Controllers

{
    public class HomeController : Controller
    {
        // GET: Home
        nhasachEntities4 db = new nhasachEntities4();
        public ActionResult Index(int? page)
        {
            // tạo biến số sản phẩm trên trang
            int pageSize = 16;
            int pageNumber = (page ?? 1);
            return View(db.Saches.ToList().OrderBy(n => n.YeuThich).ToPagedList(pageNumber, pageSize));
            //return View(db.Saches.Where(n => n.Moi == 1).ToList());
        }


    }
}