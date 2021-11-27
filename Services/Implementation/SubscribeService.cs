using Data;
using Domain.Repositories.Implementation;
using Microsoft.AspNetCore.Identity;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class SubscribeService:ISubscribeService
    {
        private readonly IEmail _email;
        private readonly UserManager<User> _userManager;
        private readonly SubscriptionRepository _subscriptionRepository;
        public SubscribeService(IEmail email, UserManager<User> userManager, SubscriptionRepository subscriptionRepository)
        {
            _email = email;
            _userManager = userManager;
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task CheckSubscribe()
        {
            var subscriptions = await _subscriptionRepository.GetAllPostsWithUsers();
            foreach(var sub in subscriptions)
            {
                if(sub.EndTime <= DateTime.UtcNow)
                {
                    var message = $"Привет! Твоя подписка на пользователя {sub.User.UserName} закончилась";
                }
            }
        }


    }
}
