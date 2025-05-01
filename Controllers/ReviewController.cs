using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CropDeal.API.DTOs.Review;
using CropDeal.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CropDeal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Dealer,Farmers")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepo;

        public ReviewController(IReviewRepository reviewRepo)
        {
            _reviewRepo = reviewRepo;
        }

        [HttpGet("farmer/{farmerId}")]
        public async Task<IActionResult> GetReviewsForFarmer(Guid farmerId)
        {
            var reviews = await _reviewRepo.GetReviewsByFarmerIdAsync(farmerId);
            var result = reviews.Select(r => new ReviewDto
            {
                Id = r.Id,
                DealerId = r.DealerId,
                FarmerId = r.FarmerId,
                TransactionId = r.TransactionId,
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt
            });

            return Ok(result);
        }

        [HttpGet("transaction/{transactionId}")]
        public async Task<IActionResult> GetReviewForTransaction(Guid transactionId)
        {
            var review = await _reviewRepo.GetReviewByTransactionIdAsync(transactionId);

            var result = new ReviewDto
            {
                Id = review.Id,
                DealerId = review.DealerId,
                FarmerId = review.FarmerId,
                TransactionId = review.TransactionId,
                Rating = review.Rating,
                Comment = review.Comment,
                CreatedAt = review.CreatedAt
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview(CreateReviewDto dto)
        {
            var dealerId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            await _reviewRepo.CreateReviewAsync(dto, dealerId);
            return Ok("Review submitted successfully.");
        }

    }
}