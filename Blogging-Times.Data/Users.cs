using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging_Times.Data
{
    public class Users 
    {
        public Users()
        {
            ID = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }
        [Key]
        public Guid ID { get; set; }

        [ScaffoldColumn(false)]
        public DateTime CreatedAt { get; set; }

        [ScaffoldColumn(false)]
        public DateTime? UpdatedAt { get; set; }

        [StringLength(450)]
        [Index(IsUnique = true)]
        public string Username { get; set; }
        
        [Required]
        public string Password { get; set; }

        [Display(Name = "User roal")]
        public virtual UserRoleEnum? UserRoleEnum { get; set; }
    }
}
