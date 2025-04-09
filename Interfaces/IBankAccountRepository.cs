using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CropDeal.API.DTOs.BankAccount;
using CropDeal.API.Models;

namespace CropDeal.API.Interfaces
{
    public interface IBankAccountRepository
    {
        Task<IEnumerable<BankAccount>> GetUserBankAccountsAsync(Guid userId);

        Task<BankAccount> GetBankAccountByIdAsync(Guid id, Guid userId);

        Task<bool> CreateBankAccountAsync(CreateBankAccountDto dto, Guid userId);

        Task<bool> UpdateBankAccountAsync(UpdateBankAccountDto dto, Guid userId);

        Task<bool> DeleteBankAccountAsync(Guid id, Guid userId);
    }
}