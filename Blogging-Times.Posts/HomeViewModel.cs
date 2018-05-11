using Blogging_Times.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging_Times.Posts
{
    public class HomeViewModel
    {
        public Post PostDetails { get; set; }
        public List<Post> PostList { get; set; }
        public List<PostTag> PostTagList { get; set; }
        public List<PostCategory> PostCategoryList { get; set; }
        public List<Post> RecentPostList { get; set; }
        public List<ArchiveEntry> ArchiveList { get; set; }

        public HomeViewModel(Guid? tag, Guid? category,int? month,int? year,Guid? post=null, string search=null)
        {
            PostDbContext db = new PostDbContext();
            if (post.HasValue)
            {
                PostDetails = db.Post.Where(x => x.ID == post.Value).SingleOrDefault();
            }
           
            /*==================================================*/
            IQueryable<Post> result = db.Post;
            if (tag.HasValue)
            {
                //search by tag
                result = result.Where(x => x.PostTagList.Any(y => y.ID==tag.Value));
            }
            if (category.HasValue)
            {
                //search by category
                result = result.Where(x => x.PostCategory.ID == category.Value);
            }
            if (month.HasValue && year.HasValue)
            {
                //search by moth year
                result = result.Where(x => x.CreatedAt.Month == month);
                result = result.Where(x => x.CreatedAt.Year == year);
            }

            if (! string.IsNullOrEmpty(search))
            {
                result = result.Where(
                    x => x.PostTitle.ToLower().Contains(search.ToLower())
                    || x.PostDescription.ToLower().Contains(search.ToLower())
                    || x.PostCategory.CategoryName.ToLower().Contains(search.ToLower())
                    || x.PostTagList.Any(y => y.TagName.ToLower().Contains(search.ToLower()))
                    );
            }
            PostList = result.OrderByDescending(x=> x.CreatedAt).ToList();

            /*==================================================*/

            PostTagList = db.PostTag.ToList();
            PostCategoryList = db.PostCategory.OrderByDescending(x => x.PostList.Count).ToList();
            RecentPostList = db.Post.OrderByDescending(x => x.CreatedAt).Take(5).ToList();

            ArchiveList = db.Post
                        .GroupBy(g => new
                        {
                            Month = g.CreatedAt.Month,
                            Year = g.CreatedAt.Year
                        })
                        .Select(s => new ArchiveEntry
                        {
                            Month = s.Key.Month,
                            Year = s.Key.Year,
                            Total = s.Count()
                        })
                        .OrderByDescending(a => a.Year)
                        .ThenByDescending(a => a.Month)
                        .ToList();

         }
    }
}
