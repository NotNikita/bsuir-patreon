using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data
{
    public class Like:BaseEntity
    {
        public User Author { get; set; }
        public Post Post { get; set; }
    }
}
