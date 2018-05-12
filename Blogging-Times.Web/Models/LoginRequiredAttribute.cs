using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blogging_Times.Web.Models
{
    public class LoginRequiredAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            var returnUrl = filterContext.HttpContext.Request.RawUrl;
            LoggedUserInfo loggedUser = LoggedUserInfo.GetLoggedUserInfo();

            if (loggedUser == null)
            {
                filterContext.Result = new RedirectResult(String.Concat("/login", "?ReturnUrl=", returnUrl));
            }
        }
    }
}