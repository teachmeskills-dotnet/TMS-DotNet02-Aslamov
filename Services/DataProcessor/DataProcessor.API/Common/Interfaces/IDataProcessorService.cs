using System.Threading.Tasks;
using DataProcessor.API.DTO;
using Microsoft.AspNetCore.Mvc;

namespace DataProcessor.API.Common.Interfaces
{
    /// <summary>
    /// Interface for processing sernsor records.
    /// </summary>
    public interface IDataProcessorService
    {
        /// <summary>
        /// Process sensor data and generate report.
        /// </summary>
        /// <param name="dataDTO">Sensor data.</param>
        /// <returns>Processing report.</returns>
        Task<(ReportDTO report, bool success)> ProcessData(DataDTO dataDTO);

        /// <summary>
        /// Add data processing report to cache.
        /// </summary>
        /// <returns>Operation result.</returns>
        Task<IActionResult> AddReportToCache();

        /// <summary>
        /// Get report from cache.
        /// </summary>
        /// <param name="id">Report Identifier.</param>
        /// <returns>Report DTO.</returns>
        Task<ReportDTO> GetReportFromCache(int id);

        /// <summary>
        /// Delete report data from cache.
        /// </summary>
        /// <param name="id">Report Identifier.</param>
        /// <returns>Operation result.</returns>
        Task<IActionResult> DeleteReportFromCache(int id);

        /// <summary>
        /// Clean cache data.
        /// </summary>
        /// <returns>Operation result.</returns>
        Task<IActionResult> CleanAllCache();
    }
}