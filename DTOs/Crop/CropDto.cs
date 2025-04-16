using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CropDeal.API.Enums;

namespace CropDeal.API.DTOs.Crop
{
    public class CropDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public CropType Type { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}