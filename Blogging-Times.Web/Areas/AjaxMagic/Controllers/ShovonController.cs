using Blogging_Times.Data;
using Blogging_Times.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blogging_Times.Web.Areas.AjaxMagic.Controllers
{
    public class ShovonController : Controller
    {
        private PostDbContext db = new PostDbContext();
        // GET: AjaxMagic/Shovon
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AutoComplete()
        {
            return View();
        }

        public JsonResult AutoCompleteSearch_result()
        {
           //To Avoid--> A circular reference was detected while serializing an object of type
            db.Configuration.ProxyCreationEnabled = false;

            string keyward = Request["term"];
            var result = db.PostCategory.Where(x => x.CategoryName.ToLower().Contains(keyward.ToLower())).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult OnSelectAppendData()
        {
            ViewBag.SelectPostCategory = new SelectList(db.PostCategory, "ID", "CategoryName");
            return View();
        }

        public ActionResult GetPostListByPostCategory()
        {
            db.Configuration.ProxyCreationEnabled = false;

            string selected_id = Request["selected_id"];
            Guid selected_id_guid = new Guid(selected_id);
            var result = db.Post.Where(x => x.PostCategory.ID== selected_id_guid).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult OnChangeShowData()
        {
            ViewBag.SelectPost = new SelectList(db.Post, "ID", "PostTitle");
            return View();
        }
        public JsonResult GetPostListByID()
        {
            db.Configuration.ProxyCreationEnabled = false;

            string selected_id = Request["selected_id"];
            Guid selected_id_guid = new Guid(selected_id);

            var result = db.Post.Where(x => x.ID == selected_id_guid).SingleOrDefault();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult OnClickLoadData()
        {
            var result = db.Post.OrderBy(x => x.PostTitle).Take(1).Skip(0).ToList();
            return View(result);
        }

        public JsonResult LoadMoreData()
        {
            int page =Convert.ToInt32(Request["page"]) ;
		    int limit= 100;
            int offset = 0;
            IQueryable<Post> result = null;
            if (page!=0)
            {
                offset = ((page - 1)* limit);
                 result = db.Post.OrderBy(x => x.PostTitle).Take(limit).Skip(offset);
                 var resultView = View("~/AjaxMagic/Views/Shared/_PostList", result);
                return Json(resultView, JsonRequestBehavior.AllowGet);
            }
            else
            {
                result = db.Post.OrderBy(x => x.PostTitle).Take(limit).Skip(offset);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

    }
}