using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging_Times.Data
{
    public class Entity
    {
        public Entity()
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

        [ScaffoldColumn(false)]
        public Guid? CreatedBy { get; set; }
        [ForeignKey("CreatedBy")]
        public virtual Users CreatedByUsers { get; set; }

        [ScaffoldColumn(false)]
         public Guid? UpdatedBy { get; set; }
        [ForeignKey("UpdatedBy")]
        public virtual Users UpdatedByUsers { get; set; }
    }
}
