using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging_Times.Data
{
    public class PostTag : Entity
    {
        public string TagName { get; set; }
        public virtual List<Post> PostList { get; set; }
    }
}
