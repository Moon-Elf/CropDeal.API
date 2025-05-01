using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CropDeal.API.DTOs.Report;
using CropDeal.API.Models;


namespace CropDeal.API.Interfaces
{
    public interface IReportRepository
    {
        Task<IEnumerable<Report>> GetAllReportsAsync();
        Task<IEnumerable<Report>> GetReportsGeneratedByAsync(Guid adminId);
        Task<IEnumerable<Report>> GetReportsForUserAsync(Guid userId);
        Task<Report> GetReportByIdAsync(Guid id);
        Task<bool> CreateReportAsync(CreateReportDto dto, Guid adminId);
    }
}