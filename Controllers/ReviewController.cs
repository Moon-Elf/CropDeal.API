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
    [Authorize(Roles = "Dealer,Farmer,Admin")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepo;

        public ReviewController(IReviewRepository reviewRepo)
        {
            _reviewRepo = reviewRepo;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReviewById(Guid id)
        {
            var review = await _reviewRepo.GetReviewByIdAsync(id);

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

        [HttpGet]
        public async Task<IActionResult> GetAllReviews()
        {
            var reviews = await _reviewRepo.GetAllReviewsAsync();
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

        [HttpGet("dealer/{dealerId}")]
        public async Task<IActionResult> GetReviewsForDealer(Guid dealerId)
        {
            var reviews = await _reviewRepo.GetReviewsByDealerIdAsync(dealerId);
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
            return Ok( new {message="Review submitted successfully."});
        }

        [HttpPut("{reviewId}")]
        public async Task<IActionResult> UpdateReview(Guid reviewId, UpdateReviewDto dto)
        {
            var result = await _reviewRepo.UpdateReviewAsync(reviewId, dto);
            if (result)
            {
                return Ok(new {message="Review updated successfully."});
            }

            return NotFound(new {message="Review not found."});
        }

        [HttpDelete("{reviewId}")]
        public async Task<IActionResult> DeleteReview(Guid reviewId)
        {
            var result = await _reviewRepo.DeleteReviewAsync(reviewId);
            if (result)
            {
                return Ok(new {message="Review deleted successfully."});
            }

            return NotFound(new {message="Review not found."});
        }
    }
}