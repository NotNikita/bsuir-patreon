using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.Implementation
{
    public class SubscriptionTypeRepository : Repository<SubscriptionType>
    {
        private readonly ApplicationContext _context;
        public SubscriptionTypeRepository(ApplicationContext context):base(context)
        {
            _context = context;
        }

        public async Task<SubscriptionType> FindByIdWithData(int id)
        {
            var sub = await _context.SubscriptionTypes.Include(x => x.Author).FirstAsync(x => x.Id == id);
            return sub;
        }
    }
}
