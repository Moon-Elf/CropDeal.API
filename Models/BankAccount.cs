using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CropDeal.API.Models
{
    public class BankAccount
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string AccountNumber { get; set; }

        [Required]
        public string IFSCCode { get; set; }

        [Required]
        public string BankName { get; set; }

        [Required]
        public string BranchName { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        public User User { get; set; }
    }
}