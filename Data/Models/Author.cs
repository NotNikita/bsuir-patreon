using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Author: User
    {
        public IEnumerable<User> Followers { get; set; }
    }
}
