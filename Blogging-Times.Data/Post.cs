using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging_Times.Data
{
    public class Post : Entity
    {
        public string PostTitle { get; set; }
        public string PostDescription { get; set; }
        public virtual PostCategory PostCategory { get; set; }
        public virtual List<PostTag> PostTagList { get; set; }
    }
}
