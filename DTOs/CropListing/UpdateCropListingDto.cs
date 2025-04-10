using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CropDeal.API.DTOs.CropListing
{
    public class UpdateCropListingDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public float PricePerKg { get; set; }

        [Required]
        public int Quantity { get; set; }

        public string? Description { get; set; }

        public IFormFile? Image { get; set; }

    }
}