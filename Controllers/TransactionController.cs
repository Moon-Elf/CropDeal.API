using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CropDeal.API.DTOs.Transaction;
using CropDeal.API.Enums;
using CropDeal.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CropDeal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Farmer,Dealer,Admin")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepo;
        private readonly IReviewRepository _reviewRepo;
        public TransactionController(ITransactionRepository transactionRepo, IReviewRepository reviewRepo)
        {
            _transactionRepo = transactionRepo;
            _reviewRepo = reviewRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var transactions = await _transactionRepo.GetAll();
            var result = new List<TransactionDto>();
            foreach (var t in transactions)
            {
                var review = await _reviewRepo.GetReviewIdByTransactionIdAsync(t.Id).ConfigureAwait(false);
                result.Add(new TransactionDto
                {
                    Id = t.Id,
                    DealerId = t.DealerId,
                    ListingId = t.ListingId,
                    Quantity = t.Quantity,
                    FinalPricePerKg = t.FinalPricePerKg,
                    TotalPrice = t.TotalPrice,
                    Status = t.Status,
                    CreatedAt = t.CreatedAt,
                    ReviewId = review?.Id
                });
            }
            return Ok(result);
        }

        [HttpGet("my")]
        public async Task<IActionResult> GetMyTransactions()
        {
            var userId = GetUserId();
            var role = GetUserRole();
            var transactions = await _transactionRepo.GetAllForUserAsync(userId, role);

            var result = new List<TransactionDto>();
            foreach (var t in transactions)
            {
                var review = await _reviewRepo.GetReviewIdByTransactionIdAsync(t.Id).ConfigureAwait(false);
                result.Add(new TransactionDto
                {
                    Id = t.Id,
                    DealerId = t.DealerId,
                    ListingId = t.ListingId,
                    Quantity = t.Quantity,
                    FinalPricePerKg = t.FinalPricePerKg,
                    TotalPrice = t.TotalPrice,
                    Status = t.Status,
                    CreatedAt = t.CreatedAt,
                    ReviewId = review?.Id
                });
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var transaction = await _transactionRepo.GetByIdAsync(id);

            var review = await _reviewRepo.GetReviewIdByTransactionIdAsync(transaction.Id);

            var result = new TransactionDto
            {
                Id = transaction.Id,
                DealerId = transaction.DealerId,
                ListingId = transaction.ListingId,
                Quantity = transaction.Quantity,
                FinalPricePerKg = transaction.FinalPricePerKg,
                TotalPrice = transaction.TotalPrice,
                Status = transaction.Status,
                CreatedAt = transaction.CreatedAt,
                ReviewId = review?.Id 
            };

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Dealer")]
        public async Task<IActionResult> Create(CreateTransactionDto dto)
        {
            await _transactionRepo.CreateTransactionAsync(dto, GetUserId());
            return Ok(new { message = "Transaction created successfully." });
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromQuery] TransactionStatus status)
        {
            await _transactionRepo.UpdateTransactionStatusAsync(id, status, GetUserId(), GetUserRole());
            return Ok(new { message = "Transaction status updated successfully." });
        }

        private Guid GetUserId()
        {
            return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }

        private string GetUserRole()
        {
            return User.FindFirstValue(ClaimTypes.Role)!;
        }
    }
}