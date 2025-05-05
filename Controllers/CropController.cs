using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CropDeal.API.DTOs.Crop;
using CropDeal.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CropDeal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CropController : ControllerBase
    {
        private readonly ICropRepository _cropRepo;

        public CropController(ICropRepository cropRepo)
        {
            _cropRepo = cropRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var crops = await _cropRepo.GetAllCropsAsync();
            return Ok(crops);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var crop = await _cropRepo.GetCropByIdAsync(id);
            return Ok(crop);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateCropDto dto)
        {
            await _cropRepo.CreateCropAsync(dto);
            return Ok(new {message="Crop created successfully."});
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid id, UpdateCropDto dto)
        {
            await _cropRepo.UpdateCropAsync(dto);
            return Ok(new {message="Crop updated successfully."});
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _cropRepo.DeleteCropAsync(id);
            return Ok(new {message="Crop deleted successfully."});
        }
    }
}