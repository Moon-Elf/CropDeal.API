using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CropDeal.API.Models
{
    public enum CropAvailability
    {
        Available,
        OutOfStock
    }

    public class CropListing
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid FarmerId { get; set; }

        [Required]
        public Guid CropId { get; set; }

        [Required]
        public float PricePerKg { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public CropAvailability Status { get; set; }

        public string? ImageUrl { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}