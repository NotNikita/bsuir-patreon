using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data
{
    public class Subscription:BaseEntity
    {
        public SubscriptionType Sub { get; set; }
        public User User { get; set; }
        public User Author { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

    }
}
