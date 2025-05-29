using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CropDeal.API.Enums;

namespace CropDeal.API.DTOs.User
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public float? AverageRating { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public UserStatus Status { get; set; }

    }
}