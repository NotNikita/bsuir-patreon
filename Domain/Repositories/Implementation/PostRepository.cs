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
        private readonly UserRepository _userRepository;
        public PostRepository(ApplicationContext context, UserManager<User> userManager, UserRepository userRepository) : base(context)
        {
            _context = context;
            _userManager = userManager;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<Post>> GetAllPostsWithData()
        {
            var posts = await _context.Posts.Include(x => x.Likes).ThenInclude(x => x.Author).Include(x => x.Comments).ThenInclude(x => x.Author).Include(x=>x.Author).ToListAsync();
            return posts.OrderByDescending(x => x.PublicationDate);
        }

        public async Task<IEnumerable<Post>> GetAuthorPosts(User user)
        {
            var posts = await _context.Posts.Include(x => x.Likes).ThenInclude(x=>x.Author).Include(x => x.Comments).ThenInclude(x => x.Author).Where(x => x.Author.Id == user.Id).ToListAsync();
            return posts.OrderByDescending(x=>x.PublicationDate);
        }

        public async Task<IEnumerable<Post>> GetSubPosts(User user)
        {
            var full_user = await _context.Users.Include(u => u.Subscriptions).ThenInclude(u => u.Author).Where(x => x.Id == user.Id).FirstOrDefaultAsync();
            List<Post> posts = new List<Post>();
            foreach (var u in full_user.Subscriptions)
            {
                
                posts.AddRange(await _context.Posts.Include(x => x.Likes).ThenInclude(x => x.Author)
                    .Include(x => x.Comments).ThenInclude(x => x.Author).Include(x=>x.Author)
                    .Where(x=>x.Author == u.Author && x.IsChecked == true).ToListAsync());
            }

            return posts.OrderByDescending(x => x.PublicationDate); ;
        }

        public async Task<Post> GetPostWithData(int postId)
        {
            var post = await _context.Posts.Include(x => x.Likes).ThenInclude(x=>x.Author).Include(x=>x.Comments).ThenInclude(x => x.Author).Include(x=>x.Author).FirstAsync(x => x.Id == postId);
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
            var find = _context.Likes.Where(u => u.Author.UserName == user.UserName && u.Post.Id == postId).FirstOrDefault();
            if (find is null)
            {
                _context.Likes.Add(like);
            }
            else
            {
                _context.Likes.Remove(find);
            }
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Post>> GetUncheckedPost()
        {
            var posts = await _context.Posts.Include(x => x.Author).Where(x => x.IsChecked == false).ToListAsync();
            return posts.OrderByDescending(x=>x.PublicationDate);
        }
    }
}
