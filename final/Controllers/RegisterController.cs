using final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace final.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        projectEntities db = new projectEntities();
        // GET: Register
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(string username, string your_email, string password)
        {
            user_info log = new user_info();
            log.user_name = username;
            log.user_email = your_email;
            log.user_password = password;
            //log.user_id = ID;
            db.user_info.Add(log);
            db.SaveChanges();

            return RedirectToAction("login");
        }
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            var obj = db.user_info.Where(x => x.user_email.Equals(email) && x.user_password.Equals(password)).FirstOrDefault();
            if (obj.user_email == email && obj.user_password == password)
            {
                Session["Signin"] = obj.user_name;
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session["Signin"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Login2()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login2(string email, string password)
        {
            var obj = db.admins.Where(x => x.ad_email.Equals(email) && x.ad_password.Equals(password)).FirstOrDefault();
            if (obj.ad_email == email && obj.ad_password == password)
            {
                Session["Admin"] = obj.ad_name;
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }
        //public ActionResult Logout_User()
        //{
        //    //FormsAuthentication.SignOut();


        //    return RedirectToAction("Index", "Home");
        //}
    }
}