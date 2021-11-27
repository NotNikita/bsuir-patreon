using Data;
using Domain.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;
        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserWithData(string username)
        {
            var user = await _context.Users.Include(s => s.Subscriptions).ThenInclude(s => s.User).Include(f => f.Followers).ThenInclude(f => f.User).FirstOrDefaultAsync(u => u.UserName == username);
            return user;
        }

        public async Task<IEnumerable<User>> GetFollowers(string userId)
        {
            var user = await _context.Users.Include(f => f.Followers).ThenInclude(f => f.User).FirstOrDefaultAsync(u => u.Id == userId);
            List<User> followers = new List<User>();
            foreach (var u in user.Followers)
            {
                followers.Add(u.User);
            }
            return followers;
        }

        public async Task<IEnumerable<User>> GetSubscriptions(string userId)
        {
            var user = await _context.Users.Include(s => s.Subscriptions).ThenInclude(s => s.Author).FirstOrDefaultAsync(u => u.Id == userId);
            List<User> subscribes = new List<User>();
            foreach (var u in user.Subscriptions)
            {
                subscribes.Add(u.Author);
            }
            return subscribes;
        }
    }
}
