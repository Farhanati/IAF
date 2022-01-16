using final.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace final.Controllers
{
    public class HomeController : Controller
    {
        projectEntities db = new projectEntities();

        // GET: Home
        public ActionResult Index()
        {
            ViewBag.data = (from s in db.expoes select s).Take(3);
            ViewBag.one = (from f in db.expoes select f).Take(1);
            ViewBag.first = (from b in db.expoes select b).Take(4);
            ViewBag.gallery = (from a in db.expoes select a).Take(12);
            ViewBag.Staff = db.user_info.Where(x => x.user_category == "teacher").Take(4);
            var obj = db.user_info.Where(x => x.user_category == "manager").FirstOrDefault();
            Session["Manager"] = obj.user_name;
            Session["Image"] = obj.user_img;
            return View(db.expoes.ToList());
        }
        public ActionResult Gallery()
        {
            return View(db.expoes.ToList());
        }
        public ActionResult Exihibition()
        {
            ViewBag.one = (from f in db.expoes select f).Take(1);
            ViewBag.second = (from c in db.expoes select c).Take(6);
            return View(db.expoes.ToList());
        }
        public ActionResult Detail(int id)
        {
            ViewBag.ne = (from a in db.expoes select a).Take(6);
            var data = db.expoes.Where(x => x.exp_id== id).FirstOrDefault();
            Session["img "] = data.exp_img.ToString();
            if (Session["Signin"] != null)
            {

                return View(data);
            }
            return RedirectToAction("Login", "Register");
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Collection()
        {
            ViewBag.col = (from a in db.expoes select a).Take(4);
            ViewBag.one = (from e in db.expoes select e).Take(1);
            return View(db.expoes.ToList());
        }
        public ActionResult Review()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Review(review rev)
        {
                rev.r_picture = Session["img "].ToString();
                rev.r_username = Session["Signin"].ToString();
                db.reviews.Add(rev);
                db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ReviewPage()
        {
            if (Session["Signin"] != null)
            {

                return View(db.reviews.ToList());
            }
            return RedirectToAction("Login", "Register");
        }
        public ActionResult Addproduct()
        {
            if (Session["Signin"] != null)
            {

                return View();
            }
            return RedirectToAction("Login", "Register");
        }

        [HttpPost]
        public ActionResult Addproduct(expo ex, HttpPostedFileBase file)
        {

            string path = Server.MapPath(Path.Combine("~/image/" + file.FileName));
            file.SaveAs(path);
            ex.exp_img = file.FileName;
            db.expoes.Add(ex);
            db.SaveChanges();
            return RedirectToAction("Index","Home");
        }
    }
}
