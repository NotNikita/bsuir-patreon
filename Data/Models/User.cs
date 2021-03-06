using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public uint Balance { get; set; }
        public IEnumerable<Subscription> Subscriptions { get; set; }
        public IEnumerable<Subscription> Followers { get; set; }

    }
}
