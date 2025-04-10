using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CropDeal.API.DTOs.Subscription
{
    public class SubscriptionDto
    {
        public Guid Id { get; set; }
        public Guid CropId { get; set; }
        public string CropName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}