using System;
using System.Threading.Tasks;
using DataProcessor.API.Common.Constants;
using DataProcessor.API.Common.Interfaces;
using DataProcessor.API.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DataProcessor.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class DataProcessorController : ControllerBase
    {
        private readonly IDataProcessorService _dataProcessorService;
        private readonly ILogger<DataProcessorController> _logger;

        /// <summary>
        /// Constructor of controller for sensor data processing.
        /// </summary>
        /// <param name="dataProcessorService">Service for processing of sensor data.</param>
        /// <param name="logger">Logging service.</param>
        public DataProcessorController( IDataProcessorService dataProcessorService,
                                        ILogger<DataProcessorController> logger)
        {
            _dataProcessorService = dataProcessorService ?? throw new ArgumentNullException(nameof(dataProcessorService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // Post: api/dataprocessor
        [Authorize(Roles = "User, Admin")]
        [HttpPost]
        public async Task<IActionResult> ProcessData([FromBody] RecordDTO record)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (report, success) = await _dataProcessorService.ProcessData(record);
            if (!success)
            {
                _logger.LogWarning(DataProcessorConstants.DATA_PROCESSING_CONFLICT);
                return Conflict();
            }

            _logger.LogInformation(DataProcessorConstants.DATA_PROCESSING_SUCCESS);
            return Ok(report);
        }
    }
}