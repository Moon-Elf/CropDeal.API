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
    [Authorize(Roles = "Farmer,Dealer")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepo;

        public TransactionController(ITransactionRepository transactionRepo)
        {
            _transactionRepo = transactionRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetMyTransactions()
        {
            var userId = GetUserId();
            var role = GetUserRole();
            var transactions = await _transactionRepo.GetAllForUserAsync(userId, role);

            var result = transactions.Select(t => new TransactionDto
            {
                Id = t.Id,
                DealerId = t.DealerId,
                ListingId = t.ListingId,
                Quantity = t.Quantity,
                FinalPricePerKg = t.FinalPricePerKg,
                TotalPrice = t.TotalPrice,
                Status = t.Status,
                CreatedAt = t.CreatedAt
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var transaction = await _transactionRepo.GetByIdAsync(id);

            var result = new TransactionDto
            {
                Id = transaction.Id,
                DealerId = transaction.DealerId,
                ListingId = transaction.ListingId,
                Quantity = transaction.Quantity,
                FinalPricePerKg = transaction.FinalPricePerKg,
                TotalPrice = transaction.TotalPrice,
                Status = transaction.Status,
                CreatedAt = transaction.CreatedAt
            };

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Dealer")]
        public async Task<IActionResult> Create(CreateTransactionDto dto)
        {
            await _transactionRepo.CreateTransactionAsync(dto, GetUserId());
            return Ok("Transaction created successfully.");
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromQuery] TransactionStatus status)
        {
            await _transactionRepo.UpdateTransactionStatusAsync(id, status, GetUserId(), GetUserRole());
            return Ok("Transaction status updated successfully.");
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