using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CropDeal.API.Models;
using CropDeal.API.DTOs.Transaction;
using CropDeal.API.Enums;
using CropDeal.API.Data;
using Microsoft.EntityFrameworkCore;
using CropDeal.API.Exceptions;
using CropDeal.API.Interfaces;

namespace CropDeal.API.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;

        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Transaction>> GetAll() {
            return await _context.Transactions.ToListAsync();
            
        }

        public async Task<IEnumerable<Transaction>> GetAllForUserAsync(Guid userId, string role)
        {
            IEnumerable<Transaction> transactions = null;

            if (role == "Dealer")
            {
                transactions = await _context.Transactions.Where(t => t.DealerId == userId).ToListAsync();
            }
            else if (role == "Farmer")
            {
                transactions = await _context.Transactions.Include(t => t.PurchasingCrop).Where(t => t.PurchasingCrop.FarmerId == userId).ToListAsync();
            }

            return transactions ?? new List<Transaction>();


        }
        public async Task<Transaction> GetByIdAsync(Guid transactionId)
        {
            var transaction = await _context.Transactions.FirstOrDefaultAsync(t => t.Id == transactionId);

            if (transaction == null)
                throw new NotFoundException("Transaction not found");

            return transaction;
        }

        public async Task<bool> CreateTransactionAsync(CreateTransactionDto dto, Guid dealerId)
        {
            var cropListing = await _context.CropListings.FirstOrDefaultAsync(cl => cl.Id == dto.ListingId);

            if (cropListing == null)
                throw new NotFoundException("Crop listing not found");

            if (dto.Quantity > cropListing.Quantity)
                throw new AppException("Requested quantity exceeds available stock.");

            var totalPrice = dto.Quantity * dto.FinalPricePerKg;

            var transaction = new Transaction
            {
                DealerId = dealerId,
                ListingId = dto.ListingId,
                Quantity = dto.Quantity,
                FinalPricePerKg = dto.FinalPricePerKg,
                TotalPrice = totalPrice,
                Status = TransactionStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            cropListing.Quantity -= dto.Quantity;
            if (cropListing.Quantity == 0)
                cropListing.Status = CropAvailability.OutOfStock;

            await _context.Transactions.AddAsync(transaction);
            var result = await _context.SaveChangesAsync();

            if (result == 0)
                throw new AppException("Failed to create transaction.");

            return true;
        }

        public async Task<bool> UpdateTransactionStatusAsync(Guid transactionId, TransactionStatus status, Guid userId, string role)
        {
            Transaction transaction = null;

            if (role == "Dealer")
            {
                transaction = await _context.Transactions
                    .Include(t => t.PurchasingCrop)
                    .FirstOrDefaultAsync(t => t.Id == transactionId && t.DealerId == userId);
            }
            else if (role == "Farmer")
            {
                transaction = await _context.Transactions
                    .Include(t => t.PurchasingCrop)
                    .FirstOrDefaultAsync(t => t.Id == transactionId && t.PurchasingCrop.FarmerId == userId);
            }

            if (transaction == null)
                throw new NotFoundException("Transaction not found");

            if (transaction.Status == status)
                return true;

            if (status == TransactionStatus.Cancelled)
            {
                var cropListing = transaction.PurchasingCrop;
                cropListing.Quantity += transaction.Quantity;
                cropListing.Status = CropAvailability.Available;
            }
            transaction.Status = status;
            transaction.UpdatedAt = DateTime.UtcNow;

            var result = await _context.SaveChangesAsync();
            if (result == 0)
                throw new AppException("Failed to update transaction status.");

            return true;
        }

    }
}