using Data;
using Domain;
using Domain.Repositories.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Patreon.Controllers
{
    [Authorize]
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

        // GET: api/User/followers
        [HttpGet("followers")]
        public async Task<IEnumerable<User>> GetFollowers()
        {
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                var name = identity.FindFirst(ClaimTypes.Name).Value;
                var user = await _userManager.FindByNameAsync(name);

                var followers = await _userRepository.GetFollowers(user.Id);
                return followers;

            }
            return null;
        }

        // GET: api/User/subscribes
        [HttpGet("subscribes")]
        public async Task<IEnumerable<User>> GetSubscribes()
        {
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                var name = identity.FindFirst(ClaimTypes.Name).Value;
                var user = await _userManager.FindByNameAsync(name);

                var subscriptions = await _userRepository.GetSubscriptions(user.Id);
                return subscriptions;

            }
            return null;
        }

        // POST: api/User/replenish/500
        [HttpPost("replenish/{amount}")]
        public async Task<ActionResult> AddMoney(uint amount)
        {
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                var name = identity.FindFirst(ClaimTypes.Name).Value;
                var user = await _userManager.FindByNameAsync(name);
                user.Balance += amount;
                await _userManager.UpdateAsync(user);
                return Ok("Пополнение баланса проведено успешно");

            }
            return Unauthorized();
        }

        // DELETE: api/User/4
        [HttpDelete("id")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(user);
            return Ok($"{user.UserName} успешно удален");
        }

        // POST: api/User/role/5/moderator
        [HttpPost("role/{id}/{nameRole}")]
        public async Task<ActionResult> ChangeRole(string id, string nameRole)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (await _userManager.IsInRoleAsync(user, nameRole))
            {
                await _userManager.RemoveFromRoleAsync(user, nameRole);
            }
            else
            {
                await _userManager.AddToRoleAsync(user, nameRole);
            }

            return Ok();
        }

        // POST: api/User/find/forbz
        [HttpPost("find/{username}")]
        public async Task<IEnumerable<User>> FindUser(string username)
        {
            var user = await _userRepository.FindUsers(username);
            return user;
        }

        // Текущий пользователь с ролью
        // GET: api/User/currentuser
        [HttpGet("currentuser")]
        public async Task<ActionResult<string>> CurrentUser()
        {
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                var name = identity.FindFirst(ClaimTypes.Name).Value;
                var user = await _userManager.FindByNameAsync(name);
                var roles = await _userManager.GetRolesAsync(user);
                string output = JsonConvert.SerializeObject(user);
                output = output.Insert(output.Length - 1, $",\"Role\":\"{roles[0]}\"");
                return output;
            }
            return Unauthorized();

        }
    }
}
