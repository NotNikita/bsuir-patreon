using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.Implementation
{
    public class SubscriptionRepository: Repository<Subscription>
    {
        public SubscriptionRepository(ApplicationContext context) : base(context)
        {

        }
    }
}
