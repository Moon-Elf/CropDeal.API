using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CropDeal.API.DTOs.Subscription
{
    public class CreateSubscriptionDto
    {
        [Required]
        public Guid CropId { get; set; }
    }
}