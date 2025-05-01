using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CropDeal.API.DTOs.Report
{
    public class ReportDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid GeneratedById { get; set; }
        public Guid GeneratedForId { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}