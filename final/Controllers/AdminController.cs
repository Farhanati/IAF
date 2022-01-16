using final.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace final.Controllers
{
    public class AdminController : Controller
    {
        projectEntities db = new projectEntities();
        // GET: Admin
        public ActionResult Index()
        {
            if(Session["Admin"] != null) { 
                Session["ucount"] = db.user_info.Count();
                Session["pcount"] = db.expoes.Count();
                Session["rcount"] = db.reviews.Count();
                Session["Acount"] = db.admins.Count();
                return View();
            }
            else { 
                return RedirectToAction("Login2","Register");
            }
        }
        public ActionResult List()
        {
            return View(db.user_info.ToList());
        }
        public ActionResult Review()
        {
            return View(db.reviews.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(user_info std, HttpPostedFileBase file)
        {
            
            string pth = Server.MapPath(Path.Combine("~/image/" + file.FileName));
           file.SaveAs(pth);
            std.user_img = file.FileName;
            db.user_info.Add(std);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            var std = db.user_info.Where(x => x.user_id == id).FirstOrDefault();
            Session["image"] = std.user_img;
            return View(std);
        }
        [HttpPost]
        public ActionResult Edit(user_info std, HttpPostedFileBase file)
        {

            if (file == null)
            {
                std.user_img = Session["image"].ToString();
                db.Entry(std).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                string path = Server.MapPath(Path.Combine("~/image/" + file.FileName));
                file.SaveAs(path);
                std.user_img = file.FileName;
                db.Entry(std).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            var std = db.user_info.Where(x => x.user_id == id).FirstOrDefault();
            db.user_info.Remove(std);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        /*    expo pictures   */

        public ActionResult Showproduct()
        {
            return View(db.expoes.ToList());
        }

        public ActionResult Addproduct()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Addproduct(expo ex, HttpPostedFileBase file)
        {

           string path = Server.MapPath(Path.Combine("~/image/" + file.FileName));
            file.SaveAs(path);
            ex.exp_img = file.FileName;
            db.expoes.Add(ex);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit_product(int id)
        {
            var st = db.expoes.Where(x => x.exp_id == id).FirstOrDefault();
            Session["image"] = st.exp_img;
            return View(st);
        }
        [HttpPost]
        public ActionResult Edit_product(expo st, HttpPostedFileBase file)
        {

            if (file == null)
            {
                st.exp_img = Session["image"].ToString();
                db.Entry(st).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                string path = Server.MapPath(Path.Combine("~/image/" + file.FileName));
                file.SaveAs(path);
                st.exp_img = file.FileName;
                db.Entry(st).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        public ActionResult Delete_product(int id)
        {
            var st = db.expoes.Where(x => x.exp_id == id).FirstOrDefault();
            db.expoes.Remove(st);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}