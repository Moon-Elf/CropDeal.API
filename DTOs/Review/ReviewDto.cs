using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CropDeal.API.DTOs.Review
{
    public class ReviewDto
    {
        public Guid Id { get; set; }
        public Guid DealerId { get; set; }
        public Guid FarmerId { get; set; }
        public Guid TransactionId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}