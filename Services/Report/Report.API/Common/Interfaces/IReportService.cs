using Report.API.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Report.API.Common.Interfaces
{
    /// <summary>
    /// Interface to manage reports.
    /// </summary>
    public interface IReportService
    {
        /// <summary>
        /// Register new report.
        /// </summary>
        /// <param name="reportDTO">Report model.</param>
        /// <returns>Report Id and operation status.</returns>
        Task<(int id, bool success)> RegisterNewReportAsync(ReportDTO reportDTO);

        /// <summary>
        /// Get report by identifier.
        /// </summary>
        /// <param name="id">Report identifier.</param>
        /// <returns>Report object.</returns>
        Task<ReportDTO> GetReportByIdAsync(int id);

        /// <summary>
        /// Get all registered reports.
        /// </summary>
        /// <returns>Reports collection.</returns>
        Task<ICollection<ReportDTO>> GetAllReportsAsync();

        /// <summary>
        /// Update report information.
        /// </summary>
        /// <param name="reportDTO">Report object.</param>
        /// <returns>Operation status.</returns>
        Task<bool> UpdateReportAsync(ReportDTO reportDTO);

        /// <summary>
        /// Delete report from application.
        /// </summary>
        /// <param name="id">Report identifier.</param>
        /// <returns>Operation status.</returns>
        Task<bool> DeleteReportByIdAsync(int id);
    }
}