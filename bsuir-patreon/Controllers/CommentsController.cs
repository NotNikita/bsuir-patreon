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

namespace Patreon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly CommentRepository _commentRepository;

        public CommentsController(ApplicationContext context, CommentRepository commentRepository)
        {
            _context = context;
            _commentRepository = commentRepository;
        }

        // GET: api/Comments
        [HttpGet]
        public async Task<IEnumerable<Comment>> GetComments()
        {
            //return await _context.Comments.ToListAsync();
            return await _commentRepository.GetAll();
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(int id)
        {
            //var comment = await _context.Comments.FindAsync(id);
            var comment = await _commentRepository.FindById(id);

            if (comment == null)
            {
                return NotFound();
            }

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

            //_context.Entry(comment).State = EntityState.Modified;
            await _commentRepository.Update(comment);

            return NoContent();
        }

        // POST: api/Comments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Comment>> PostComment(Comment comment)
        {
            //_context.Comments.Add(comment);
            //await _context.SaveChangesAsync();

            await _commentRepository.Create(comment);

            return CreatedAtAction("GetComment", new { id = comment.Id }, comment);
        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            //var comment = await _context.Comments.FindAsync(id);
            var comment = await _commentRepository.FindById(id);
            if (comment == null)
            {
                return NotFound();
            }
            await _commentRepository.Delete(comment);
            //_context.Comments.Remove(comment);
            //await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
