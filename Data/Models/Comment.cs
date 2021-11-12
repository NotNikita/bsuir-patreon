using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; }
        public User Author { get; set; }
        public Post Post { get; set; }
    }
}
