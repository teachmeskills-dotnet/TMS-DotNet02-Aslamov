using DataSource.Application.Enums;
using DataSource.Application.Interfaces;
using DataSource.Application.Settings;
using DataSource.Infrastructure.Services;
using Shouldly;
using System;
using System.Threading;
using Xunit;

namespace DataSource.UnitTests.Services
{
    public class DataGeneratorServiceTest
    {
        /// <summary>
        /// Test data generator.
        /// </summary>
        IDataGeneratorService TestGenerator { get; set; }

        public DataGeneratorServiceTest()
        {
            var settings = new GeneratorSettings
            {
                DataType = DataType.Temperature.ToString(),
                Serial = "1234567890",
                GenerationTimeInterval = "5",
            };

            TestGenerator = new DataGeneratorService(settings);
        }

        [Fact]
        public void Constructor_WithEmptySettins_Returns_ArgumentNullException()
        {
            // Arrange
            GeneratorSettings settings = null;

            // Act
            var constructGenerator = new Action(() => new DataGeneratorService(settings));

            // Assert
            Assert.Throws<ArgumentNullException>(constructGenerator);
        }

        [Fact]
        public void Configure_WithEmptySettins_Returns_ArgumentNullException()
        {
            // Arrange
            GeneratorSettings settings = null;

            // Act
            var configureGenerator = new Action(() => TestGenerator.Configure(settings));

            // Assert
            Assert.Throws<ArgumentNullException>(configureGenerator);
        }

        [Fact]
        public void Configure_WithValidSettins_Updates_Generator()
        {
            // Arrange
            var oldSensorType = TestGenerator.Sensor.DataType;
            var oldSensorSerial = TestGenerator.Sensor.Serial;
            var oldGenerationTimeInterval = TestGenerator.GenerationTimeInterval;

            var settings = new GeneratorSettings
            {
                DataType = DataType.Acoustic.ToString(),
                Serial = "0987654321",
                GenerationTimeInterval = "10",
            };

            // Act
            TestGenerator.Configure(settings);

            // Assert
            (TestGenerator.Sensor.DataType).ShouldNotBeNull();
            (TestGenerator.Sensor.DataType).ShouldNotBe(oldSensorType);
            (TestGenerator.Sensor.Serial).ShouldNotBeNull();
            (TestGenerator.Sensor.Serial).ShouldNotBe(oldSensorSerial);
            (TestGenerator.GenerationTimeInterval).ShouldNotBeNull();
            (TestGenerator.GenerationTimeInterval).ShouldNotBe(oldGenerationTimeInterval);
        }

        [Fact]
        // Hand test.
        public void Start_Generates_Data()
        {
            // Arrange

            // Act
            TestGenerator.Start();

            Thread.Sleep(20000);
            TestGenerator.Stop();

            Thread.Sleep(20000);
            TestGenerator.Start(1000);

            Thread.Sleep(20000);
            TestGenerator.Stop();

            // Assert
        }

    }
}