using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Report.API.Common.Constants;
using Report.API.Common.Interfaces;
using Report.API.DTO;
using Serilog;

namespace Report.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor of reports controller.
        /// </summary>
        /// <param name="reportService">Service to manage reports.</param>
        /// <param name="logger">Logging service.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public ReportsController(IReportService reportService, ILogger logger)
        {
            _reportService = reportService ?? throw new ArgumentNullException(nameof(reportService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET: api/reports
        [Authorize(Roles = "User, Admin")]
        [HttpGet]
        public async Task<ICollection<ReportDTO>> GetReports()
        {
            var reports = await _reportService.GetAllReportsAsync();
            var count = reports.Count;

            _logger.Information($"{count} {ReportConstants.GET_REPORTS}");

            return reports;
        }

        // GET: api/reports/{id}
        [Authorize(Roles = "User, Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReport([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var report = await _reportService.GetReportByIdAsync(id);
            if (report == null)
            {
                _logger.Warning($"{id} {ReportConstants.REPORT_NOT_FOUND}");
                return NotFound(id);
            }

            _logger.Information($"{report.Id} {ReportConstants.GET_FOUND_REPORT}");
            return Ok(report);
        }

        // POST: api/reports
        [Authorize(Roles = "User, Admin")]
        [HttpPost]
        public async Task<IActionResult> RegisterNewReport([FromBody] ReportDTO reportDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (id, success) = await _reportService.RegisterNewReportAsync(reportDTO);
            if (!success)
            {
                _logger.Warning($"{id} {ReportConstants.ADD_REPORT_CONFLICT}");
                return Conflict(id);
            }

            reportDTO.Id = id;

            _logger.Information($"{reportDTO.Id} {ReportConstants.ADD_REPORT_SUCCESS}");
            return CreatedAtAction(nameof(RegisterNewReport), reportDTO);
        }

        // PUT: api/reports/{id}
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReport([FromBody] ReportDTO reportDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (reportDTO.Id <= 0)
            {
                return BadRequest(reportDTO.Id);
            }

            var reportFound = await _reportService.GetReportByIdAsync(reportDTO.Id);
            if (reportFound == null)
            {
                _logger.Warning($"{reportDTO.Id} {ReportConstants.REPORT_NOT_FOUND}");
                return NotFound(reportDTO.Id);
            }

            var success = await _reportService.UpdateReportAsync(reportDTO);
            if (!success)
            {
                _logger.Warning($"{reportDTO.Id} {ReportConstants.UPDATE_REPORT_CONFLICT}");
                return Conflict();
            }

            _logger.Information($"{reportDTO.Id} {ReportConstants.UPDATE_REPORT_SUCCESS}");
            return Ok(reportDTO);
        }

        // DELETE: api/reports/{id}
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReport([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = await _reportService.DeleteReportByIdAsync(id);
            if (!success)
            {
                _logger.Warning($"{id} {ReportConstants.REPORT_NOT_FOUND}");
                return NotFound(id);
            }

            _logger.Information($"{id} {ReportConstants.DELETE_REPORT_SUCCESS}");
            return Ok(id);
        }
    }
}