using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging_Times.Data
{
    public class PostTag : Entity
    {
        [Required]
        [Display(Name = "Tag Name")]
        public string TagName { get; set; }

        public virtual List<Post> PostList { get; set; }
    }
}
