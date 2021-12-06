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
using Data.Models;
using System.Security.Claims;
using Hangfire;
using Services.Implementation;

namespace Patreon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionsController : ControllerBase
    {
        private readonly SubscriptionRepository _subscriptionRepository;
        private readonly UserManager<User> _userManager;
        private readonly SubscriptionTypeRepository _subscriptionTypeRepository;
        private readonly IRecurringJobManager _recurringJobManager;

        public SubscriptionsController(SubscriptionRepository subscriptionRepository, UserManager<User> userManager, SubscriptionTypeRepository subscriptionTypeRepository, IRecurringJobManager recurringJobManager)
        {
            _subscriptionRepository = subscriptionRepository;
            _userManager = userManager;
            _subscriptionTypeRepository = subscriptionTypeRepository;
            _recurringJobManager = recurringJobManager;
        }

        [HttpGet("/ReccuringJob")]
        public ActionResult CreateReccuringJub()
        {
            _recurringJobManager.AddOrUpdate<SubscribeService>("1", x => x.CheckSubscribe(), "0 1 ? ? ? ?");
            return Ok();
        }
        // GET: api/Subscriptions
        [HttpGet]
        public async Task<IEnumerable<Subscription>> GetSubscriptions()
        {
            return await _subscriptionRepository.GetAll();
        }

        // GET: api/Subscriptions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Subscription>> GetSubscription(int id)
        {
            var subscription = await _subscriptionRepository.FindById(id);

            if (subscription == null)
            {
                return NotFound();
            }

            return subscription;
        }

        // PUT: api/Subscriptions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubscription(int id, Subscription subscription)
        {
            if (id != subscription.Id)
            {
                return BadRequest();
            }

            await _subscriptionRepository.Update(subscription);

            return NoContent();
        }

        //POST: api/Subscriptions/idSubType
        [HttpPost("{idSubType}")]
        public async Task<ActionResult<Subscription>> Subscribe(Subscription subscription, int idSubType)
        {
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                var name = identity.FindFirst(ClaimTypes.Name).Value;
                var user = await _userManager.FindByNameAsync(name);

                var authorSub = await _subscriptionTypeRepository.FindByIdWithData(idSubType);
                var author = authorSub.Author;

                subscription.User = user;
                subscription.Author = author;
                subscription.Sub = authorSub;
                subscription.StartTime = DateTime.Now;
                subscription.EndTime = subscription.StartTime.AddDays((double)authorSub.Duration);
                user.Balance -= subscription.Sub.Price;
                author.Balance += subscription.Sub.Price;
                await _userManager.UpdateAsync(user);
                await _subscriptionRepository.Create(subscription);
            }
            

            return CreatedAtAction("GetSubscription", new { id = subscription.Id }, subscription);

        }

        // DELETE: api/Subscriptions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubscription(int id)
        {
            var subscription = await _subscriptionRepository.FindById(id);
            if (subscription == null)
            {
                return NotFound();
            }

            await _subscriptionRepository.Delete(subscription);

            return NoContent();
        }
    }
}
