using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CropDeal.API.Data;
using CropDeal.API.DTOs.Subscription;
using CropDeal.API.Exceptions;
using CropDeal.API.Interfaces;
using CropDeal.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CropDeal.API.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly AppDbContext _context;

        public SubscriptionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SubscriptionDto>> GetSubscriptionsAsync(Guid dealerId)
        {
            return await _context.Subscriptions
                .Where(s => s.DealerId == dealerId)
                .Include(s => s.Crop)
                .Select(s => new SubscriptionDto
                {
                    Id = s.Id,
                    CropId = s.CropId,
                    CropName = s.Crop.Name,
                    CreatedAt = s.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<SubscriptionDto> GetSubscriptionByIdAsync(Guid id, Guid dealerId)
        {
            var sub = await _context.Subscriptions
                                    .Include(s => s.Crop)
                                    .FirstOrDefaultAsync(s => s.Id == id && s.DealerId == dealerId);

            if (sub == null)
                throw new NotFoundException("Subscription not found.");

            return new SubscriptionDto
            {
                Id = sub.Id,
                CropId = sub.CropId,
                CropName = sub.Crop.Name,
                CreatedAt = sub.CreatedAt
            };

        }

        public async Task<bool> CreateSubscriptionAsync(CreateSubscriptionDto dto, Guid dealerId)
        {
            var alreadyExists = await _context.Subscriptions.AnyAsync(s => s.CropId == dto.CropId && s.DealerId == dealerId);
            if (alreadyExists)
                throw new AppException("You have already subscribed to this crop.");

            var subscription = new Subscription
            {
                DealerId = dealerId,
                CropId = dto.CropId,
            };

            await _context.Subscriptions.AddAsync(subscription);
            var result = await _context.SaveChangesAsync();

            if (result == 0)
                throw new AppException("Failed to subscribe.");

            return true;
        }

        public async Task<bool> DeleteSubscriptionAsync(Guid id, Guid dealerId)
        {
            var subscription = await _context.Subscriptions.FirstOrDefaultAsync(s => s.Id == id && s.DealerId == dealerId);

            if (subscription == null)
                throw new NotFoundException("Subscription not found.");

            _context.Subscriptions.Remove(subscription);

            var result = await _context.SaveChangesAsync();

            if (result == 0)
                throw new AppException("Failed to delete subscription.");

            return true;
        }
    }
}