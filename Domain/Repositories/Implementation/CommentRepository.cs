using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.Implementation
{
    public class CommentRepository: Repository<Comment>
    {
        public CommentRepository(ApplicationContext context) : base(context)
        {

        }
    }
}
