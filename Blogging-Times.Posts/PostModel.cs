using Blogging_Times.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging_Times.Posts
{
    public class PostModel : Post
    {
        public string []PostTag_ID { get; set; }

        PostDbContext db = new PostDbContext();

        public Post GetPostInfoFromUserInput(Post post=null)
        {
            List<PostTag> postTagList = new List<PostTag>();

            for (int i = 0; i < PostTag_ID.Length; i++)
            {
                Guid id = new Guid(PostTag_ID[i]);
                PostTag p = db.PostTag.Where(x => x.ID == id).FirstOrDefault();
                postTagList.Add(p);
            }
            if (post==null)
            {
                 post = new Post();
            }
            else
            {
                //Clear List To avoid duplicate list data
                post.PostTagList.Clear();
                post.UpdatedAt = DateTime.Now;
            }
            
            post.PostCategory_ID = PostCategory_ID;
            post.PostTitle = PostTitle;
            post.PostDescription = PostDescription;
            post.PostTagList = postTagList;

            return post;
        }
        public void CreatePost()
        {
            Post post = GetPostInfoFromUserInput();
            //db.Post.Add(post);
            new Repository<Post>(db).Add(post);
            db.SaveChanges();
        }

        public void UpdatePost()
        {
            Post post = db.Post.Find(ID);

            Post updatedData = GetPostInfoFromUserInput(post);
            db.SaveChanges();
        }

        public int GetPostCountByMonthAndYear(int month=0,int year=0)
        {
            IQueryable<Post> result= db.Post;

            if(month != 0)
            {
                result = result.Where(x => x.CreatedAt.Month == month);
            }

            if (year != 0)
            {
                result = result.Where(x => x.CreatedAt.Year == year);
            }

            return result.Count();
        }
    }
}
