using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging_Times.Data
{
    public class Entity
    {
        public Entity()
        {
            ID = new Guid();
            CreatedAt = DateTime.Now;
        }

        [Key]
        public Guid ID { get; set; }

        [ScaffoldColumn(false)]
        public DateTime CreatedAt { get; set; }
    }
}
