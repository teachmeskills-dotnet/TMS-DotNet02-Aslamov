using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Sensor.API.Common.Interfaces;
using Sensor.API.DTO;
using Sensor.API.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sensor.API.Services
{
    /// <summary>
    /// Define service to manage application sensors.
    /// </summary>
    public class SensorService : ISensorService
    {
        private readonly SensorContext _sensorContext;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor of sensor service.
        /// </summary>
        /// <param name="sensorContext">Sensor context.</param>
        /// <param name="mapper">Automapper.</param>
        public SensorService(SensorContext sensorContext,
                             IMapper mapper)
        {
            _sensorContext = sensorContext ?? throw new ArgumentNullException(nameof(sensorContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <inheritdoc/>
        public Task<(int id, bool success)> RegisterNewSensorAsync(SensorDTO sensor)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<SensorDTO> GetSensorByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<ICollection<SensorDTO>> GetAllSensorsAsync()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<bool> UpdateSensorAsync(SensorDTO sensor)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<bool> DeleteSensorAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}