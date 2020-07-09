using AutoMapper;
using EventBus.Contracts.Common;
using EventBus.Contracts.DTO;
using EventBus.Contracts.Events;
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
    /// Define service to manage application sensors.
    /// </summary>
    public class SensorService : ISensorService
    {
        private readonly ISensorContext _sensorContext;
        private readonly IMapper _mapper;
        private readonly IEventProducer<ISensorDeleted, int> _sensorDeletedEventProducer;
        private readonly IRecordService _recordService;
        private readonly ILogger<SensorService> _logger;

        /// <summary>
        /// Constructor of sensor service.
        /// </summary>
        /// <param name="sensorContext">Sensor context.</param>
        /// <param name="mapper">Automapper.</param>
        public SensorService(ISensorContext sensorContext,
                             IMapper mapper,
                             IEventProducer<ISensorDeleted, int> sensorDeletedEventProducer,
                             IRecordService recordService,
                             ILogger<SensorService> logger)
        {
            _sensorContext = sensorContext ?? throw new ArgumentNullException(nameof(sensorContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _sensorDeletedEventProducer = sensorDeletedEventProducer ?? throw new ArgumentNullException(nameof(sensorDeletedEventProducer));
            _recordService = recordService ?? throw new ArgumentNullException(nameof(recordService));
        }

        /// <inheritdoc/>
        public async Task<(int id, bool success)> RegisterNewSensorAsync(SensorDTO sensorDTO)
        {
            var sensor = _mapper.Map<SensorDTO, SensorDevice>(sensorDTO);
            var sensorFound = await _sensorContext.Sensors.FirstOrDefaultAsync(s => s.Serial == sensorDTO.Serial);

            if (sensorFound != null)
            {
                _logger.LogError(SensorsConstants.SENSOR_ALREADY_EXIST);
                return (0, false);
            }

            var typeFound = await _sensorContext.Types.FirstOrDefaultAsync(t => t.Type == sensorDTO.SensorType);
            if (typeFound == null)
            {
                _logger.LogError($"{sensorDTO.SensorType} {SensorsConstants.UNKNOWN_SENSOR_TYPE}");
                return (0, false);
            }

            sensor.SensorTypeId = typeFound.Id;
            sensor.SensorType = typeFound;

            await _sensorContext.Sensors.AddAsync(sensor);
            await _sensorContext.SaveChangesAsync(new CancellationToken());

            var id = sensor.Id;

            return (id, true);
        }

        /// <inheritdoc/>
        public async Task<SensorDTO> GetSensorByIdAsync(int id)
        {
            var sensor = await _sensorContext.Sensors.FirstOrDefaultAsync(s => s.Id == id);
            if(sensor == null)
            {
                return null;
            }

            var sensorDTO = _mapper.Map<SensorDevice, SensorDTO>(sensor);

            var type = await _sensorContext.Types.FirstOrDefaultAsync(t => t.Id == sensor.SensorTypeId);
            if (type != null)
            {
                sensorDTO.SensorType = type.Type;
            }

            return sensorDTO;
        }

        /// <inheritdoc/>
        public async Task<ICollection<SensorDTO>> GetAllSensorsAsync()
        {
            var sensors = await _sensorContext.Sensors.ToListAsync();
            var collectionOfSensorDTO = _mapper.Map<ICollection<SensorDevice>, ICollection<SensorDTO>>(sensors);

            foreach(var sensor in collectionOfSensorDTO)
            {
                var type = await _sensorContext.Types.FirstOrDefaultAsync(t => t.Id == sensor.SensorTypeId);
                sensor.SensorType = type.Type;
            }

            return collectionOfSensorDTO;
        }

        /// <inheritdoc/>
        public async Task<ICollection<SensorDTO>> GetAllSensorsByProfileIdAsync(Guid profileId)
        {
            var sensors = await _sensorContext.Sensors.Where(s => s.ProfileId == profileId).ToListAsync();
            var collectionOfSensorDTO = _mapper.Map<ICollection<SensorDevice>, ICollection<SensorDTO>>(sensors);

            foreach (var sensor in collectionOfSensorDTO)
            {
                var type = await _sensorContext.Types.FirstOrDefaultAsync(t => t.Id == sensor.SensorTypeId);
                sensor.SensorType = type.Type;
            }

            return collectionOfSensorDTO;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateSensorAsync(SensorDTO sensorDTO)
        {
            var sensor = await _sensorContext.Sensors.FirstOrDefaultAsync(s => s.Id == sensorDTO.Id);

            if (sensor == null)
            {
                return false;
            }

            sensor.Serial = sensorDTO.Serial;
            if (sensor.SensorType.Type != sensorDTO.SensorType)
            {
                var newType = await _sensorContext.Types.FirstOrDefaultAsync(t => t.Type == sensorDTO.SensorType);
                if (newType == null)
                {
                    _logger.LogError(SensorsConstants.UNKNOWN_SENSOR_TYPE);
                    return false;
                }
                else
                {
                    sensor.SensorTypeId = newType.Id;
                    sensor.SensorType = newType;
                }
            }

            _sensorContext.Update(sensor);
            await _sensorContext.SaveChangesAsync(new CancellationToken());

            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteSensorByIdAsync(int id)
        {
            var sensorFound = await _sensorContext.Sensors.FirstOrDefaultAsync(s => s.Id == id);
            if (sensorFound == null)
            {
                _logger.LogError(SensorsConstants.SENSOR_NOT_FOUND);
                return false;
            }

            _sensorContext.Remove(sensorFound);
            await _recordService.DeleteAllRecordsBySensorIdAsync(sensorFound.Id);
            await _sensorContext.SaveChangesAsync(new CancellationToken());

            await _sensorDeletedEventProducer.Publish(sensorFound.Id);

            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteAllSensorsByProfileIdAsync(Guid profileId)
        {
            var sensorListFound = await _sensorContext.Sensors.Where(s => s.ProfileId == profileId).ToListAsync();
            if (sensorListFound == null)
            {
                _logger.LogError(SensorsConstants.SENSOR_NOT_FOUND);
                return false;
            }

            // Remove sensors from local DB.
            foreach(var sensor in sensorListFound)
            {
                _sensorContext.Remove(sensor);
                await _recordService.DeleteAllRecordsBySensorIdAsync(sensor.Id);
            }
            await _sensorContext.SaveChangesAsync(new CancellationToken());

            // Publish event on sensors deleting.
            foreach(var sensor in sensorListFound)
            {
                await _sensorDeletedEventProducer.Publish(sensor.Id);
            }
            return true;
        }
    }
}