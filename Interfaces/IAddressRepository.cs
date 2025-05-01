using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CropDeal.API.DTOs.Address;
using CropDeal.API.Models;

namespace CropDeal.API.Interfaces
{
    public interface IAddressRepository
    {
    
        Task<IEnumerable<Address>> GetUserAddressesAsync(Guid userId);
        Task<Address> GetAddressByIdAsync(Guid id);
        Task<bool> CreateAddressAsync(CreateAddressDto dto, Guid userId);
        Task<bool> UpdateAddressAsync(UpdateAddressDto dto, Guid userId);
        Task<bool> DeleteAddressAsync(Guid id, Guid userId);
    }
}