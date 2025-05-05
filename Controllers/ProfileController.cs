using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CropDeal.API.DTOs;
using CropDeal.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CropDeal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileController(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var user = await _profileRepository.GetProfileAsync(Guid.Parse(userId));
            if (user == null)
                return NotFound();

            var profile = new ProfileDto
            {
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };

            return Ok(profile);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProfile(ProfileDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var updated = await _profileRepository.UpdateProfileAsync(Guid.Parse(userId), dto);
            if (!updated)
                return BadRequest("Failed to update profile.");

            return Ok(new {message="Profile updated successfully"});
        }

    }
}