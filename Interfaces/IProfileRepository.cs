using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CropDeal.API.DTOs;
using CropDeal.API.Models;

namespace CropDeal.API.Interfaces
{
    public interface IProfileRepository
    {
        Task<User> GetProfileAsync(Guid userId);
        Task<bool> UpdateProfileAsync(Guid userId, ProfileDto profileDto);
    }
}