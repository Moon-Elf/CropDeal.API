using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CropDeal.API.Data;
using CropDeal.API.DTOs;
using CropDeal.API.Interfaces;
using CropDeal.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CropDeal.API.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly AppDbContext _context;

        public ProfileRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetProfileAsync(Guid userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<bool> UpdateProfileAsync(Guid userId, ProfileDto profileDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return false;

            user.Name = profileDto.Name;
            user.PhoneNumber = profileDto.PhoneNumber;
            user.Email = profileDto.Email;

            _context.Users.Update(user);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}