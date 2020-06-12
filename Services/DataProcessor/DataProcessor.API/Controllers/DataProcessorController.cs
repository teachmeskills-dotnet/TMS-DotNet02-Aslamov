using System;
using System.Threading.Tasks;
using DataProcessor.API.Common.Interfaces;
using DataProcessor.API.DTO;
using Microsoft.AspNetCore.Mvc;

namespace DataProcessor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataProcessorController : ControllerBase
    {
        private readonly IDataProcessorService _dataProcessorService;

        /// <summary>
        /// Constructor of controller for sensor data processing.
        /// </summary>
        /// <param name="dataProcessorService">Service for processing of sensor data.</param>
        public DataProcessorController(IDataProcessorService dataProcessorService)
        {
            _dataProcessorService = dataProcessorService ?? throw new ArgumentNullException();
        }

        // Post: api/dataprocessor
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

            return Ok(report);
        }
    }
}