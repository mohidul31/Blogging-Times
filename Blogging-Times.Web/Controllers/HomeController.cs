﻿using Blogging_Times.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blogging_Times.Data;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;

namespace Blogging_Times.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(Guid? tag,Guid ? category,int? month,int? year,string search="")
        {
            ViewBag.Title = "Home Page";
            ViewBag.Search = search;

            var result = new HomeViewModel(tag,category,month,year,null, search);

            return View(result);
        }

        public ActionResult PostDetails(Guid? post)
        {
            ViewBag.Title = "Post Page";
            ViewBag.Search = null;

            var result = new HomeViewModel(null,null,null,null,post);

            return View(result);
        }
    }
}
