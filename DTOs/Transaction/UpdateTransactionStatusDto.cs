using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CropDeal.API.Enums;
namespace CropDeal.API.DTOs.Transaction
{
    public class UpdateTransactionStatusDto
    {
        [Required]
        public Guid TransactionId { get; set; }

        [Required]
        public TransactionStatus Status { get; set; }
    }
}