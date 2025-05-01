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
        Task<IEnumerable<Review>> GetReviewsByFarmerIdAsync(Guid farmerId);
        Task<Review> GetReviewByTransactionIdAsync(Guid transactionId);
        Task<bool> CreateReviewAsync(CreateReviewDto dto, Guid dealerId);
    }
}