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
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Blogging_Times.Web.Areas.Admin.Controllers
{
    [LoginRequired]
    public class PostsController : Controller
    {
        private PostDbContext db = new PostDbContext();

        public ActionResult Index()
        {
             var post = db.Post;
            return View(post.ToList());
        }
        // GET: Admin/Posts
        public ActionResult Index2()
        {
            
            IEnumerable<Post> post = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:51978/");

                client.DefaultRequestHeaders.Clear();
                var responseTask = client.GetAsync("api/PostApi");
                responseTask.Wait();
                HttpResponseMessage response = responseTask.Result;

                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsAsync<IList<Post>>();
                    readTask.Wait();
                    post= readTask.Result;
                }
                else
                {
                    post = Enumerable.Empty<Post>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

            }
            return View("Index", post);
        }

        public async Task<ActionResult> Index3()
        {
            IEnumerable<Post> post = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:51978/");

                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = await client.GetAsync("api/PostApi");

                if (response.IsSuccessStatusCode)
                {
                     post = await response.Content.ReadAsAsync<IList<Post>>();
                }
                else
                {
                    post =  Enumerable.Empty<Post>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View("Index",post);
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
        [ValidateInput(false)]
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
        [AdminRequired]
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

        public ActionResult Export()
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/CrystalReports/PostsReportForAdmin.rpt")));

            //rd.SetDataSource(db.Post.ToList());
            rd.SetDataSource(db.Post.Select(p => new
            {
                Title = p.PostTitle,
                Description = p.PostDescription,
            }).ToList());

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "PostList.pdf");
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
