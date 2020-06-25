using DataSource.Application.Settings;

namespace DataSource.Application.Interfaces
{
    /// <summary>
    /// Define interface for generator of telemetry data.
    /// </summary>
    public interface IDataGeneratorService
    {
        /// <summary>
        /// Sensor device.
        /// </summary>
        ISensor Sensor { get; }

        /// <summary>
        /// Transmitter for posting data to specific API.
        /// </summary>
        ITransmitter Transmitter { get; }

        /// <summary>
        /// Time interval (ms) for data generation .
        /// </summary>
        int GenerationTimeInterval { get; }

        /// <summary>
        /// Start data generation with defaul generation time interval.
        /// </summary>
        void Start();

        /// <summary>
        /// Start data generation with specific generation time interval (ms).
        /// </summary>
        /// <param name="generationTimeInterval">Generation time interval (ms).</param>
        void Start(int generationTimeInterval);

        /// <summary>
        /// Stop data generation.
        /// </summary>
        void Stop();

        /// <summary>
        /// Configure data generator.
        /// </summary>
        /// <param name="settings">Data generator common settings.</param>
        void Configure(GeneratorSettings settings);
    }
}