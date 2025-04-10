using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CropDeal.API.DTOs.Subscription;

namespace CropDeal.API.Interfaces
{
    public interface ISubscriptionRepository
    {
        Task<IEnumerable<SubscriptionDto>> GetSubscriptionsAsync(Guid dealerId);

        Task<SubscriptionDto> GetSubscriptionByIdAsync(Guid id, Guid dealerId);

        Task<bool> CreateSubscriptionAsync(CreateSubscriptionDto dto, Guid dealerId);

        Task<bool> DeleteSubscriptionAsync(Guid id, Guid dealerId);
    }
}