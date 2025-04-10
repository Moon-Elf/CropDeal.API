using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CropDeal.API.Interfaces;
using CropDeal.API.Data;
using CropDeal.API.DTOs.CropListing;
using Microsoft.EntityFrameworkCore;
using CropDeal.API.Exceptions;
using CropDeal.API.Models;

namespace CropDeal.API.Repositories
{
    public class CropListingRepository : ICropListingRepository
    {
        private readonly AppDbContext _context;

        public CropListingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CropListingDto>> GetAllListingsAsync()
        {
            var listings = await _context.CropListings
                .Include(cl => cl.Crop)
                .Include(cl => cl.Farmer)
                .ToListAsync();

            return listings.Select(MapToDto);
        }

        public async Task<CropListingDto> GetListingByIdAsync(Guid id)
        {
            var listing = await _context.CropListings
                .Include(cl => cl.Crop)
                .Include(cl => cl.Farmer)
                .FirstOrDefaultAsync(cl => cl.Id == id);

            if (listing == null)
                throw new NotFoundException("Crop listing not found.");

            return MapToDto(listing);
        }

        public async Task<IEnumerable<CropListingDto>> GetFarmerListingsAsync(Guid farmerId)
        {
            var listings = await _context.CropListings
                .Where(cl => cl.FarmerId == farmerId)
                .Include(cl => cl.Crop)
                .ToListAsync();

            return listings.Select(MapToDto);
        }

        public async Task<bool> CreateListingAsync(CreateCropListingDto dto, Guid farmerId, string imagePath)
        {
            var crop = await _context.Crops.FindAsync(dto.CropId);
            if (crop == null)
                throw new NotFoundException("Crop not found.");

            var listing = new CropListing
            {
                Id = Guid.NewGuid(),
                CropId = dto.CropId,
                FarmerId = farmerId,
                PricePerKg = dto.PricePerKg,
                Quantity = dto.Quantity,
                Description = dto.Description,
                ImageUrl = imagePath,
                Status = dto.Quantity > 0 ? CropAvailability.Available : CropAvailability.OutOfStock,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.CropListings.Add(listing);
            var result = await _context.SaveChangesAsync();
            if (result == 0)
                throw new AppException("Failed to create crop listing.");

            return true;
        }

        public async Task<bool> UpdateListingAsync(UpdateCropListingDto dto, Guid farmerId, string? imagePath)
        {
            var listing = await _context.CropListings.FirstOrDefaultAsync(cl => cl.Id == dto.Id && cl.FarmerId == farmerId);
            if (listing == null)
                throw new NotFoundException("Crop listing not found.");

            listing.PricePerKg = dto.PricePerKg;
            listing.Quantity = dto.Quantity;
            listing.Description = dto.Description;
            listing.Status = dto.Quantity > 0 ? CropAvailability.Available : CropAvailability.OutOfStock;
            listing.UpdatedAt = DateTime.UtcNow;

            if (!string.IsNullOrEmpty(imagePath))
                listing.ImageUrl = imagePath;

            var result = await _context.SaveChangesAsync();
            if (result == 0)
                throw new AppException("Failed to update crop listing.");

            return true;
        }

        public async Task<bool> DeleteListingAsync(Guid id, Guid farmerId)
        {
            var listing = await _context.CropListings.FirstOrDefaultAsync(cl => cl.Id == id && cl.FarmerId == farmerId);
            if (listing == null)
                throw new NotFoundException("Crop listing not found.");

            _context.CropListings.Remove(listing);
            var result = await _context.SaveChangesAsync();
            if (result == 0)
                throw new AppException("Failed to delete crop listing.");

            return true;
        }

        private CropListingDto MapToDto(CropListing listing)
        {
            return new CropListingDto
            {
                Id = listing.Id,
                CropId = listing.CropId,
                CropName = listing.Crop?.Name ?? "Unknown",
                PricePerKg = listing.PricePerKg,
                Quantity = listing.Quantity,
                Description = listing.Description,
                ImageUrl = listing.ImageUrl,
                Status = listing.Status,
                CreatedAt = listing.CreatedAt,
                UpdatedAt = listing.UpdatedAt
            };
        }

    }
}