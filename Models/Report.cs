using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CropDeal.API.Models
{
    public class Report
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Content { get; set; }

        [Required]
        public Guid GeneratedById { get; set; }

        [Required]
        public Guid GeneratedForId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public User GeneratedBy { get; set; }
        public User GeneratedFor { get; set; }
    }
}