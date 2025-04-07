using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CropDeal.API.Models
{
    public enum CropTypeEnum
    {
        Fruit,
        Vegetable,
        Grain
    }

    public class Crop
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public CropTypeEnum Type { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ICollection<Subscription>? Subscriptions { get; set; }
        public ICollection<CropListing>? CropListings { get; set; }
    }
}