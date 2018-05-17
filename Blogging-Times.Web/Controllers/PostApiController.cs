using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Blogging_Times.Data;
using Blogging_Times.Posts;

namespace Blogging_Times.Web.Controllers
{
    public class PostApiController : ApiController
    {
        private PostDbContext db = new PostDbContext();
        public PostApiController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        // GET: api/PostApi
        public IQueryable<Post> GetPost()
        {
            return db.Post;
        }

        // GET: api/PostApi/5
        [ResponseType(typeof(Post))]
        public IHttpActionResult GetPost(Guid id)
        {
            Post post = db.Post.Find(id);
            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        // PUT: api/PostApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPost(Guid id, Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != post.ID)
            {
                return BadRequest();
            }

            db.Entry(post).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PostApi
        [ResponseType(typeof(Post))]
        public IHttpActionResult PostPost(Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Post.Add(post);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PostExists(post.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = post.ID }, post);
        }

        // DELETE: api/PostApi/5
        [ResponseType(typeof(Post))]
        public IHttpActionResult DeletePost(Guid id)
        {
            Post post = db.Post.Find(id);
            if (post == null)
            {
                return NotFound();
            }

            db.Post.Remove(post);
            db.SaveChanges();

            return Ok(post);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PostExists(Guid id)
        {
            return db.Post.Count(e => e.ID == id) > 0;
        }
    }
}