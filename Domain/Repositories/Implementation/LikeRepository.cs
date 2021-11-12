using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.Implementation
{
    public class LikeRepository: Repository<Like>
    {
        public LikeRepository(ApplicationContext context) : base(context)
        {

        }
    }
}
