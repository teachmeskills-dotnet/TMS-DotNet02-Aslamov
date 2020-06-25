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
    public class DataGeneratorService : IDataGeneratorService
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
        public ISensor Sensor { get; private set; }

        /// <inheritdoc/>
        public ITransmitter Transmitter { get; private set; }

        /// <summary>
        /// Constructor of the service to generate & send telemetry data.
        /// </summary>
        /// <param name="settings">Generator settings.</param>
        public DataGeneratorService(GeneratorSettings settings) 
        {
            Configure(settings);
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException"></exception>
        public void Configure(GeneratorSettings settings)
        {
            settings = settings ?? throw new ArgumentNullException(nameof(settings));

            GenerationTimeInterval = settings.GenerationTimeIntervalSeconds.ToInteger().ToMilliseconds();

            var dataType = settings.DataType.ToDataType();
            var sernoseSerial = settings.SensorSerial;

            Sensor = new Sensor(sernoseSerial, dataType);

            var hostAddress = settings.HostAddress;
            var authToken = settings.AuthToken;

            Transmitter = new Transmitter(hostAddress, authToken);
        }

        /// <inheritdoc/>
        public void Start()
        {    
            if (_genarationTask == null || _genarationTask.Status == TaskStatus.Canceled)
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
            }
        }

        /// <inheritdoc/>
        public void Start(int generationTimeInterval)
        {
            GenerationTimeInterval = generationTimeInterval;
            Start();
        }

        /// <inheritdoc/>
        public void Stop()
        {
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
            }
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
                    var status = await Transmitter.SendDataRecord(dataRecord);
                }
                await Task.Delay(GenerationTimeInterval);
            }
        }
    }
}