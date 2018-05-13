using Blogging_Times.Data;
using Blogging_Times.Posts;
using Blogging_Times.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Blogging_Times.Web.Areas.Admin.Controllers
{
    [LoginRequired]
    public class ChangePasswordController : Controller
    {
        // GET: Admin/ChangePassword
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                PostDbContext db = new PostDbContext();
                string username = LoggedUserInfo.GetLoggedUserInfo().Username;
                string pp = LoggedUserInfo.GetLoggedUserInfo().PasswordHash;
                string passwoord_hash = Crypto.SHA256(model.PresentPassword);

                int res=db.Users.Where(x => x.Username == username && x.Password == passwoord_hash).Count();
                if (res==1)
                {
                    //update password
                    try
                    {
                        Users u = db.Users.Where(x => x.Username == username && x.Password == passwoord_hash).SingleOrDefault();
                        u.Password = Crypto.SHA256(model.ConfirmPassword);
                        db.SaveChanges();

                        TempData["message"] = "Successfully Changed Password. Login Again with new Password";
                        TempData["alertType"] = "success";

                        Session.Abandon();
                    }
                    catch (Exception)
                    {

                        TempData["message"] = "Failed To Change Password";
                        TempData["alertType"] = "danger";
                    }
                    
                }
                else
                {
                    TempData["message"] = "Incorrect Old password";
                    TempData["alertType"] = "info";
                }
            }
            return View(model);
        }
    }
}