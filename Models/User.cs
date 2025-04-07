using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CropDeal.API.Models
{
    public enum UserRole
    {
        Farmer,
        Dealer,
        Admin
    }

    public enum UserStatus
    {
        Active,
        Inactive
    }

    public class User : IdentityUser<Guid>
    {
        [Required]
        public string Name { get; set; }

        public UserRole Role { get; set; }
        public UserStatus Status { get; set; } = UserStatus.Active;

        public Guid? AddressId { get; set; }
        public Guid? BankAccountId { get; set; }
        public float? AverageRating { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;


    }
}