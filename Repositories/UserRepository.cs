using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CropDeal.API.Data;
using CropDeal.API.Enums;
using CropDeal.API.Exceptions;
using CropDeal.API.Interfaces;
using CropDeal.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CropDeal.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<bool> ToggleStatus(Guid id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                throw new NotFoundException("User Not Found");
            }

            if (user.Status == UserStatus.Active)
            {
                user.Status = UserStatus.Inactive;
            }
            else
            {
                user.Status = UserStatus.Active;
            }

            var result = await _context.SaveChangesAsync();
            if (result == 0)
                throw new AppException("Failed to update user status.");
                
            return true;
        }
    }
}