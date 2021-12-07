using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Domain;
using Domain.Repositories.Implementation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Patreon.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly PostRepository _postRepository;
        private readonly UserManager<User> _userManager;

        public PostController(PostRepository postRepository, UserManager<User> userManager)
        {
            _postRepository = postRepository;
            _userManager = userManager;
        }

        // GET: api/Post
        [HttpGet]
        public async Task<IEnumerable<Post>> GetPosts()
        {
            return await _postRepository.GetAll();
        }

        // GET: api/Post/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
            var post = await _postRepository.GetPostWithData(id);

            if (post == null)
            {
                return NotFound();
            }

            return post;
        }
        // GET: api/Post/getMyPosts
        [HttpGet("getMyPosts")]
        public async Task<IEnumerable<Post>> GetMyPosts()
        {
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                var name = identity.FindFirst(ClaimTypes.Name).Value;
                var user = await _userManager.FindByNameAsync(name);

                var posts = await _postRepository.GetAuthorPosts(user);

                return posts;

            }

            return null;
        }

        // GET: api/Post/getSubPosts
        [HttpGet("getSubPosts")]
        public async Task<IEnumerable<Post>> GetSubPosts()
        {
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                var name = identity.FindFirst(ClaimTypes.Name).Value;
                var user = await _userManager.FindByNameAsync(name);

                var posts = await _postRepository.GetSubPosts(user);

                return posts;

            }

            return null;
        }

        // PUT: api/Post/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(int id, Post post)
        {
            if (id != post.Id)
            {
                return BadRequest();
            }

            await _postRepository.Update(post);

            return NoContent();
        }

        // POST: api/Post
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Post>> PostPost(Post post)
        {
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                var name = identity.FindFirst(ClaimTypes.Name).Value;
                var user = await _userManager.FindByNameAsync(name);

                post.Author = user;
                post.PublicationDate = DateTime.Now;
                await _postRepository.Create(post);
            }
            

            return CreatedAtAction("GetPost", new { id = post.Id }, post);
        }

        // DELETE: api/Post/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _postRepository.FindById(id);
            if (post == null)
            {
                return NotFound();
            }
            await _postRepository.Delete(post);

            return NoContent();
        }

        //api/Post/addlike/
        [HttpPost("addlike/{postId}")]
        public async Task<IActionResult> AddLike(int postId)
        {

            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                var name = identity.FindFirst(ClaimTypes.Name).Value;
                var user = await _userManager.FindByNameAsync(name);
                await _postRepository.AddLike(postId, user);
            }
            return NoContent();
        }

        // POST: api/Post/approve/4
        [HttpPost("approve/{postId}")]
        public async Task<IActionResult> Approve(int postId)
        {
            var post = await _postRepository.FindById(postId);
            post.IsChecked = true;
            return Ok("Пост принят");
        }
    }
}
