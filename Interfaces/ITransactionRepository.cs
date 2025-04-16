using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CropDeal.API.Models;
using CropDeal.API.DTOs.Transaction;
using CropDeal.API.Enums;

namespace CropDeal.API.Interfaces
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetAllForUserAsync(Guid userId, string role);
        Task<Transaction> GetByIdAsync(Guid transactionId);
        Task<bool> CreateTransactionAsync(CreateTransactionDto dto, Guid dealerId);
        Task<bool> UpdateTransactionStatusAsync(Guid transactionId, TransactionStatus status, Guid userId, string role);
    }
}