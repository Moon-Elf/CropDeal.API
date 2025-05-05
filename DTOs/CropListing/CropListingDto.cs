using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CropDeal.API.Enums;

namespace CropDeal.API.DTOs.CropListing
{
    public class CropListingDto
    {
        public Guid Id { get; set; }
        public Guid CropId { get; set; }
        public string CropName { get; set; }
        public float PricePerKg { get; set; }
        public int Quantity { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public CropAvailability Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Guid FarmerId { get; set; }
    }
}