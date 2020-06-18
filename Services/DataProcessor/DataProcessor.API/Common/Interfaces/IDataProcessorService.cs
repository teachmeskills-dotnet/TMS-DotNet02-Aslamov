﻿using System.Threading.Tasks;
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
    }
}