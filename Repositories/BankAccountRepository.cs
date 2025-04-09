using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CropDeal.API.Data;
using CropDeal.API.DTOs.BankAccount;
using CropDeal.API.Exceptions;
using CropDeal.API.Interfaces;
using CropDeal.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CropDeal.API.Repositories
{
    public class BankAccountRepository : IBankAccountRepository
    {
        private readonly AppDbContext _context;

        public BankAccountRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BankAccount>> GetUserBankAccountsAsync(Guid userId)
        {
            return await _context.BankAccounts.Where(b => b.UserId == userId).ToListAsync();
        }

        public async Task<BankAccount> GetBankAccountByIdAsync(Guid id, Guid userId)
        {
            var bankAccount = await _context.BankAccounts.FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId);

            if (bankAccount == null)
                throw new NotFoundException("Bank Account not found");

            return bankAccount;
        }

        public async Task<bool> CreateBankAccountAsync(CreateBankAccountDto dto, Guid userId)
        {
            var bankAccount = new BankAccount
            {
                UserId = userId,
                AccountNumber = dto.AccountNumber,
                IFSCCode = dto.IFSCCode,
                BankName = dto.BankName,
                BranchName = dto.BranchName
            };

            await _context.BankAccounts.AddAsync(bankAccount);
            var result = await _context.SaveChangesAsync();
            if (result == 0)
                throw new AppException("Failed to add bank account");

            return true;
        }

        public async Task<bool> UpdateBankAccountAsync(UpdateBankAccountDto dto, Guid userId)
        {
            var bankAccount = await _context.BankAccounts.FirstOrDefaultAsync(b => b.Id == dto.Id && b.UserId == userId);

            if (bankAccount == null)
                throw new NotFoundException("Bank Account not found");

            bankAccount.AccountNumber = dto.AccountNumber;
            bankAccount.IFSCCode = dto.IFSCCode;
            bankAccount.BankName = dto.BankName;
            bankAccount.BranchName = dto.BranchName;

            var result = await _context.SaveChangesAsync();
            if (result == 0)
                throw new AppException("Failed to update bank account.");
            return true;

        }

        public async Task<bool> DeleteBankAccountAsync(Guid id, Guid userId)
        {
            var bankAccount = await _context.BankAccounts.FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId);

            if (bankAccount == null)
                throw new NotFoundException("Bank Account not found");

            _context.BankAccounts.Remove(bankAccount);
            var result = await _context.SaveChangesAsync();
            if (result == 0)
                throw new AppException("Failed to delete bank account.");
            return true;
        }
    }
}