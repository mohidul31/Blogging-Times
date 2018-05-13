using Blogging_Times.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blogging_Times.Web.Models
{
    public class LoggedUserInfo
    {
        public string Username { get; set; }
        public Guid? ID { get; set; }
        public UserRoleEnum UserRoleEnum { get; set; }
        public string PasswordHash { get; set; }

        public static void AddLoggedUserInfo(LoggedUserInfo item)
        {
            HttpContext.Current.Session["loggedUser"] = item;
        }

        public static LoggedUserInfo GetLoggedUserInfo()
        {
            var info = (LoggedUserInfo)HttpContext.Current.Session["loggedUser"];
            return info;
        }

        public static void UserLogOut()
        {
            //Session.Abandon();
            HttpContext.Current.Session["loggedUser"] = null;
        }
    }}