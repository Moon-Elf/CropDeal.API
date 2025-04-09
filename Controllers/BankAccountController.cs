using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CropDeal.API.DTOs.BankAccount;
using CropDeal.API.Interfaces;
using CropDeal.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CropDeal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Farmer,Dealer")]
    public class BankAccountController : ControllerBase
    {
        private readonly IBankAccountRepository _bankAccountRepo;
        private readonly UserManager<User> _userManager;

        public BankAccountController(IBankAccountRepository bankAccountRepo, UserManager<User> userManager)
        {
            _bankAccountRepo = bankAccountRepo;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetMyBankAccounts()
        {
            var bankAccount = await _bankAccountRepo.GetUserBankAccountsAsync(UserId());

            var result = bankAccount.Select(b => new BankAccountDto
            {
                Id = b.Id,
                AccountNumber = b.AccountNumber,
                IFSCCode = b.IFSCCode,
                BankName = b.BankName,
                BranchName = b.BranchName
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBankAccount(Guid id)
        {
            var bankAccount = await _bankAccountRepo.GetBankAccountByIdAsync(id, UserId());
            var result = new BankAccountDto
            {
                Id = bankAccount.Id,
                AccountNumber = bankAccount.AccountNumber,
                IFSCCode = bankAccount.IFSCCode,
                BankName = bankAccount.BankName,
                BranchName = bankAccount.BranchName
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddBankAccount(CreateBankAccountDto dto)
        {
            await _bankAccountRepo.CreateBankAccountAsync(dto, UserId());
            return Ok("Bank Account added successfully.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBankAccount(UpdateBankAccountDto dto)
        {
            await _bankAccountRepo.UpdateBankAccountAsync(dto, UserId());
            return Ok("Bank Account updated successfully");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBankAccount(Guid id)
        {
            await _bankAccountRepo.DeleteBankAccountAsync(id, UserId());
            return Ok("Bank Account deleted successfully");
        }

        private Guid UserId()
        {
            return Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        }
    }
}