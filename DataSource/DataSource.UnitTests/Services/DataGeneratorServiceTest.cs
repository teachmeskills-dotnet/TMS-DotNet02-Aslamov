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
                SensorSerial = "123456789",
                GenerationTimeIntervalSeconds = "5",
                AuthToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImE3NWI2OWI4LTk2OWYtNDY5ZS1iMjZkLTA4ZDgxMzg5NGQyNyIsInJvbGUiOiJBZG1pbiIsIm5iZiI6MTU5MjU3MDMwMCwiZXhwIjoxNTkzMTc1MTAwLCJpYXQiOjE1OTI1NzAzMDB9.OBHTe43zxDN5o7pnwNEauyZ73m_juw7z46XW8C8nNvU",
                HostAddress = "http://localhost:3000/records",
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
                SensorSerial = "0987654321",
                GenerationTimeIntervalSeconds = "10",
                AuthToken = "11111",
                HostAddress = "http://localhost:4000/records",
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

        //[Fact]
        //// Hand test.
        //public void Start_Generates_Data()
        //{
        //    TestGenerator.Start();

        //    Thread.Sleep(50000);
        //    TestGenerator.Stop();

        //    //Thread.Sleep(20000);
        //    //TestGenerator.Start(1000);

        //    //Thread.Sleep(20000);
        //    //TestGenerator.Stop();
        //}
    }
}