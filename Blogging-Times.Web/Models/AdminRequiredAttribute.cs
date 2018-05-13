using Blogging_Times.Data;
using Blogging_Times.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blogging_Times.Web.Models
{
    public class AdminRequiredAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            var returnUrl = filterContext.HttpContext.Request.RawUrl;
            LoggedUserInfo loggedUser = LoggedUserInfo.GetLoggedUserInfo();

            if (loggedUser == null)
            {
                //if logged user info not avaliable
                filterContext.Result = new RedirectResult(String.Concat("/login", "?ReturnUrl=", returnUrl));
            }
            else
            {
                //if logged user info avaliable
                PostDbContext db = new PostDbContext();
                string username = loggedUser.Username;
                string passwordHash = loggedUser.PasswordHash;

                int count = db.Users.Where(x => x.Username == username && x.Password == passwordHash).Count();
                if (count != 1)
                {
                    //if logged user password not match with orginal password
                    HttpContext.Current.Session.Abandon();
                    filterContext.Result = new RedirectResult(String.Concat("/login", "?ReturnUrl=", returnUrl));
                }
                else
                {
                    //if logged user password match with orginal password-
                    if (loggedUser.UserRoleEnum != UserRoleEnum.Admin)
                    {
                        //if logged user role is not Admin
                        filterContext.Result = new RedirectResult(String.Concat("/Admin/Admin/AccessDenied"));
                    }
                }
                
            }


        }
    }
}