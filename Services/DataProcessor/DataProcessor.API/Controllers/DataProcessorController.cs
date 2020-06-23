using System;
using System.Threading.Tasks;
using DataProcessor.API.Common.Interfaces;
using DataProcessor.API.DTO;
using EventBus.Contracts.Commands;
using EventBus.Contracts.Common;
using EventBus.Contracts.DTO;
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
        private readonly ICommandProducer<IRegisterReport,IReportDTO> _registerReportCommand;

        /// <summary>
        /// Constructor of controller for sensor data processing.
        /// </summary>
        /// <param name="dataProcessorService">Service for processing of sensor data.</param>
        /// <param name="logger">Logging service.</param>
        /// <param name="registerReportCommand">Register report command producer.</param>
        public DataProcessorController( IDataProcessorService dataProcessorService,
                                        ILogger<DataProcessorController> logger,
                                        ICommandProducer<IRegisterReport,IReportDTO> registerReportCommand)
        {
            _dataProcessorService = dataProcessorService ?? throw new ArgumentNullException(nameof(dataProcessorService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _registerReportCommand = registerReportCommand ?? throw new ArgumentNullException(nameof(registerReportCommand));
        }

        // Post: api/dataprocessor
        [Authorize(Roles = "User, Admin")]
        [HttpPost]
        public async Task<IActionResult> ProcessData([FromBody] DataDTO data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (report, success) = await _dataProcessorService.ProcessData(data);
            if (!success)
            {
                return Conflict();
            }

            await _registerReportCommand.Send(report);

            return Ok(report);
        }
    }
}