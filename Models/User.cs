using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using CropDeal.API.Enums;

namespace CropDeal.API.Models
{
    public class User : IdentityUser<Guid>
    {
        [Required]
        public string Name { get; set; }
        public UserStatus Status { get; set; } = UserStatus.Active;

        public float? AverageRating { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        public ICollection<Address>? Addresses { get; set; }
        public ICollection<BankAccount>? BankAccounts { get; set; }
        public ICollection<CropListing>? CropListings { get; set; }
        public ICollection<Subscription>? Subscriptions { get; set; }
        public ICollection<Transaction>? Transactions { get; set; }
        public ICollection<Review>? GivenReviews { get; set; }
        public ICollection<Review>? ReceivedReviews { get; set; }
        public ICollection<Report>? GeneratedReports { get; set; }
        public ICollection<Report>? ReceivedReports { get; set; }
    }
}