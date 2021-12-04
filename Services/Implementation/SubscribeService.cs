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
            var subscriptions = await _subscriptionRepository.GetAllSubsWithUsers();
            string message = "";
            string subject = "";
            foreach(var sub in subscriptions)
            {
                if(sub.EndTime <= DateTime.UtcNow && sub.Sub.Price > sub.User.Balance)
                {
                    message = $"Привет {sub.User.UserName}! Твоя подписка на пользователя {sub.Author.UserName} закончилась и баланс на счету не позволяет продлить подписку," +
                        $"поэтому твоя подписка была отменена";
                    subject = $"Отмена подписки";
                    await _subscriptionRepository.Delete(sub);
                    await _email.SendAsync(sub.User.Email, subject, message);

                }
                else if (sub.EndTime <= DateTime.UtcNow && sub.Sub.Price < sub.User.Balance)
                {
                    message = $"Привет {sub.User.UserName}! Твоя подписка на пользователя {sub.Author.UserName} была автоматически продлена";
                    subject = $"Продление подписки";
                    sub.User.Balance -= sub.Sub.Price;
                    var dateDiff = sub.EndTime - sub.StartTime;
                    sub.StartTime = DateTime.UtcNow;
                    sub.EndTime = sub.StartTime + dateDiff;
                    await _subscriptionRepository.Update(sub);
                    await _userManager.UpdateAsync(sub.User);
                    await _email.SendAsync(sub.User.Email, subject, message);

                }

                

            }
        }


    }
}
