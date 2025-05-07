using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CropDeal.API.DTOs.Review;
using CropDeal.API.Models;

namespace CropDeal.API.Interfaces
{
    public interface IReviewRepository
    {
        Task<Review> GetReviewByIdAsync(Guid reviewId);
        Task<IEnumerable<Review>> GetReviewsByFarmerIdAsync(Guid farmerId);
        Task<IEnumerable<Review>> GetReviewsByDealerIdAsync(Guid dealerId);
        Task<Review> GetReviewByTransactionIdAsync(Guid transactionId);
        Task<Review> GetReviewIdByTransactionIdAsync(Guid transactionId);
        Task<bool> CreateReviewAsync(CreateReviewDto dto, Guid dealerId);
        Task<IEnumerable<Review>> GetAllReviewsAsync();
        Task<bool> UpdateReviewAsync(Guid reviewId, UpdateReviewDto dto);
        Task<bool> DeleteReviewAsync(Guid reviewId);
    }
}