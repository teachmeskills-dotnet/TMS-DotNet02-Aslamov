using System;
using System.Threading.Tasks;
using DataProcessor.API.Common.Interfaces;
using DataProcessor.API.DTO;
using DataProcessor.API.EventBus.Messages;
using DataProcessor.API.EventBus.Produsers;
using EventBus.Contracts.Commands;
using EventBus.Contracts.Common;
using EventBus.Contracts.DTO;
using MassTransit;
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
        private readonly ICommandProducer<IReportDTO> _commandProducer;

        /// <summary>
        /// Constructor of controller for sensor data processing.
        /// </summary>
        /// <param name="dataProcessorService">Service for processing of sensor data.</param>
        /// <param name="bus">Event bus.</param>
        public DataProcessorController( IDataProcessorService dataProcessorService,
                                        ILogger<DataProcessorController> logger,
                                        ICommandProducer<IReportDTO> commandProducer)
        {
            _dataProcessorService = dataProcessorService ?? throw new ArgumentNullException(nameof(dataProcessorService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _commandProducer = commandProducer ?? throw new ArgumentNullException(nameof(commandProducer));
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

            await _commandProducer.Send(report);

            return Ok(report);
        }
    }
}