using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CropDeal.API.Enums;

namespace CropDeal.API.DTOs.Crop
{
    public class CreateCropDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public CropType Type { get; set; }
    }
}