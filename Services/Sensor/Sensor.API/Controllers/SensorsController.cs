using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sensor.API.Common.Constants;
using Sensor.API.Common.Interfaces;
using Sensor.API.DTO;
using Serilog;

namespace Sensor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorsController : ControllerBase
    {
        private readonly ISensorService _sensorService;

        /// <summary>
        /// Constructor of sensor controller.
        /// </summary>
        /// <param name="sensorService">Service to manage sensors.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public SensorsController(ISensorService sensorService) => _sensorService = sensorService ?? throw new ArgumentNullException(nameof(sensorService));

        // GET: api/sensors
        [HttpGet]
        public async Task<ICollection<SensorDTO>> GetSensors()
        {
            var sensors = await _sensorService.GetAllSensorsAsync();
            var count = sensors.ToList().Count;

            Log.Error($"{count} {SensorsConstansts.GET_SENSORS}");

            return sensors;
        }

        // GET: api/sensors/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSensor([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sensor = await _sensorService.GetSensorByIdAsync(id);
            if (sensor == null)
            {
                Log.Warning($"{id} {SensorsConstansts.SENSOR_NOT_FOUND}");
                return NotFound();
            }

            Log.Information($"{sensor.Id} {SensorsConstansts.GET_FOUND_SENSOR}");
            return Ok(sensor);
        }

        // POST: api/sensors
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
                Log.Warning($"{id} {SensorsConstansts.ADD_SENSOR_CONFLICT}");
                return Conflict(id);
            }

            sensor.Id = id;

            Log.Information($"{sensor.Id} {SensorsConstansts.ADD_PROFILE_SUCCESS}");
            return CreatedAtAction(nameof(RegisterNewSensor), sensor);
        }

        // PUT: api/sensors/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSensor([FromBody] SensorDTO sensor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sensorFound = await _sensorService.GetSensorByIdAsync(sensor.Id);
            if (sensorFound == null)
            {
                Log.Warning($"{sensor.Id} {SensorsConstansts.SENSOR_NOT_FOUND}");
                return NotFound();
            }

            var success = await _sensorService.UpdateSensorAsync(sensor);
            if (!success)
            {
                Log.Warning($"{sensor.Id} {SensorsConstansts.UPDATE_SENSOR_CONFLICT}");
                return Conflict();
            }

            Log.Information($"{sensor.Id} {SensorsConstansts.UPDATE_PROFILE_SUCCESS}");
            return Ok(sensor);
        }

        // DELETE: api/sensors/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSensor([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sensorFound = await _sensorService.GetSensorByIdAsync(id);
            if (sensorFound == null)
            {
                Log.Warning($"{id} {SensorsConstansts.SENSOR_NOT_FOUND}");
                return NotFound();
            }

            var success = await _sensorService.DeleteSensorAsync(id);
            if (!success)
            {
                Log.Warning($"{id} {SensorsConstansts.DELETE_SENSOR_CONFLICT}");
                return Conflict();
            }

            Log.Information($"{id} {SensorsConstansts.UPDATE_PROFILE_SUCCESS}");
            return Ok(id);
        }
    }
}