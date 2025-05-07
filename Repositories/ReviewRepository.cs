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

        // Get review by id
        public async Task<Review> GetReviewByIdAsync(Guid reviewId) {
            var review = await _context.Reviews.FindAsync(reviewId);
            if (review == null)
                throw new NotFoundException("Review not found");

            return review;
        }

        // Get reviews by farmerId
        public async Task<IEnumerable<Review>> GetReviewsByFarmerIdAsync(Guid farmerId)
        {
            return await _context.Reviews
                .Where(r => r.FarmerId == farmerId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetReviewsByDealerIdAsync(Guid dealerId)
        {
            return await _context.Reviews
                .Where(r => r.DealerId == dealerId)
                .ToListAsync();
        }

        // Get review by transactionId
        public async Task<Review> GetReviewByTransactionIdAsync(Guid transactionId)
        {
            var review = await _context.Reviews.FirstOrDefaultAsync(r => r.TransactionId == transactionId);
            if (review == null)
                throw new NotFoundException("Review not found for this transaction.");
            return review;
        }

        public async Task<Review> GetReviewIdByTransactionIdAsync(Guid transactionId)
        {
            return await _context.Reviews.FirstOrDefaultAsync(r => r.TransactionId == transactionId);
        }

        // Create a new review
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

            var farmerReviews = await _context.Reviews.Where(r => r.FarmerId == review.FarmerId).ToListAsync();

            float average = farmerReviews.Any() ? (float)farmerReviews.Average(r => r.Rating) : 0f;
            var farmer = await _context.Users.FirstOrDefaultAsync(r => r.Id == review.FarmerId);

            if (farmer != null)
            {
                farmer.AverageRating = average;
            }
            
            await _context.Reviews.AddAsync(review);
            var result = await _context.SaveChangesAsync();
            if (result == 0)
                throw new AppException("Failed to create review.");
            return true;
        }

        // Get all reviews
        public async Task<IEnumerable<Review>> GetAllReviewsAsync()
        {
            return await _context.Reviews.ToListAsync();
        }

        // Update a review
        public async Task<bool> UpdateReviewAsync(Guid reviewId, UpdateReviewDto dto)
        {
            var review = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == reviewId);
            if (review == null)
                throw new NotFoundException("Review not found.");

            review.Rating = (int)dto.Rating;
            review.Comment = dto.Comment;

            var result = await _context.SaveChangesAsync();
            if (result == 0)
                throw new AppException("Failed to update review.");

            return true;
        }

        // Delete a review
        public async Task<bool> DeleteReviewAsync(Guid reviewId)
        {
            var review = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == reviewId);
            if (review == null)
                throw new NotFoundException("Review not found.");

            _context.Reviews.Remove(review);
            var result = await _context.SaveChangesAsync();
            if (result == 0)
                throw new AppException("Failed to delete review.");

            return true;
        }
    }
}
