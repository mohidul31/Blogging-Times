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

namespace Blogging_Times.Web.Areas.Admin.Controllers
{
    [AdminRequired]
    public class PostTagsController : Controller
    {
        private PostDbContext db = new PostDbContext();

        // GET: Admin/PostTags
        public ActionResult Index()
        {
            return View(db.PostTag.ToList());
        }

        // GET: Admin/PostTags/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostTag postTag = db.PostTag.Find(id);
            if (postTag == null)
            {
                return HttpNotFound();
            }
            return View(postTag);
        }

        // GET: Admin/PostTags/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/PostTags/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TagName,CreatedAt")] PostTag postTag)
        {
            if (ModelState.IsValid)
            {
                postTag.ID = Guid.NewGuid();
                db.PostTag.Add(postTag);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(postTag);
        }

        // GET: Admin/PostTags/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostTag postTag = db.PostTag.Find(id);
            if (postTag == null)
            {
                return HttpNotFound();
            }
            return View(postTag);
        }

        // POST: Admin/PostTags/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TagName,CreatedAt")] PostTag postTag)
        {
            if (ModelState.IsValid)
            {
                db.Entry(postTag).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(postTag);
        }

        // GET: Admin/PostTags/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostTag postTag = db.PostTag.Find(id);
            if (postTag == null)
            {
                return HttpNotFound();
            }
            return View(postTag);
        }

        // POST: Admin/PostTags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            PostTag postTag = db.PostTag.Find(id);
            db.PostTag.Remove(postTag);
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
