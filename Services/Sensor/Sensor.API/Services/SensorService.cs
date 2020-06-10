using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sensor.API.Common.Constants;
using Sensor.API.Common.Interfaces;
using Sensor.API.DTO;
using Sensor.API.Models;
using Serilog;
using System;
using System.Collections.Generic;
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

        /// <summary>
        /// Constructor of sensor service.
        /// </summary>
        /// <param name="sensorContext">Sensor context.</param>
        /// <param name="mapper">Automapper.</param>
        public SensorService(ISensorContext sensorContext,
                             IMapper mapper)
        {
            _sensorContext = sensorContext ?? throw new ArgumentNullException(nameof(sensorContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <inheritdoc/>
        public async Task<(int id, bool success)> RegisterNewSensorAsync(SensorDTO sensorDTO)
        {
            var sensor = _mapper.Map<SensorDTO, SensorDevice>(sensorDTO);
            var sensorFound = await _sensorContext.Sensors.FirstOrDefaultAsync(s => s.Serial == sensorDTO.Serial);

            if (sensorFound == null)
            {
                Log.Error(SensorsConstansts.SENSOR_ALREADY_EXIST);
                return (0, false);
            }

            await _sensorContext.Sensors.AddAsync(sensor);
            await _sensorContext.SaveChangesAsync(new CancellationToken());

            var id = sensor.Id;

            return (id, true);
        }

        /// <inheritdoc/>
        public async Task<SensorDTO> GetSensorByIdAsync(int id)
        {
            var sensor = await _sensorContext.Sensors.FirstOrDefaultAsync(s => s.Id == id);
            var sensorDTO = _mapper.Map<SensorDevice, SensorDTO>(sensor);

            return sensorDTO;
        }

        /// <inheritdoc/>
        public async Task<ICollection<SensorDTO>> GetAllSensorsAsync()
        {
            var sensors = await _sensorContext.Sensors.ToListAsync();
            var collectionOfSensorDTO = _mapper.Map<ICollection<SensorDevice>, ICollection<SensorDTO>>(sensors);

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
            if (sensor.Type.Type != sensorDTO.Type)
            {
                var newType = await _sensorContext.Types.FirstOrDefaultAsync(t => t.Type == sensorDTO.Type);
                if (newType == null)
                {
                    Log.Error(SensorsConstansts.UNKNOWN_SENSOR_TYPE);
                    return false;
                }
            }

            _sensorContext.Update(sensor);
            await _sensorContext.SaveChangesAsync(new CancellationToken());

            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteSensorAsync(int id)
        {
            var sensorFound = await _sensorContext.Sensors.FirstOrDefaultAsync(s => s.Id == id);
            if (sensorFound == null)
            {
                Log.Error(SensorsConstansts.SENSOR_NOT_FOUND);
                return false;
            }

            _sensorContext.Remove(sensorFound);
            await _sensorContext.SaveChangesAsync(new CancellationToken());

            return true;
        }
    }
}