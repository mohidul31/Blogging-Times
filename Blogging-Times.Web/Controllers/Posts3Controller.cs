using Blogging_Times.Data;
using Blogging_Times.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Blogging_Times.Web.Controllers
{
    public class Posts3Controller : ApiController
    {
        private PostDbContext db = new PostDbContext();
        public Posts3Controller()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        // GET: api/Posts3
        public IEnumerable<Post> Get()
        {
            // return new string[] { "value1", "value2" };

            return db.Post.ToList();
        }

        // GET: api/Posts3/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Posts3
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Posts3/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Posts3/5
        public void Delete(int id)
        {
        }
    }
}
