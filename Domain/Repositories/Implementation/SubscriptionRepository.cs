using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.Implementation
{
    public class SubscriptionRepository: Repository<Subscription>
    {
        private readonly ApplicationContext _context;
        public SubscriptionRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Subscription>> GetAllSubsWithUsers()
        {
            var subscriptions = await _context.Subscriptions.Include(x => x.User).Include(x=>x.Author).Include(x=>x.Sub).ToListAsync();
            return subscriptions;
        }
    }
}
