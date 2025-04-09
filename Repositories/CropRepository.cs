using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CropDeal.API.Data;
using CropDeal.API.DTOs.Crop;
using CropDeal.API.Exceptions;
using CropDeal.API.Interfaces;
using CropDeal.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CropDeal.API.Repositories
{
    public class CropRepository : ICropRepository
    {
        private readonly AppDbContext _context;

        public CropRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Crop>> GetAllCropsAsync()
        {
            return await _context.Crops.ToListAsync();
        }

        public async Task<Crop> GetCropByIdAsync(Guid id)
        {
            var crop = await _context.Crops.FindAsync(id);
            if (crop == null)
                throw new NotFoundException("Crop not found");

            return crop;
        }

        public async Task<bool> CreateCropAsync(CreateCropDto dto)
        {
            var exists = await _context.Crops.AnyAsync(c => c.Name == dto.Name);
            if (exists)
                throw new AppException("Crop with the same name already exists.");

            var crop = new Crop
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Type = dto.Type,
                CreatedAt = DateTime.UtcNow
            };

            _context.Crops.Add(crop);
            var result = await _context.SaveChangesAsync();
            if (result == 0)
                throw new AppException("Failed to create crop.");
            return true;
        }

        public async Task<bool> UpdateCropAsync(UpdateCropDto dto)
        {
            var crop = await _context.Crops.FindAsync(dto.Id);
            if (crop == null)
                throw new NotFoundException("Crop not found.");

            crop.Name = dto.Name;
            crop.Type = dto.Type;

            var result = await _context.SaveChangesAsync();
            if (result == 0)
                throw new AppException("Failed to update crop.");
            return true;
        }

        public async Task<bool> DeleteCropAsync(Guid id)
        {
            var crop = await _context.Crops.FindAsync(id);
            if (crop == null)
                throw new NotFoundException("Crop not found");

            _context.Crops.Remove(crop);
            var result = await _context.SaveChangesAsync();
            if (result == 0)
                throw new AppException("Failed to delete crop.");
            return true;
        }
    }
}