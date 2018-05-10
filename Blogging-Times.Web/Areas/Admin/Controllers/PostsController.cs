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

namespace Blogging_Times.Web.Areas.Admin.Controllers
{
    public class PostsController : Controller
    {
        private PostDbContext db = new PostDbContext();

        // GET: Admin/Posts
        public ActionResult Index()
        {
            var post = db.Post.Include(p => p.PostCategory);
            return View(post.ToList());
        }

        // GET: Admin/Posts/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Post.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Admin/Posts/Create
        public ActionResult Create()
        {
            ViewBag.PostCategory_ID = new SelectList(db.PostCategory, "ID", "CategoryName");
            ViewBag.PostTag_ID = new SelectList(db.PostTag, "ID", "TagName");
            return View();
        }

        // POST: Admin/Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( PostModel model)
        {
            if (ModelState.IsValid)
            {
                model.CreatePost();
                //post.ID = Guid.NewGuid();
                //db.Post.Add(post);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PostTag_ID = new SelectList(db.PostTag, "ID", "TagName");
            ViewBag.PostCategory_ID = new SelectList(db.PostCategory, "ID", "CategoryName", model.PostCategory_ID);
            return View(model);
        }

        // GET: Admin/Posts/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Post.Find(id);
            PostModel model = new PostModel();
            model.ID = post.ID;
            model.PostCategory_ID = post.PostCategory_ID;
            model.PostDescription = post.PostDescription;
            model.PostTitle = post.PostTitle;
            model.PostTag_ID = post.PostTagList.Select(x => x.ID.ToString()).ToArray();

            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.PostTag_ID = new MultiSelectList(db.PostTag, "ID", "TagName", model.PostTag_ID);
            ViewBag.PostCategory_ID = new SelectList(db.PostCategory, "ID", "CategoryName", post.PostCategory_ID);
            return View(model);
        }

        // POST: Admin/Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PostModel model)
        {
            if (ModelState.IsValid)
            {
                model.UpdatePost();
                return RedirectToAction("Index");
            }
            ViewBag.PostTag_ID = new MultiSelectList(db.PostTag, "ID", "TagName", model.PostTag_ID);
            ViewBag.PostCategory_ID = new SelectList(db.PostCategory, "ID", "CategoryName", model.PostCategory_ID);
            return View(model);
        }

        // GET: Admin/Posts/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Post.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Admin/Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Post post = db.Post.Find(id);
            db.Post.Remove(post);
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
