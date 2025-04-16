using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CropDeal.API.Enums;

namespace CropDeal.API.Models
{
    public class Crop
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public CropType Type { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ICollection<Subscription>? Subscriptions { get; set; }
        public ICollection<CropListing>? CropListings { get; set; }
    }
}