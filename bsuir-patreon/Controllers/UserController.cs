using Data;
using Domain;
using Domain.Repositories.Implementation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Patreon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly UserRepository _userRepository;

        public UserController(ApplicationContext context, UserManager<User> userManager, SignInManager<User> signInManager, UserRepository userRepository)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _userRepository = userRepository;
        }
        // GET: api/<UserController>
        [HttpGet]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetCustomers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET api/<UserController>/forbz
        [HttpGet("{username}")]
        public async Task<ActionResult<User>> Get(string username)
        {
            var user = await _userRepository.GetUserWithData(username);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpGet("followers/{userId}")]
        public async Task<IEnumerable<User>> GetFollowers(string userId)
        {
            var followers = await _userRepository.GetFollowers(userId);
            return followers;
        }

        [HttpGet("subscribes/{userId}")]
        public async Task<IEnumerable<User>> GetSubscribes(string userId)
        {
            var subscriptions = await _userRepository.GetSubscriptions(userId);
            return subscriptions;
        }
    }
}
