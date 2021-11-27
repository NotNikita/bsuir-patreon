using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.Implementation
{
    public class CommentRepository: Repository<Comment>
    {
        private readonly ApplicationContext _context;
        public CommentRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Comment>> GetPostComments(int postId)
        {
            var comments = await _context.Comments.Include(x => x.Author).Include(x => x.Post).Where(x => x.Post.Id == postId).ToListAsync();
            return comments;
        }
    }
}
