using DataSource.Application.DTO;
using DataSource.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace DataSource.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataSourceController : ControllerBase
    {
        private readonly IDataSourceService _dataSourceService;
        private readonly ILogger<DataSourceController> _logger;

        /// <summary>
        /// Constructor of data source controller.
        /// </summary>
        /// <param name="dataSourceService">Service for data generation.</param>
        /// <param name="logger">Logging service.</param>
        public DataSourceController(IDataSourceService dataSourceService,
                                    ILogger<DataSourceController> logger)
        {
            _dataSourceService = dataSourceService ?? throw new ArgumentNullException(nameof(dataSourceService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost("start")]
        public IActionResult Start()
        {
            var success = _dataSourceService.Start();
            if (!success)
            {
                return Conflict("Problems starting the service!");
            }

            return Ok("Started!");
        }

        [HttpPost("stop")]
        public IActionResult Stop()
        {
            var success = _dataSourceService.Stop();
            if (!success)
            {
                return Conflict("Problems stopping the service!");
            }

            return Ok("Stopped!");
        }

        [HttpGet("configuration")]
        public SettingsDTO GetConfiguration()
        {
            var settings = _dataSourceService.GetConfiguration();

            return settings;
        }

        [HttpPost("configuration")]
        public IActionResult Configure([FromBody] SettingsDTO settings)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            
            var success = _dataSourceService.Configure(settings);
            if (!success)
            {
                return Conflict("Configuration conflict!");
            }

            return Accepted("Configuration accepted!");
        }
    }
}