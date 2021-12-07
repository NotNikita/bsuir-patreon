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
using Microsoft.AspNetCore.Authorization;

namespace Patreon.Controllers
{
    [Authorize]
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
    }
}
