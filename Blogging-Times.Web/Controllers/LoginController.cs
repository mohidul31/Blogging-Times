using Blogging_Times.Data;
using Blogging_Times.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Blogging_Times.Web.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [AllowAnonymous]
        public ActionResult Index(string returnUrl)
        {
            LoggedUserInfo upsd = LoggedUserInfo.GetLoggedUserInfo();
            if (upsd != null)
            {
                return Redirect("/Admin/Posts");
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Index(LoginViewModel model, string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (!ModelState.IsValid)
            {
                TempData["message"] = "";
                TempData["alertType"] = "danger";
                return View(model);
            }

            bool result = new AccountLoginModel().CheckLogin(model.Username, model.Password, model.RememberMe);
            if (result)
            {
                Users user = new AccountLoginModel().GetUserByLogin(model.Username, model.Password, model.RememberMe);

                LoggedUserInfo loggedUser = new LoggedUserInfo();
                loggedUser.Username = user.Username;
                loggedUser.ID = user.ID;
                loggedUser.UserRoleEnum = user.UserRoleEnum.Value;
                loggedUser.PasswordHash = Crypto.SHA256(model.Password);

                LoggedUserInfo.AddLoggedUserInfo(loggedUser);

                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return Redirect("/Admin/Posts");


            }
            else
            {
                TempData["message"] = "Invalid login attempt";
                TempData["alertType"] = "danger";
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }
        }
    }
}