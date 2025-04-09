using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CropDeal.API.Data;
using CropDeal.API.DTOs.Address;
using CropDeal.API.Exceptions;
using CropDeal.API.Interfaces;
using CropDeal.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CropDeal.API.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly AppDbContext _context;

        public AddressRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Address>> GetUserAddressesAsync(Guid userId)
        {
            return await _context.Addresses.Where(a => a.UserId == userId).ToListAsync();
        }

        public async Task<Address> GetAddressByIdAsync(Guid id)
        {
            var address = await _context.Addresses.FirstOrDefaultAsync(a => a.Id == id);

            if (address == null)
                throw new NotFoundException("Address not found");

            return address;
        }

        public async Task<bool> CreateAddressAsync(CreateAddressDto dto, Guid userId)
        {
            var address = new Address
            {
                UserId = userId,
                Street = dto.Street,
                City = dto.City,
                State = dto.State,
                ZipCode = dto.ZipCode
            };

            var result = await _context.Addresses.AddAsync(address);
            if (result.State != EntityState.Added)
                throw new AppException("Failed to add address");

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAddressAsync(UpdateAddressDto dto, Guid userId)
        {
            var address = await _context.Addresses.FirstOrDefaultAsync(a => a.Id == dto.Id && a.UserId == userId);

            if (address == null)
                throw new NotFoundException("Address not found");

            address.Street = dto.Street;
            address.City = dto.City;
            address.State = dto.State;
            address.ZipCode = dto.ZipCode;

            var result = await _context.SaveChangesAsync();
            if (result == 0)
                throw new AppException("Failed to update address.");
            return true;
        }

        public async Task<bool> DeleteAddressAsync(Guid id, Guid userId)
        {
            var address = await _context.Addresses.FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);
            if (address == null)
                throw new NotFoundException("Address not found");

            _context.Addresses.Remove(address);
            var result = await _context.SaveChangesAsync();
            if (result == 0)
                throw new AppException("Failed to delete address.");
            return true;
        }


    }
}