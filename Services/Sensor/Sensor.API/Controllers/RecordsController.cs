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
    public class RecordsController : ControllerBase
    {
        private readonly IRecordService _recordService;

        /// <summary>
        /// Constructor of records controller.
        /// </summary>
        /// <param name="recordService">Service to manage sensor records.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public RecordsController(IRecordService recordService) => _recordService = recordService ?? throw new ArgumentNullException(nameof(recordService));

        // GET: api/records
        [HttpGet]
        public async Task<ICollection<RecordDTO>> GetRecords(int? sensorId)
        {
            var records = await _recordService.GetAllRecordsAsync(sensorId);
            var count = records.Count;

            Log.Information($"{count} {RecordsConstants.GET_RECORDS}");

            return records;
        }

        // GET: api/records/{id}
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
                Log.Warning($"{id} {RecordsConstants.RECORD_NOT_FOUND}");
                return NotFound();
            }

            Log.Information($"{record.Id} {RecordsConstants.GET_FOUND_RECORD}");
            return Ok(record);
        }

        // POST: api/records
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
                Log.Warning($"{id} {RecordsConstants.ADD_RECORD_CONFLICT}");
                return Conflict(id);
            }

            record.Id = id;

            Log.Information($"{record.Id} {RecordsConstants.ADD_RECORD_SUCCESS}");
            return CreatedAtAction(nameof(RegisterNewRecord), record);
        }

        // PUT: api/records/{id}
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
                Log.Warning($"{record.Id} {RecordsConstants.RECORD_NOT_FOUND}");
                return NotFound();
            }

            var success = await _recordService.UpdateRecordAsync(record);
            if (!success)
            {
                Log.Warning($"{record.Id} {RecordsConstants.UPDATE_RECORD_CONFLICT}");
                return Conflict();
            }

            Log.Information($"{record.Id} {RecordsConstants.UPDATE_RECORD_SUCCESS}");
            return Ok(record);
        }

        // DELETE: api/records/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecord([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var recordFound = await _recordService.GetRecordByIdAsync(id);
            if (recordFound == null)
            {
                Log.Warning($"{id} {RecordsConstants.RECORD_NOT_FOUND}");
                return NotFound();
            }

            var success = await _recordService.DeleteRecordByIdAsync(id);
            if (!success)
            {
                Log.Warning($"{id} {RecordsConstants.DELETE_RECORD_CONFLICT}");
                return Conflict();
            }

            Log.Information($"{id} {RecordsConstants.DELETE_RECORD_SUCCESS}");
            return Ok(id);
        }

        // DELETE: api/records
        [HttpDelete]
        public async Task<IActionResult> DeleteRecords()
        {
            var success = await _recordService.DeleteAllRecordsAsync();
            if (!success)
            {
                Log.Warning($"{RecordsConstants.DELETE_RECORD_CONFLICT}");
                return Conflict();
            }

            Log.Information($"{RecordsConstants.DELETE_RECORD_SUCCESS}");
            return Ok();
        }
    }
}