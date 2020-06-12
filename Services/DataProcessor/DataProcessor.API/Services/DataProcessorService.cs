using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DataProcessor.API.Common.Enums;
using DataProcessor.API.Common.Interfaces;
using DataProcessor.API.DTO;
using Microsoft.AspNetCore.Mvc;

namespace DataProcessor.API.Services
{
    /// <summary>
    /// Service for processing sensor data and generate reports on patient health.
    /// </summary>
    public class DataProcessorService : IDataProcessorService
    {
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor of dataprocessor service.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public DataProcessorService(IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException();
        }

        /// <inheritdoc/>
        public Task<IActionResult> AddReportToCache()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<IActionResult> CleanAllCache()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<IActionResult> DeleteReportFromCache(int id)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<ReportDTO> GetReportFromCache(int id)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<(ReportDTO report, bool success)> ProcessData(DataDTO dataDTO)
        {
            if(dataDTO == null)
            {
                return (null, false);
            }
            
            var healthStatus = await GetHealthStatus();

            var report = _mapper.Map<DataDTO, ReportDTO>(dataDTO);
            report.HealthStatus = healthStatus;

            return (report, true);
        }
        
        // Randomly generate health status of the patient.
        private Task<string> GetHealthStatus()
        {
            var statusGenerator = new Random();
            var status = Enum.GetName(typeof(HealthStatus), statusGenerator.Next(0, 3));

            // Imitation of processing time. 
            Thread.Sleep(5000); 

            return Task.FromResult(status);
        }
    }
}