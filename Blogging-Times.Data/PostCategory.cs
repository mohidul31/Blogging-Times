using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging_Times.Data
{
    public class PostCategory : Entity
    {
        [Required]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        //report
        public virtual List<Post> PostList { get; set; }
    }
}
