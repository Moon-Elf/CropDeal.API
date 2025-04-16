using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CropDeal.API.DTOs.Transaction
{
    public class CreateTransactionDto
    {
        [Required]
        public Guid ListingId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage ="Quantity must be at least 1")]
        public int Quantity { get; set; }

        
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Final price per kg must be greater than 0")]
        public float FinalPricePerKg { get; set; }
    }
}