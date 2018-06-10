using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Blogging_Times.Data;
using Blogging_Times.Posts;
using Blogging_Times.Web.Models;
using System.Linq.Expressions;

namespace Blogging_Times.Web.Areas.Admin.Controllers
{
    [AdminRequired]
    public class PostCategoriesController : Controller
    {
        private PostDbContext db = new PostDbContext();

        // GET: Admin/PostCategories
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetPostCategoryResult(DataTablesAjaxRequestModel datatableRequest)
        {
            // All Post Data
            string[] tableColumnmList = { null, "CategoryName", null, null };
            string searchValue = datatableRequest.GetSearchText();
            int serial = datatableRequest.GetSerialNoOfFirstRow();

            string search_by_name = Request["parameterName"];
            string search_by_postType = Request["parameterName2"];

            //PostCategory postCategory = new PostCategory();
            List<Expression<Func<PostCategory, bool>>> filterList = new List<Expression<Func<PostCategory, bool>>>();
            if (search_by_name != "")
            {
                Expression<Func<PostCategory, bool>> filter = null;
                filter = x => x.CategoryName.Contains(search_by_name);
                filterList.Add(filter);
            }
            if (search_by_postType == "1")
            {
                //Non
                Expression<Func<PostCategory, bool>> filter = null;
                filter = x => x.PostList.Count <= 0;
                filterList.Add(filter);
            }
            if (search_by_postType=="2")
            {
                //Posted
                Expression<Func<PostCategory, bool>> filter = null;
                filter = x => x.PostList.Count > 0;
                filterList.Add(filter);
            }

            IEnumerable<PostCategory> records = new Repository<PostCategory>(db).GetAjaxDatatablePagedDataList(datatableRequest,
                out int recordsTotal, out int recordsFiltered,tableColumnmList,filterList);

            var dataSet = (
                    from record in records
                    select new string[]
                    {
                        serial++.ToString(),
                        record.CategoryName.ToString(),
                        record.CreatedAt.ToString(),
                        record.ID.ToString(),
                    }
                );

            var jsonData = new
            {
                recordsTotal = recordsTotal,
                recordsFiltered = recordsFiltered,
                data = records
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/PostCategories/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostCategory postCategory = db.PostCategory.Find(id);
            if (postCategory == null)
            {
                return HttpNotFound();
            }
            return View(postCategory);
        }

        // GET: Admin/PostCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/PostCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostCategory postCategory)
        {
            if (ModelState.IsValid)
            {
                postCategory.CreatedBy = LoggedUserInfo.GetLoggedUserInfo().ID;
                db.PostCategory.Add(postCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(postCategory);
        }

        // GET: Admin/PostCategories/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostCategory postCategory = db.PostCategory.Find(id);
            if (postCategory == null)
            {
                return HttpNotFound();
            }
            return View(postCategory);
        }

        // POST: Admin/PostCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( PostCategory postCategory)
        {
            if (ModelState.IsValid)
            {
                postCategory.UpdatedBy = LoggedUserInfo.GetLoggedUserInfo().ID;
                db.Entry(postCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(postCategory);
        }

        // GET: Admin/PostCategories/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostCategory postCategory = db.PostCategory.Find(id);
            if (postCategory == null)
            {
                return HttpNotFound();
            }
            return View(postCategory);
        }

        // POST: Admin/PostCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            PostCategory postCategory = db.PostCategory.Find(id);
            db.PostCategory.Remove(postCategory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
