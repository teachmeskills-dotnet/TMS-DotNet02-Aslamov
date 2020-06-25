using DataSource.Application.DTO;
using DataSource.Application.Enums;
using DataSource.Application.Extensions;
using DataSource.Application.Interfaces;
using DataSource.Application.Settings;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DataSource.Infrastructure.Services
{
    /// <summary>
    /// Define service to generate data records of a specific type.
    /// </summary>
    public class DataSourceService : IDataSourceService
    {
        // Source of cancellation tokens.
        private CancellationTokenSource _cancellationTokenSource;

        // Task for data generation.
        private Task _genarationTask;

        // Time interval (ms) for data generation.
        private int _generationTimeInterval = 1000;

        /// <inheritdoc/>
        public int GenerationTimeInterval
        {
            get => _generationTimeInterval;
            private set => (_generationTimeInterval) = (value > 0 || value == Timeout.Infinite) ? value : throw new ArgumentOutOfRangeException();
        }

        /// <inheritdoc/>
        public ISensor Sensor { get; protected set; }

        /// <inheritdoc/>
        public ITransmitter Transmitter { get; protected set; }

        /// <inheritdoc/>
        public ServiceState ServiceState { get; protected set; }

        /// <summary>
        /// Constructor of the service to generate & send telemetry data.
        /// </summary>
        /// <param name="settings">Generator settings.</param>
        public DataSourceService(DataSourceSettings settings) 
        {
            Configure(settings);
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException"></exception>
        public bool Configure(DataSourceSettings settings)
        {
            settings = settings ?? throw new ArgumentNullException(nameof(settings));

            GenerationTimeInterval = settings.GenerationTimeIntervalSeconds.ToInteger().ToMilliseconds();

            try
            {
                var dataType = settings.DataType.ToDataType();
                var sernoseSerial = settings.SensorSerial;

                Sensor = new Sensor(sernoseSerial, dataType);
            }
            catch (Exception)
            {
                return false;
            }

            try
            {
                var hostAddress = settings.HostAddress;
                var authToken = settings.AuthToken;

                Transmitter = new Transmitter(hostAddress, authToken);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <inheritdoc/>
        public bool Configure(SettingsDTO settings)
        {
            settings = settings ?? throw new ArgumentNullException(nameof(settings));

            var dataType = settings.DataType.ToDataType();
            var sernoseSerial = settings.SensorSerial;

            GenerationTimeInterval = settings.GenerationTimeIntervalSeconds.ToInteger().ToMilliseconds();

            Sensor = new Sensor(sernoseSerial, dataType);

            return true;
        }

        /// <inheritdoc/>
        public SettingsDTO GetConfiguration()
        {
            var settings = new SettingsDTO
            {
                SensorSerial = Sensor.Serial,
                DataType = Sensor.DataType.ToString(),
                GenerationTimeIntervalSeconds = GenerationTimeInterval.ToSeconds().ToString(),
            };

            return settings;
        }

        /// <inheritdoc/>
        public bool Start()
        {    
            if (_genarationTask == null || _genarationTask.Status == TaskStatus.Canceled || _genarationTask.Status == TaskStatus.Faulted)
            {
                _cancellationTokenSource = new CancellationTokenSource();
                var cancellationToken = _cancellationTokenSource.Token;

                try
                {
                    _genarationTask = Task.Run(() => GenerateAndSend(cancellationToken));
                }
                catch (OperationCanceledException)
                {
                    _cancellationTokenSource.Dispose();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR! {ex.Message}");
                }

                ServiceState = ServiceState.Working;
                return true;
            }
            return false;
        }

        /// <inheritdoc/>
        public bool Start(int generationTimeInterval)
        {
            GenerationTimeInterval = generationTimeInterval;
            var success = Start();

            return success;
        }

        /// <inheritdoc/>
        public bool Stop()
        {
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
                ServiceState = ServiceState.Stopped;
                return true;
            }
            return false;
        }

        // Generate data and send it to specific API.
        private async Task GenerateAndSend(CancellationToken token)
        {
            while (true)
            {
                if (token.IsCancellationRequested)
                {
                    token.ThrowIfCancellationRequested();
                }

                var dataRecord = Sensor.GetDataRecord();
                if (dataRecord != null)
                {
                    await Transmitter.SendDataRecord(dataRecord);
                }
                await Task.Delay(GenerationTimeInterval);
            }
        }
    }
}