using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Patreon.Models
{
    public class Like:BaseEntity
    {
        public User Author { get; set; }
        public Post Post { get; set; }
    }
}
