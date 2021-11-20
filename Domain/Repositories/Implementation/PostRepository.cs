using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly UserManager<User> _userManager;
        public PostRepository(ApplicationContext context, UserManager<User> userManager) : base(context)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<Post> GetPostWithData(int postId)
        {
            var post = await _context.Posts.Include(x => x.Likes).FirstAsync(x => x.Id == postId);
            return post;
        }

        public async Task AddLike(int postId, User user)
        {
            var post = await _context.Posts.FindAsync(postId);
            var like = new Like
            {
                Author = user,
                Post = post
            };

            _context.Likes.Add(like);
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
        }
    }
}
