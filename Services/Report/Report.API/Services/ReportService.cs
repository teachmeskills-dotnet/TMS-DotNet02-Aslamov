using AutoMapper;
using EventBus.Contracts.DTO;
using Microsoft.EntityFrameworkCore;
using Report.API.Common.Constants;
using Report.API.Common.Interfaces;
using Report.API.DTO;
using Report.API.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Report.API.Services
{
    /// <summary>
    /// Define service to manage user reports.
    /// </summary>
    public class ReportService : IReportService
    {
        private readonly IReportContext _reportContext;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor of service for managing reports.
        /// </summary>
        /// <param name="reportContext">Report context.</param>
        /// <param name="mapper">Automapper.</param>
        /// <param name="logger">Logging service.</param>
        public ReportService(IReportContext reportContext,
                             IMapper mapper,
                             ILogger logger)
        {
            _reportContext = reportContext ?? throw new ArgumentNullException(nameof(reportContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public async Task<(int id, bool success)> RegisterNewReportAsync(IReportDTO reportDTO)
        {
            var report = _mapper.Map<IReportDTO, ReportModel>(reportDTO);
            var reportFound = await _reportContext.Reports.FirstOrDefaultAsync(r => r.Date == reportDTO.Date && r.RecordId == reportDTO.RecordId);

            if (reportFound != null)
            {
                _logger.Error(ReportConstants.REPORT_ALREADY_EXIST);
                return (0, false);
            }

            await _reportContext.Reports.AddAsync(report);
            await _reportContext.SaveChangesAsync(new CancellationToken());

            var id = report.Id;

            return (id, true);
        }

        /// <inheritdoc/>
        public async Task<ReportDTO> GetReportByIdAsync(int id)
        {
            var report = await _reportContext.Reports.FirstOrDefaultAsync(r => r.Id == id);
            if(report == null)
            {
                return null;
            }

            var reportDTO = _mapper.Map<ReportModel, ReportDTO>(report);

            return reportDTO;
        }

        /// <inheritdoc/>
        public async Task<ICollection<ReportDTO>> GetAllReportsAsync()
        {
            var reports = await _reportContext.Reports.ToListAsync();
            var collectionOfReportDTO = _mapper.Map<ICollection<ReportModel>, ICollection<ReportDTO>>(reports);

            return collectionOfReportDTO;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateReportAsync(ReportDTO reportDTO)
        {
            var report = await _reportContext.Reports.FirstOrDefaultAsync(r => r.Id == reportDTO.Id);

            if (report == null)
            {
                return false;
            }

            report.RecordId = reportDTO.RecordId;
            report.Date = reportDTO.Date;
            report.DataType = reportDTO.DataType;
            report.HealthStatus = reportDTO.HealthStatus;
            report.HealthDescription = reportDTO.HealthDescription;
            report.Diseases = reportDTO.Diseases;
            report.Accuracy = reportDTO.Accuracy;

            _reportContext.Update(report);
            await _reportContext.SaveChangesAsync(new CancellationToken());

            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteReportByIdAsync(int id)
        {
            var reportFound = await _reportContext.Reports.FirstOrDefaultAsync(r => r.Id == id);
            if (reportFound == null)
            {
                _logger.Error(ReportConstants.REPORT_NOT_FOUND);
                return false;
            }

            _reportContext.Remove(reportFound);
            await _reportContext.SaveChangesAsync(new CancellationToken());

            return true;
        }
    }
}