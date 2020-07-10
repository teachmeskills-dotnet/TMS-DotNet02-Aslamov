using System;
using System.Collections.Generic;
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
    public class RecordsController : ControllerBase
    {
        private readonly IRecordService _recordService;
        private readonly ILogger<RecordsController> _logger;

        /// <summary>
        /// Constructor of records controller.
        /// </summary>
        /// <param name="recordService">Service to manage sensor records.</param>
        /// <param name="logger">Logging service.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public RecordsController(IRecordService recordService, ILogger<RecordsController> logger)
        {
            _recordService = recordService ?? throw new ArgumentNullException(nameof(recordService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET: api/records
        [Authorize(Roles = "User, Admin")]
        [HttpGet]
        public async Task<ICollection<RecordDTO>> GetRecords([FromQuery] int? sensorId)
        {
            var records = await _recordService.GetAllRecordsAsync(sensorId);
            var count = records.Count;

            _logger.LogInformation($"{count} {RecordsConstants.GET_RECORDS}");

            return records;
        }

        // GET: api/records/{id}
        [Authorize(Roles = "User, Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecord([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var record = await _recordService.GetRecordByIdAsync(id);
            if (record == null)
            {
                _logger.LogWarning($"{id} {RecordsConstants.RECORD_NOT_FOUND}");
                return NotFound(id);
            }

            _logger.LogInformation($"{record.Id} {RecordsConstants.GET_FOUND_RECORD}");
            return Ok(record);
        }

        // POST: api/records
        [Authorize(Roles = "User, Admin")]
        [HttpPost]
        public async Task<IActionResult> RegisterNewRecord([FromBody] RecordDTO record)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (id, success) = await _recordService.RegisterNewRecordAsync(record);
            if (!success)
            {
                _logger.LogWarning($"{id} {RecordsConstants.ADD_RECORD_CONFLICT}");
                return Conflict(id);
            }

            record.Id = id;

            _logger.LogInformation($"{record.Id} {RecordsConstants.ADD_RECORD_SUCCESS}");
            return CreatedAtAction(nameof(RegisterNewRecord), record);
        }

        // PUT: api/records/{id}
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecord([FromBody] RecordDTO record)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (record.Id <= 0)
            {
                return BadRequest(record.Id);
            }

            var sensorFound = await _recordService.GetRecordByIdAsync(record.Id);
            if (sensorFound == null)
            {
                _logger.LogWarning($"{record.Id} {RecordsConstants.RECORD_NOT_FOUND}");
                return NotFound(record.Id);
            }

            var success = await _recordService.UpdateRecordAsync(record);
            if (!success)
            {
                _logger.LogWarning($"{record.Id} {RecordsConstants.UPDATE_RECORD_CONFLICT}");
                return Conflict();
            }

            _logger.LogInformation($"{record.Id} {RecordsConstants.UPDATE_RECORD_SUCCESS}");
            return Ok(record);
        }

        // DELETE: api/records/{id}
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecord([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = await _recordService.DeleteRecordByIdAsync(id);
            if (!success)
            {
                _logger.LogWarning($"{id} {RecordsConstants.RECORD_NOT_FOUND}");
                return NotFound(id);
            }

            _logger.LogInformation($"{id} {RecordsConstants.DELETE_RECORD_SUCCESS}");
            return Ok(id);
        }

        // DELETE: api/records
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteRecords()
        {
            await _recordService.DeleteAllRecordsAsync();

            _logger.LogInformation($"{RecordsConstants.DELETE_RECORD_SUCCESS}");
            return Ok();
        }
    }
}