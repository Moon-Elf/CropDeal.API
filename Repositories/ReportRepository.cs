using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CropDeal.API.Data;
using CropDeal.API.DTOs.Report;
using CropDeal.API.Exceptions;
using CropDeal.API.Interfaces;
using CropDeal.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CropDeal.API.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly AppDbContext _context;

        public ReportRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Report>> GetAllReportsAsync()
        {
            return await _context.Reports.ToListAsync();
        }
        
        public async Task<IEnumerable<Report>> GetReportsGeneratedByAsync(Guid adminId)
        {
            return await _context.Reports
                .Where(r => r.GeneratedById == adminId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Report>> GetReportsForUserAsync(Guid userId)
        {
            return await _context.Reports
                .Where(r => r.GeneratedForId == userId)
                .ToListAsync();
        }

        public async Task<Report> GetReportByIdAsync(Guid id)
        {
            var report = await _context.Reports.FindAsync(id);
            if (report == null)
                throw new NotFoundException("Report not found");
            return report;
        }

        public async Task<bool> CreateReportAsync(CreateReportDto dto, Guid adminId)
        {
            var report = new Report
            {
                Title = dto.Title,
                Content = dto.Content,
                GeneratedForId = dto.GeneratedForId,
                GeneratedById = adminId,
                CreatedAt = DateTime.UtcNow
            };

            await _context.Reports.AddAsync(report);
            var result = await _context.SaveChangesAsync();
            if (result == 0)
                throw new AppException("Failed to create report");
            return true;
        }

    }
}