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
    public class AnalysisController : Controller
    {
        // GET: Admin/Analysis
        public ActionResult PostAnalysis()
        {
            return View();
        }
        public ActionResult GetChartImage()
        {
            var key = new Chart(width: 300, height: 300)
                .AddTitle("Employee Chart")
                .AddSeries(
                chartType: "pie",
                name: "Employee",
                xValue: new[] { "Peter", "Andrew", "Julie", "Dave" },
                yValues: new[] { "2", "7", "5", "3" });

            return File(key.ToWebImage().GetBytes(), "image/jpeg");
        }
    }
}