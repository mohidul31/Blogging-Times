using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging_Times.Data
{
    public class Post : Entity
    {
        [Required]
        [Display(Name = "Post Title")]
        public string PostTitle { get; set; }

        [Required]
        [Display(Name = "Post Description")]
        public string PostDescription { get; set; }

        [Display(Name = "Post Category")]
        public Guid PostCategory_ID { get; set; }
        [ForeignKey("PostCategory_ID")]
        public virtual PostCategory PostCategory { get; set; }

        public virtual List<PostTag> PostTagList { get; set; }
    }
}
