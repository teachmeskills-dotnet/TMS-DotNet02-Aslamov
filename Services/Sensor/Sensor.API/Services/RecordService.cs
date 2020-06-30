using AutoMapper;
using EventBus.Contracts.Commands;
using EventBus.Contracts.Common;
using EventBus.Contracts.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sensor.API.Common.Constants;
using Sensor.API.Common.Interfaces;
using Sensor.API.DTO;
using Sensor.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sensor.API.Services
{
    /// <summary>
    /// Define service to manage sensor records.
    /// </summary>
    public class RecordService : IRecordService
    {
        private readonly ISensorContext _sensorContext;
        private readonly IMapper _mapper;
        private readonly ICommandProducer<IProcessData, IRecordDTO> _processDataCommandProducer;
        private readonly ILogger<RecordService> _logger;

        /// <summary>
        /// Constructor of record service.
        /// </summary>
        /// <param name="sensorContext">Sensor context.</param>
        /// <param name="mapper">Automapper.</param>
        public RecordService(ISensorContext sensorContext,
                             IMapper mapper,
                             ICommandProducer<IProcessData, IRecordDTO> processDataCommandProducer,
                             ILogger<RecordService> logger)
        {
            _sensorContext = sensorContext ?? throw new ArgumentNullException(nameof(sensorContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _processDataCommandProducer = processDataCommandProducer ?? throw new ArgumentNullException(nameof(processDataCommandProducer));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public async Task<(int id, bool success)> RegisterNewRecordAsync(RecordDTO recordDTO)
        {
            var recordFound = await _sensorContext.Records.FirstOrDefaultAsync(r => r.Date == recordDTO.Date &&
                                                                                    r.SensorDevice.Serial == recordDTO.SensorDeviceSerial);
            if (recordFound != null)
            {
                _logger.LogError(RecordsConstants.RECORD_ALREADY_EXIST);
                return (0, false);
            }

            var sensorFound = await _sensorContext.Sensors.FirstOrDefaultAsync(s => s.Serial == recordDTO.SensorDeviceSerial);
            if (sensorFound == null)
            {
                _logger.LogError($"{recordDTO.SensorDeviceSerial} {SensorsConstants.UNKNOWN_SENSOR_SERIAL}");
                return (0, false);
            }

            recordDTO.SensorDeviceId = sensorFound.Id;

            var record = _mapper.Map<RecordDTO, SensorRecord>(recordDTO);
            record.SensorDevice = sensorFound;

            await _sensorContext.Records.AddAsync(record);
            await _sensorContext.SaveChangesAsync(new CancellationToken());
            
            var id = record.Id;
            recordDTO.Id = id;

            var sensorTypeFound = await _sensorContext.Types.FirstOrDefaultAsync(t => t.Id == sensorFound.SensorTypeId);
            if (sensorTypeFound == null)
            {
                _logger.LogError($"{sensorFound.Id} {SensorsConstants.UNKNOWN_SENSOR_TYPE}");
                recordDTO.SensorDeviceType = null;
            }
            else
            {
                recordDTO.SensorDeviceType = sensorTypeFound.Type;
            }

            await _processDataCommandProducer.Send(recordDTO);

            return (id, true);
        }

        /// <inheritdoc/>
        public async Task<RecordDTO> GetRecordByIdAsync(int id)
        {
            var record = await _sensorContext.Records.FirstOrDefaultAsync(r => r.Id == id);
            if (record == null)
            {
                return null;
            }

            var recordDTO = _mapper.Map<SensorRecord, RecordDTO>(record);

            var sensorDevice = await _sensorContext.Sensors.FirstOrDefaultAsync(s => s.Id == recordDTO.SensorDeviceId);
            if (sensorDevice != null)
            {
                recordDTO.SensorDeviceId = sensorDevice.Id;
                recordDTO.SensorDeviceSerial = sensorDevice.Serial;
            }

            return recordDTO;
        }

        /// <inheritdoc/>
        public async Task<ICollection<RecordDTO>> GetAllRecordsAsync(int? sensorId)
        {
            var queriableCollectionOfRecords = _sensorContext.Records.Select(r => r);

            if (sensorId !=null && sensorId >=0)
            {
                queriableCollectionOfRecords = queriableCollectionOfRecords.Where(r => r.SensorDeviceId == (int)sensorId);
            }

            var records = await queriableCollectionOfRecords.ToListAsync();

            var collectionOfRecordDTO = _mapper.Map<ICollection<SensorRecord>, ICollection<RecordDTO>> (records);

            foreach (var recordDTO in collectionOfRecordDTO)
            {
                var sensorDevice = await _sensorContext.Sensors.FirstOrDefaultAsync(s => s.Id == recordDTO.SensorDeviceId);
                if (sensorDevice != null)
                {
                    recordDTO.SensorDeviceId = sensorDevice.Id;
                    recordDTO.SensorDeviceSerial = sensorDevice.Serial;
                }
            }

            return collectionOfRecordDTO;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateRecordAsync(RecordDTO recordDTO)
        {
            var record = await _sensorContext.Records.FirstOrDefaultAsync(r => r.Id == recordDTO.Id);

            if (record == null)
            {
                return false;
            }

            if (recordDTO.Value == null)
            {
                _logger.LogError(RecordsConstants.EMPTY_RECORD_VALUE);
                return false;
            }
            record.Value = recordDTO.Value;
            record.Date = recordDTO.Date;
            record.IsDeleted = recordDTO.IsDeleted;

            if (record.SensorDevice.Serial != recordDTO.SensorDeviceSerial)
            {
                var newSensorDevice = await _sensorContext.Sensors.FirstOrDefaultAsync(s => s.Serial== recordDTO.SensorDeviceSerial);
                if (newSensorDevice == null)
                {
                    _logger.LogError(SensorsConstants.UNKNOWN_SENSOR_SERIAL);
                    return false;
                }
                else
                {
                    record.SensorDeviceId = newSensorDevice.Id;
                    record.SensorDevice = newSensorDevice;
                }
            }

            _sensorContext.Update(record);
            await _sensorContext.SaveChangesAsync(new CancellationToken());

            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteRecordByIdAsync(int id)
        {
            var recordFound = await _sensorContext.Records.FirstOrDefaultAsync(r => r.Id == id);
            if (recordFound == null)
            {
                _logger.LogError(RecordsConstants.RECORD_NOT_FOUND);
                return false;
            }

            _sensorContext.Remove(recordFound);
            await _sensorContext.SaveChangesAsync(new CancellationToken());

            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteAllRecordsAsync()
        {
            var recordsFound = await _sensorContext.Records.ToListAsync();

            foreach(var record in recordsFound)
            {
                _sensorContext.Remove(record);
            }
            
            await _sensorContext.SaveChangesAsync(new CancellationToken());
            return true;
        }
    }
}