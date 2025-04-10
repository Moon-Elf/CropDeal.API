using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CropDeal.API.DTOs.CropListing;
using CropDeal.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CropDeal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CropListingController : ControllerBase
    {
        private readonly ICropListingRepository _listingRepo;
        private readonly IWebHostEnvironment _env;

        public CropListingController(ICropListingRepository listingRepo, IWebHostEnvironment env)
        {
            _listingRepo = listingRepo;
            _env = env;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var listings = await _listingRepo.GetAllListingsAsync();
            return Ok(listings);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(Guid id)
        {
            var listing = await _listingRepo.GetListingByIdAsync(id);
            return Ok(listing);
        }

        [HttpGet("my")]
        [Authorize(Roles = "Farmer")]
        public async Task<IActionResult> GetFarmerListings()
        {
            var listings = await _listingRepo.GetFarmerListingsAsync(UserId());
            return Ok(listings);
        }

        [HttpPost]
        [Authorize(Roles = "Farmer")]
        public async Task<IActionResult> Create([FromForm] CreateCropListingDto dto)
        {
            string imagePath = await SaveImageAsync(dto.Image);

            await _listingRepo.CreateListingAsync(dto, UserId(), imagePath);
            return Ok("Crop listing created successfully.");
        }

        [HttpPut]
        [Authorize(Roles = "Farmer")]
        public async Task<IActionResult> Update([FromForm] UpdateCropListingDto dto)
        {
            string? imagePath = null;
            if (dto.Image != null)
                imagePath = await SaveImageAsync(dto.Image);

            await _listingRepo.UpdateListingAsync(dto, UserId(), imagePath);
            return Ok("Crop listing updated successfully.");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Farmer")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _listingRepo.DeleteListingAsync(id, UserId());
            return Ok("Crop listing deleted.");
        }

        private Guid UserId()
        {
            return Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        }

        private async Task<string> SaveImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            var uploadsFolder = Path.Combine(_env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), "images");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var path = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/images/{fileName}";
        }

    }
}