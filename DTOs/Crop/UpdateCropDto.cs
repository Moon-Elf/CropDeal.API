using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CropDeal.API.Models;

namespace CropDeal.API.DTOs.Crop
{
    public class UpdateCropDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public CropTypeEnum Type { get; set; }
    }
}