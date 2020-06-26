using DataSource.Application.Interfaces;
using DataSource.Application.Enums;
using System;
using DataSource.Application.DTO;
using System.Collections.Generic;

namespace DataSource.Infrastructure.Services
{
    /// <summary>
    /// Define class to simulate sensor.
    /// </summary>
    public class Sensor : ISensor
    {
        /// <inheritdoc/>
        public DataType DataType { get; set; }

        /// <inheritdoc/>
        public string Serial { get; set; }

        /// <summary>
        /// Constructor of sersor object.
        /// </summary>
        /// <param name="serial">Sensor serial number.</param>
        /// <param name="dataType">Sensor data type.</param>
        public Sensor(string serial, DataType dataType)
        {
            Serial = serial ?? throw new ArgumentNullException(nameof(serial));
            DataType = dataType != DataType.Unknown ? dataType : throw new ArgumentException(nameof(dataType));
        }

        /// <inheritdoc/>
        public RecordDTO GetDataRecord()
        {
            if (DataType == DataType.Unknown)
                return null;

            var value = (DataType == DataType.Acoustic) ? GetAcousticRecord() : GetTemperatureValue();

            var record = new RecordDTO
            {
                Value = value,
                Date = DateTime.Now,
                SensorDeviceType = DataType.ToString(),
                SensorDeviceSerial = Serial,
            };

            return record;
        }

        // Get random temperature value (exponential distribution).
        private byte[] GetTemperatureValue()
        {
            const double TEMP_MIN = 36.6;
            const double TEMP_MAX = 38.4;
            double scale = 5;

            var probability = new Random().NextDouble();
            var temperature = Math.Exp(probability*scale)/ Math.Exp(scale)*(TEMP_MAX - TEMP_MIN) + TEMP_MIN;

            return BitConverter.GetBytes(temperature);
        }

        // Get random short acoustic record.
        private byte[] GetAcousticRecord()
        {
            var LENGTH_MAX = 10;
            var rand = new Random();

            // Generate random acoustic double record.
            var recordList = new List<double>();
            for (int i=0; i< LENGTH_MAX; i++)
            {
                var value = 2 * rand.NextDouble() - 1;
                recordList.Add(value);
            }
            var record = recordList.ToArray();

            // Conver double acoustic record to byte array.
            var byteList = new List<byte>();
            foreach(var value in record)
            {
                byteList.AddRange(BitConverter.GetBytes(value));
            }
            var byteArray = byteList.ToArray();

            return byteArray;
        }
    }
}