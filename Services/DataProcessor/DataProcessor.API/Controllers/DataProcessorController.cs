using System;
using DataProcessor.API.Common.Interfaces;
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
    }
}