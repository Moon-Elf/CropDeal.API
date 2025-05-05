using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CropDeal.API.DTOs.User;
using CropDeal.API.Interfaces;
using CropDeal.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CropDeal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Farmer,Dealer,Admin")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        
        public UsersController(IUserRepository userRepository, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        {
            var users = await _userRepository.GetAllUsersAsync();

            var userDtos = new List<UserDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                userDtos.Add(new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    PhoneNumber = user.PhoneNumber,
                    AverageRating = user.AverageRating,
                    Email = user.Email,
                    Role = roles.FirstOrDefault() ?? "Unknown"
                });
            }

            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetById(Guid id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null) return NotFound();

            var roles = await _userManager.GetRolesAsync(user);

            var userDto = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                AverageRating = user.AverageRating,
                Email = user.Email,
                Role = roles.FirstOrDefault() ?? "Unknown"
            };

            return Ok(userDto);
        }
    }
}