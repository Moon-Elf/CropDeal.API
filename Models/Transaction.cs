using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CropDeal.API.Enums;

namespace CropDeal.API.Models
{
    public class Transaction
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid DealerId { get; set; }

        [Required]
        public Guid ListingId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public float FinalPricePerKg { get; set; }

        [Required]
        public float TotalPrice { get; set; }

        [Required]
        public TransactionStatus Status { get; set; } = TransactionStatus.Pending;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public User Dealer { get; set; }
        public CropListing PurchasingCrop { get; set; }
    }
}