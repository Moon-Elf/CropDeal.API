using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using CropDeal.API.DTOs.Report;
using CropDeal.API.Interfaces;
using CropDeal.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CropDeal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class ReportController : ControllerBase
    {
        private readonly IReportRepository _reportRepo;

        public ReportController(IReportRepository reportRepo)
        {
            _reportRepo = reportRepo;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReport(CreateReportDto dto)
        {
            var adminId = UserId();
            await _reportRepo.CreateReportAsync(dto, adminId);
            return Ok("Report created successfully.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReports()
        {
            var reports = await _reportRepo.GetAllReportsAsync();
            var result = reports.Select(r => new ReportDto
            {
                Id = r.Id,
                Title = r.Title,
                Content = r.Content,
                GeneratedById = r.GeneratedById,
                GeneratedForId = r.GeneratedForId,
                CreatedAt = r.CreatedAt
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReportById(Guid id)
        {
            var report = await _reportRepo.GetReportByIdAsync(id);
            var result = new ReportDto
            {
                Id = report.Id,
                Title = report.Title,
                Content = report.Content,
                GeneratedById = report.GeneratedById,
                GeneratedForId = report.GeneratedForId,
                CreatedAt = report.CreatedAt
            };
            return Ok(result);
        }

        private Guid UserId()
        {
            return Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        }

    }
}