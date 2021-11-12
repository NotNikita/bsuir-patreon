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
    public class LikeController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly LikeRepository _likeRepository;

        public LikeController(ApplicationContext context, LikeRepository likeRepository)
        {
            _context = context;
            _likeRepository = likeRepository;
        }

        // GET: api/Like
        [HttpGet]
        public async Task<IEnumerable<Like>> GetLikes()
        {
            return await _likeRepository.GetAll();
        }

        // GET: api/Like/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Like>> GetLike(int id)
        {
            var like = await _likeRepository.FindById(id);

            if (like == null)
            {
                return NotFound();
            }

            return like;
        }

        // PUT: api/Like/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLike(int id, Like like)
        {
            if (id != like.Id)
            {
                return BadRequest();
            }

            await _likeRepository.Update(like);

            return NoContent();
        }

        // POST: api/Like
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Like>> PostLike(Like like)
        {
            await _likeRepository.Create(like);

            return CreatedAtAction("GetLike", new { id = like.Id }, like);
        }

        // DELETE: api/Like/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLike(int id)
        {
            var like = await _likeRepository.FindById(id);
            if (like == null)
            {
                return NotFound();
            }

            await _likeRepository.Delete(like);

            return NoContent();
        }
    }
}
