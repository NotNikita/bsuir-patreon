using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data
{
    public class Post : BaseEntity
    {
        public string Content { get; set; }
        public User Author { get; set; }
        public string FileUrl { get; set; }
        public IEnumerable<Like> Likes { get; set; }
        public IEnumerable<Comment> Comments { get; set; }

    }
}
