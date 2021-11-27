﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data.Models;
using Domain;
using Domain.Repositories.Implementation;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Data;

namespace Patreon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionTypeController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly SubscriptionTypeRepository _subscriptionTypeRepository;
        private readonly UserManager<User> _userManager;

        public SubscriptionTypeController(ApplicationContext context, SubscriptionTypeRepository subscriptionTypeRepository, UserManager<User> userManager)
        {
            _context = context;
            _subscriptionTypeRepository = subscriptionTypeRepository;
            _userManager = userManager;
        }

        // GET: api/SubscriptionType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubscriptionType>>> GetSubscriptionTypes()
        {
            return await _context.SubscriptionTypes.ToListAsync();
        }

        // GET: api/SubscriptionType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubscriptionType>> GetSubscriptionType(int id)
        {
            var subscriptionType = await _context.SubscriptionTypes.FindAsync(id);

            if (subscriptionType == null)
            {
                return NotFound();
            }

            return subscriptionType;
        }

        // POST: api/SubscriptionType
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SubscriptionType>> PostSubscriptionType(SubscriptionType subscriptionType)
        {
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                var name = identity.FindFirst(ClaimTypes.Name).Value;
                var author = await _userManager.FindByNameAsync(name);
                subscriptionType.Author = author;
                await _subscriptionTypeRepository.Create(subscriptionType);
            }


            return CreatedAtAction("GetSubscriptionType", new { id = subscriptionType.Id }, subscriptionType);
        }

        // DELETE: api/SubscriptionType/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubscriptionType(int id)
        {
            var subscriptionType = await _context.SubscriptionTypes.FindAsync(id);
            if (subscriptionType == null)
            {
                return NotFound();
            }

            _context.SubscriptionTypes.Remove(subscriptionType);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
