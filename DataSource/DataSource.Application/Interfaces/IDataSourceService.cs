using DataSource.Application.DTO;
using DataSource.Application.Enums;
using DataSource.Application.Settings;

namespace DataSource.Application.Interfaces
{
    /// <summary>
    /// Define interface for generator of telemetry data.
    /// </summary>
    public interface IDataSourceService
    {
        /// <summary>
        /// Sensor device for data collection.
        /// </summary>
        ISensor Sensor { get; }

        /// <summary>
        /// Transmitter for posting data to specific API.
        /// </summary>
        ITransmitter Transmitter { get; }

        /// <summary>
        /// Service state (stopped / working).
        /// </summary>
        ServiceState ServiceState { get; }

        /// <summary>
        /// Time interval (ms) for data generation .
        /// </summary>
        int GenerationTimeInterval { get; }

        /// <summary>
        /// Start data generation with defaul generation time interval.
        /// </summary>
        /// <returns>Operation result.</returns>
        bool Start();

        /// <summary>
        /// Start data generation with specific generation time interval (ms).
        /// </summary>
        /// <param name="generationTimeInterval">Generation time interval (ms).</param>
        /// <returns>Operation result.</returns>
        bool Start(int generationTimeInterval);

        /// <summary>
        /// Stop data generation.
        /// </summary>
        /// <returns>Operation result.</returns>
        bool Stop();

        /// <summary>
        /// Configure the whole data source service.
        /// </summary>
        /// <param name="settings">Service settings.</param>
        /// <returns>Operation result.</returns>
        bool Configure(DataSourceSettings settings);

        /// <summary>
        /// Configure data generator.
        /// </summary>
        /// <param name="settings">Generator settings.</param>
        /// <returns>Operation result.</returns>
        bool Configure(SettingsDTO settings);

        /// <summary>
        /// Get data source configuration.
        /// </summary>
        /// <returns>Data source settings.</returns>
        SettingsDTO GetConfiguration();
    }
}