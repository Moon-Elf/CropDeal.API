using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CropDeal.API.DTOs.CropListing;

namespace CropDeal.API.Interfaces
{
    public interface ICropListingRepository
    {
        Task<IEnumerable<CropListingDto>> GetAllListingsAsync();
        Task<CropListingDto> GetListingByIdAsync(Guid id);
        Task<IEnumerable<CropListingDto>> GetFarmerListingsAsync(Guid farmerId);
        Task<bool> CreateListingAsync(CreateCropListingDto dto, Guid farmerId, string imagePath);
        Task<bool> UpdateListingAsync(UpdateCropListingDto dto, Guid farmerId, string? imagePath);
        Task<bool> DeleteListingAsync(Guid id, Guid farmerId);
    }
}