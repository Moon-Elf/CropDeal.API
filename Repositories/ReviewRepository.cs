using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CropDeal.API.Interfaces;
using CropDeal.API.Models;
using CropDeal.API.Exceptions;
using CropDeal.API.DTOs.Review;
using CropDeal.API.Data;
using Microsoft.EntityFrameworkCore;

namespace CropDeal.API.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly AppDbContext _context;

        public ReviewRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Review>> GetReviewsByFarmerIdAsync(Guid farmerId)
        {
            return await _context.Reviews
                .Where(r => r.FarmerId == farmerId)
                .ToListAsync();
        }

        public async Task<Review> GetReviewByTransactionIdAsync(Guid transactionId)
        {
            var review = await _context.Reviews.FirstOrDefaultAsync(r => r.TransactionId == transactionId);
            if (review == null)
                throw new NotFoundException("Review not found for this transaction.");
            return review;
        }

        public async Task<bool> CreateReviewAsync(CreateReviewDto dto, Guid dealerId)
        {
            var alreadyExists = await _context.Reviews.AnyAsync(r => r.TransactionId == dto.TransactionId);
            if (alreadyExists)
                throw new AppException("Review already exists for this transaction.");

            var review = new Review
            {
                DealerId = dealerId,
                FarmerId = dto.FarmerId,
                TransactionId = dto.TransactionId,
                Rating = dto.Rating,
                Comment = dto.Comment
            };

            await _context.Reviews.AddAsync(review);
            var result = await _context.SaveChangesAsync();
            if (result == 0)
                throw new AppException("Failed to create review.");
            return true;
        }

    }
}