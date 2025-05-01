using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CropDeal.API.DTOs.Review
{
    public class CreateReviewDto
    {
        [Required]
        public Guid FarmerId { get; set; }

        [Required]
        public Guid TransactionId { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }

        public string? Comment { get; set; }

    }
}