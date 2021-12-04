using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class SubscriptionType:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public uint Price { get; set; }
        public User Author { get; set; }
    }
}
