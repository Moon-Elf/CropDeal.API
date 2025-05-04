using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CropDeal.API.DTOs.Address;
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
    public class AddressController : ControllerBase
    {
        private readonly IAddressRepository _addressRepo;
        private readonly UserManager<User> _userManager;

        public AddressController(IAddressRepository addressRepo, UserManager<User> userManager)
        {
            _addressRepo = addressRepo;
            _userManager = userManager;
        }


        [HttpGet]
        public async Task<IActionResult> GetMyAddresses()
        {
            var addresses = await _addressRepo.GetUserAddressesAsync(UserId());

            var result = addresses.Select(a => new AddressDto
            {
                Id = a.Id,
                Street = a.Street,
                City = a.City,
                State = a.State,
                ZipCode = a.ZipCode
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAddress(Guid id)
        {
            var address = await _addressRepo.GetAddressByIdAsync(id);

            var result = new AddressDto
            {
                Id = address.Id,
                Street = address.Street,
                City = address.City,
                State = address.State,
                ZipCode = address.ZipCode
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddAddress(CreateAddressDto dto)
        {
            await _addressRepo.CreateAddressAsync(dto, UserId());
            return Ok(new {message="Address added successfully."});
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAddress(UpdateAddressDto dto)
        {
            await _addressRepo.UpdateAddressAsync(dto, UserId());
            return Ok(new {message="Address updated successfully."});
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAddress(Guid id)
        {
            await _addressRepo.DeleteAddressAsync(id, UserId());
            return Ok(new {message="Address deleted successfully."});
        }

        private Guid UserId()
        {
            return Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        }
    }
}