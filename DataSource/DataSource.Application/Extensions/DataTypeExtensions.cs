using DataSource.Application.Enums;

namespace DataSource.Application.Extensions
{
    /// <summary>
    /// Define extensions for conversion of the stirng to telemetry data type.
    /// </summary>
    public static class DataTypeExtensions
    {
        /// <summary>
        /// Extension to conver string to telemetry data type.
        /// </summary>
        /// <param name="dataTypeString">Data type in string format.</param>
        /// <returns>Data type.</returns>
        public static DataType ToDataType(this string dataTypeString)
        {
            return dataTypeString switch
            {
                "Temperature" => DataType.Temperature,
                "Acoustic" => DataType.Acoustic,
                _ => DataType.Unknown,
            };
        }
    }
}