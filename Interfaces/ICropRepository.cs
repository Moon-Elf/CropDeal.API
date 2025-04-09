using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CropDeal.API.DTOs.Crop;
using CropDeal.API.Models;

namespace CropDeal.API.Interfaces
{
    public interface ICropRepository
    {
        Task<IEnumerable<Crop>> GetAllCropsAsync();
        Task<Crop> GetCropByIdAsync(Guid id);
        Task<bool> CreateCropAsync(CreateCropDto dto);
        Task<bool> UpdateCropAsync(UpdateCropDto dto);
        Task<bool> DeleteCropAsync(Guid id);
    }
}