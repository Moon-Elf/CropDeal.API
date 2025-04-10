using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CropDeal.API.DTOs.Subscription;
using CropDeal.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CropDeal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionRepository _subscriptionRepo;

        public SubscriptionController(ISubscriptionRepository subscriptionRepo)
        {
            _subscriptionRepo = subscriptionRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetMySubscriptions()
        {
            var dealerId = GetUserId();
            var subscriptions = await _subscriptionRepo.GetSubscriptionsAsync(dealerId);
            return Ok(subscriptions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubscription(Guid id)
        {
            var dealerId = GetUserId();
            var subscription = await _subscriptionRepo.GetSubscriptionByIdAsync(id, dealerId);
            return Ok(subscription);
        }

        [HttpPost]
        public async Task<IActionResult> Subscribe(CreateSubscriptionDto dto)
        {
            await _subscriptionRepo.CreateSubscriptionAsync(dto, GetUserId());
            return Ok("Subscribed to crop successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Unsubscribe(Guid id)
        {
            await _subscriptionRepo.DeleteSubscriptionAsync(id, GetUserId());
            return Ok("Unsubscribed from crop successfully.");
        }



        private Guid GetUserId()
        {
            return Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        }
    }
}