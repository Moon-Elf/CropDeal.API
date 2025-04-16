using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CropDeal.API.Enums;

namespace CropDeal.API.DTOs.Transaction
{
    public class TransactionDto
    {
        public Guid Id { get; set; }
        public Guid DealerId { get; set; }
        public Guid ListingId { get; set; }

        public int Quantity { get; set; }
        public float FinalPricePerKg { get; set; }
        public float TotalPrice { get; set; }

        public TransactionStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}