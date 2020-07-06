using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sensor.API.Common.Constants;
using Sensor.API.Common.Interfaces;
using Sensor.API.DTO;

namespace Sensor.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class SensorsController : ControllerBase
    {
        private readonly ISensorService _sensorService;
        private readonly ILogger<SensorsController> _logger;

        /// <summary>
        /// Constructor of sensors controller.
        /// </summary>
        /// <param name="sensorService">Service to manage sensors.</param>
        /// <param name="logger">Logging service.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public SensorsController(ISensorService sensorService, ILogger<SensorsController> logger)
        {
            _sensorService = sensorService ?? throw new ArgumentNullException(nameof(sensorService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET: api/sensors
        [Authorize(Roles = "User, Admin")]
        [HttpGet]
        public async Task<ICollection<SensorDTO>> GetSensors()
        {
            var sensors = await _sensorService.GetAllSensorsAsync();
            var count = sensors.Count;

            _logger.LogInformation($"{count} {SensorsConstants.GET_SENSORS}");

            return sensors;
        }

        // GET: api/sensors
        [Authorize(Roles = "User, Admin")]
        [HttpGet("{profileId:guid}")]
        public async Task<ICollection<SensorDTO>> GetSensors([FromRoute] Guid profileId)
        {
            var sensors = await _sensorService.GetAllSensorsByProfileIdAsync(profileId);

            var count = sensors.Count;
            _logger.LogInformation($"{count} {SensorsConstants.GET_PROFILE_SENSORS} {profileId}");

            return sensors;
        }

        // GET: api/sensors/{id}
        [Authorize(Roles = "User, Admin")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSensor([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sensor = await _sensorService.GetSensorByIdAsync(id);
            if (sensor == null)
            {
                _logger.LogWarning($"{id} {SensorsConstants.SENSOR_NOT_FOUND}");
                return NoContent();
            }

            _logger.LogInformation($"{sensor.Id} {SensorsConstants.GET_FOUND_SENSOR}");
            return Ok(sensor);
        }

        // POST: api/sensors
        [Authorize(Roles = "User, Admin")]
        [HttpPost]
        public async Task<IActionResult> RegisterNewSensor([FromBody] SensorDTO sensor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (id, success) = await _sensorService.RegisterNewSensorAsync(sensor);
            if (!success)
            {
                _logger.LogWarning($"{id} {SensorsConstants.ADD_SENSOR_CONFLICT}");
                return Conflict(id);
            }

            sensor.Id = id;

            _logger.LogInformation($"{sensor.Id} {SensorsConstants.ADD_SENSOR_SUCCESS}");
            return CreatedAtAction(nameof(RegisterNewSensor), sensor);
        }

        // PUT: api/sensors/{id}
        [Authorize(Roles = "User, Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSensor([FromBody] SensorDTO sensor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (sensor.Id <=0 )
            {
                return BadRequest(sensor.Id);
            }

            var sensorFound = await _sensorService.GetSensorByIdAsync(sensor.Id);
            if (sensorFound == null)
            {
                _logger.LogWarning($"{sensor.Id} {SensorsConstants.SENSOR_NOT_FOUND}");
                return NotFound(sensor.Id);
            }

            var success = await _sensorService.UpdateSensorAsync(sensor);
            if (!success)
            {
                _logger.LogWarning($"{sensor.Id} {SensorsConstants.UPDATE_SENSOR_CONFLICT}");
                return Conflict();
            }

            _logger.LogInformation($"{sensor.Id} {SensorsConstants.UPDATE_SENSOR_SUCCESS}");
            return Ok(sensor);
        }

        // DELETE: api/sensors/{id}
        [Authorize(Roles = "User, Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSensor([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = await _sensorService.DeleteSensorByIdAsync(id);
            if (!success)
            {
                _logger.LogWarning($"{id} {SensorsConstants.SENSOR_NOT_FOUND}");
                return NotFound(id);
            }

            _logger.LogInformation($"{id} {SensorsConstants.DELETE_SENSOR_SUCCESS}");
            return Ok(id);
        }
    }
}