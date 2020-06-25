using DataSource.Application.Enums;
using DataSource.Infrastructure.Services;
using Shouldly;
using System;
using Xunit;

namespace DataSource.UnitTests.Services
{
    public class SensorTest
    {
        /// <summary>
        /// Sensor object for testing.
        /// </summary>
        public Sensor TestSensor { get; set; } 

        public SensorTest()
        {
            var serial = "1234567890";
            var dataType = DataType.Temperature;

            TestSensor = new Sensor(serial, dataType);
        }

        [Fact]
        public void Constructor_WithNullSerial_Returns_ArgumentNullException()
        {
            // Arrange
            string serial = null;
            DataType dataType = DataType.Temperature;

            // Act
            var createSensor = new Action(() => new Sensor(serial, dataType));

            // Assert
            Assert.Throws<ArgumentNullException>(createSensor);
        }

        [Fact]
        public void Constructor_WithUnknownSensorType_Returns_ArgumentException()
        {
            // Arrange
            string serial = "1234567890";
            DataType sensorType = DataType.Unknown;

            // Act
            var createSensor = new Action(() => new Sensor(serial, sensorType));

            // Assert
            Assert.Throws<ArgumentException>(createSensor);
        }

        [Fact]
        public void GetRecordData_WithUnknownSensorType_Returns_Null()
        {
            // Arrange
            TestSensor.DataType = DataType.Unknown;

            // Act
            var record = TestSensor.GetDataRecord();

            // Assert
            Assert.Null(record);
        }

        [Fact]
        public void GetRecordData_WithTemperatureSensor_Returns_TemperatureData()
        {
            // Arrange
            TestSensor.DataType = DataType.Temperature;

            // Act
            var record = TestSensor.GetDataRecord();

            // Assert
            record.ShouldNotBeNull();
            (record.Value).ShouldNotBeNull();
            (record.Value).ShouldBeOfType<byte[]>();
            (record.Date).ShouldNotBeNull();
            (record.SensorDeviceType).ShouldNotBeNull();
            (record.SensorDeviceType).ShouldBeOfType<string>();
            (record.SensorDeviceType).ShouldBe("Temperature");
            (record.SensorDeviceSerial).ShouldNotBeNull();
            (record.SensorDeviceSerial).ShouldBeOfType<string>();
            (record.SensorDeviceSerial).ShouldBe("1234567890");
        }

        [Fact]
        public void GetRecordData_WithAcousticSensor_Returns_AcousticData()
        {
            // Arrange
            TestSensor.DataType = DataType.Acoustic;

            // Act
            var record = TestSensor.GetDataRecord();

            // Assert
            record.ShouldNotBeNull();
            (record.Value).ShouldNotBeNull();
            (record.Value).ShouldBeOfType<byte[]>();
            (record.Date).ShouldNotBeNull();
            (record.SensorDeviceType).ShouldNotBeNull();
            (record.SensorDeviceType).ShouldBeOfType<string>();
            (record.SensorDeviceType).ShouldBe("Acoustic");
            (record.SensorDeviceSerial).ShouldNotBeNull();
            (record.SensorDeviceSerial).ShouldBeOfType<string>();
            (record.SensorDeviceSerial).ShouldBe("1234567890");
        }
    }
}