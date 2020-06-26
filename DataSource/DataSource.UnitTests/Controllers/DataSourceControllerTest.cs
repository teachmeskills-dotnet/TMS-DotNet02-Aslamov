using DataSource.API.Controllers;
using DataSource.Application.DTO;
using DataSource.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace DataSource.UnitTests.Controllers
{
    public class DataSourceControllerTest
    {
        [Fact]
        public void Start_WhenServiceNotAccessable_Returns_Conflict()
        {
            // Arrange
            var dataSourceServiceMock = new Mock<IDataSourceService>();
            dataSourceServiceMock.Setup(service => service
                .Start())
                .Returns(false);

            var loggerMock = new Mock<ILogger<DataSourceController>>();

            var controller = new DataSourceController(dataSourceServiceMock.Object, loggerMock.Object);

            // Act
            var result = controller.Start();

            // Assert
            Assert.IsType<ConflictObjectResult>(result);
        }

        [Fact]
        public void Start_WhenStartSuccessfully_Returns_Ok()
        {
            // Arrange
            var dataSourceServiceMock = new Mock<IDataSourceService>();
            dataSourceServiceMock.Setup(service => service
                .Start())
                .Returns(true);

            var loggerMock = new Mock<ILogger<DataSourceController>>();

            var controller = new DataSourceController(dataSourceServiceMock.Object, loggerMock.Object);

            // Act
            var result = controller.Start();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Stop_WhenServiceNotAccessable_Returns_Conflict()
        {
            // Arrange
            var dataSourceServiceMock = new Mock<IDataSourceService>();
            dataSourceServiceMock.Setup(service => service
                .Stop())
                .Returns(false);

            var loggerMock = new Mock<ILogger<DataSourceController>>();

            var controller = new DataSourceController(dataSourceServiceMock.Object, loggerMock.Object);

            // Act
            var result = controller.Stop();

            // Assert
            Assert.IsType<ConflictObjectResult>(result);
        }

        [Fact]
        public void Stop_WhenStopSuccessfully_Returns_Ok()
        {
            // Arrange
            var dataSourceServiceMock = new Mock<IDataSourceService>();
            dataSourceServiceMock.Setup(service => service
                .Stop())
                .Returns(true);

            var loggerMock = new Mock<ILogger<DataSourceController>>();

            var controller = new DataSourceController(dataSourceServiceMock.Object, loggerMock.Object);

            // Act
            var result = controller.Stop();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetConfiguration_WhenConfigurationSearchError_Returns_NotFound()
        {
            // Arrange
            var dataSourceServiceMock = new Mock<IDataSourceService>();
            dataSourceServiceMock.Setup(service => service
                .GetConfiguration())
                .Returns(GetNullSetingsDTO());

            var loggerMock = new Mock<ILogger<DataSourceController>>();

            var controller = new DataSourceController(dataSourceServiceMock.Object, loggerMock.Object);

            // Act
            var result = controller.GetConfiguration();

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetConfiguration_WhenFound_Returns_Configuration()
        {
            // Arrange
            var dataSourceServiceMock = new Mock<IDataSourceService>();
            dataSourceServiceMock.Setup(service => service
                .GetConfiguration())
                .Returns(GetSettingsDTO());

            var loggerMock = new Mock<ILogger<DataSourceController>>();

            var controller = new DataSourceController(dataSourceServiceMock.Object, loggerMock.Object);

            // Act
            var result = controller.GetConfiguration();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<SettingsDTO>(okObjectResult.Value);
        }

        [Fact]
        public void Configure_WithInvalidModel_Returns_BadRequestResult()
        {
            // Arrange
            var dataSourceServiceMock = new Mock<IDataSourceService>();
            var loggerMock = new Mock<ILogger<DataSourceController>>();

            var controller = new DataSourceController(dataSourceServiceMock.Object, loggerMock.Object);
            controller.ModelState.AddModelError("Error", "Model Error");
            var settingsDTO = new SettingsDTO();

            // Act
            var result = controller.Configure(settingsDTO);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Configure_WithValidExistingModel_Returns_ConflictResult()
        {
            // Arrange
            var dataSourceServiceMock = new Mock<IDataSourceService>();
            dataSourceServiceMock.Setup(service => service
                .Configure(It.IsAny<SettingsDTO>()))
                .Returns(false);

            var loggerMock = new Mock<ILogger<DataSourceController>>();

            var controller = new DataSourceController(dataSourceServiceMock.Object, loggerMock.Object);
            var settingsDTO = new SettingsDTO();

            // Act
            var result = controller.Configure(settingsDTO);

            // Assert
            var conflictObjectResult = Assert.IsType<ConflictObjectResult>(result);
            Assert.IsAssignableFrom<string>(conflictObjectResult.Value);
        }

        [Fact]
        public void Configure_WithValidModel_Returns_AcceptedResult()
        {
            // Arrange
            var dataSourceServiceMock = new Mock<IDataSourceService>();
            dataSourceServiceMock.Setup(service => service
                .Configure(It.IsAny<SettingsDTO>()))
                .Returns(true);

            var loggerMock = new Mock<ILogger<DataSourceController>>();

            var controller = new DataSourceController(dataSourceServiceMock.Object, loggerMock.Object);
            var settingsDTO = new SettingsDTO();

            // Act
            var result = controller.Configure(settingsDTO);

            // Assert
            Assert.IsType<AcceptedResult>(result);
        }

        // Generate settings DTO
        private SettingsDTO GetSettingsDTO()
        {
            return new SettingsDTO
            {
                DataType = "Temperature",
                GenerationTimeIntervalSeconds = "5",
                SensorSerial = "123456789",
            };
        }

        // Generate null settings.
        private SettingsDTO GetNullSetingsDTO()
        {
            return null;
        }
    }
}