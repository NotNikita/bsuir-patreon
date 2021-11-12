using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.Implementation
{
    public class PostRepository : Repository<Post>
    {
        private readonly ApplicationContext _context;
        public PostRepository(ApplicationContext context) : base(context)
        {

        }
    }
}
