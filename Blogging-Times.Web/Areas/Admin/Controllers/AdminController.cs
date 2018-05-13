using Blogging_Times.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blogging_Times.Web.Areas.Admin.Controllers
{
    [LoginRequired]
    public class AdminController : Controller
    {
        // GET: Admin/Admin
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Dashboard2()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Blank()
        {
            return View();
        }

        public ActionResult AccessDenied()
        {
            return View();
        }

        public ActionResult Logout()
        {
            LoggedUserInfo.UserLogOut();
            return Redirect("/home");
        }
    }
}