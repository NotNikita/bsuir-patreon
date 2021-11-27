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
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Patreon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly CommentRepository _commentRepository;
        private readonly UserManager<User> _userManager;
        private readonly PostRepository _postRepository;

        public CommentsController(ApplicationContext context, CommentRepository commentRepository, UserManager<User> userManager, PostRepository postRepository)
        {
            _context = context;
            _commentRepository = commentRepository;
            _userManager = userManager;
            _postRepository = postRepository;
        }

        // GET: api/Comments
        [HttpGet]
        public async Task<IEnumerable<Comment>> GetComments()
        {
            return await _commentRepository.GetAll();
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<IEnumerable<Comment>> GetComment(int id)
        {
            var comment = await _commentRepository.GetPostComments(id);
            return comment;
        }

        // PUT: api/Comments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(int id, Comment comment)
        {
            if (id != comment.Id)
            {
                return BadRequest();
            }

            await _commentRepository.Update(comment);

            return NoContent();
        }

        // POST: api/Comments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{postId}")]
        public async Task<ActionResult<Comment>> PostCommentToPost(Comment comment, int postId)
        {
            if(HttpContext.User.Identity is ClaimsIdentity identity)
            {
                var name = identity.FindFirst(ClaimTypes.Name).Value;
                var user = await _userManager.FindByNameAsync(name);
                var post = await _postRepository.FindById(postId);

                comment.Author = user;
                comment.Post = post;
                await _commentRepository.Create(comment);
            }
            

            return CreatedAtAction("GetComment", new { id = comment.Id }, comment);
        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _commentRepository.FindById(id);
            if (comment == null)
            {
                return NotFound();
            }
            await _commentRepository.Delete(comment);

            return NoContent();
        }
    }
}
