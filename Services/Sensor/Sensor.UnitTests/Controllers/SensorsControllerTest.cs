using Microsoft.AspNetCore.Mvc;
using Moq;
using Sensor.API.Common.Interfaces;
using Sensor.API.Controllers;
using Sensor.API.DTO;
using Serilog;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Sensor.UnitTests.Controllers
{
    public class SensorsControllerTest : ConstrollerTestFixture
    {
        [Fact]
        public void GetSensors_Returns_CollectionOfSensorDTO()
        {
            // Arrange
            var sensorServiceMock = new Mock<ISensorService>();
            sensorServiceMock.Setup(service => service
                .GetAllSensorsAsync())
                .Returns(Task.FromResult(GetAllSensors()));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(c => c.Information(It.IsAny<string>()));

            var controller = new SensorsController(sensorServiceMock.Object);

            // Act
            var result = controller.GetSensors().GetAwaiter().GetResult();

            // Assert
            Assert.IsType<List<SensorDTO>>(result);
        }

        [Fact]
        public void GetSensor_WithInvalidModel_Returns_BadRequestResult()
        {
            // Arrange
            var sensorServiceMock = new Mock<ISensorService>();
            sensorServiceMock.Setup(service => service
                .GetSensorByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(GetSensor()));

            var controller = new SensorsController(sensorServiceMock.Object);
            controller.ModelState.AddModelError("Id", "InvalidId");

            var id = 1;

            // Act
            var result = controller.GetSensor(id).GetAwaiter().GetResult();

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void GetSensor_WithValidModelAndValidId_Returns_SensorDTO()
        {
            // Arrange
            var sensorServiceMock = new Mock<ISensorService>();
            sensorServiceMock.Setup(service => service
                .GetSensorByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(GetSensor()));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(c => c.Information(It.IsAny<string>()));

            var controller = new SensorsController(sensorServiceMock.Object);
            var id = 1;

            // Act
            var result = controller.GetSensor(id).GetAwaiter().GetResult();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<SensorDTO>(okObjectResult.Value);
        }

        [Fact]
        public void GetSensor_WithValidModelAndInvalidId_Returns_NotFoundResult()
        {
            // Arrange
            var sensorServiceMock = new Mock<ISensorService>();
            sensorServiceMock.Setup(service => service
                .GetSensorByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(GetNullSensor()));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(c => c.Warning(It.IsAny<string>()));

            var controller = new SensorsController(sensorServiceMock.Object);
            var id = 2;

            // Act
            var result = controller.GetSensor(id).GetAwaiter().GetResult();

            // Assert
            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.IsAssignableFrom<int>(notFoundObjectResult.Value);
        }

        [Fact]
        public void RegisterNewSensorPost_WithInvalidModel_Returns_BadRequestResult()
        {
            // Arrange
            var sensorServiceMock = new Mock<ISensorService>();

            var controller = new SensorsController(sensorServiceMock.Object);
            controller.ModelState.AddModelError("Error", "Model Error");
            var sensorDTO = new SensorDTO();

            // Act
            var result = controller.RegisterNewSensor(sensorDTO).GetAwaiter().GetResult();

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void RegisterNewSensorPost_WithValidExistingModel_Returns_ConflictResult()
        {
            // Arrange
            var sensorServiceMock = new Mock<ISensorService>();
            sensorServiceMock.Setup(service => service
                .RegisterNewSensorAsync(It.IsAny<SensorDTO>()))
                .Returns(Task.FromResult((1, false)));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(c => c.Warning(It.IsAny<string>()));

            var controller = new SensorsController(sensorServiceMock.Object);
            var sensorDTO = new SensorDTO();

            // Act
            var result = controller.RegisterNewSensor(sensorDTO).GetAwaiter().GetResult();

            // Assert
            var conflictObjectResult = Assert.IsType<ConflictObjectResult>(result);
            Assert.IsAssignableFrom<int>(conflictObjectResult.Value);
        }

        [Fact]
        public void RegisterNewSensorPost_WithValidModel_Returns_CreatedAtActionResult()
        {
            // Arrange
            var sensorServiceMock = new Mock<ISensorService>();
            sensorServiceMock.Setup(service => service
                .RegisterNewSensorAsync(It.IsAny<SensorDTO>()))
                .Returns(Task.FromResult((1, true)));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(c => c.Information(It.IsAny<string>()));

            var controller = new SensorsController(sensorServiceMock.Object);
            var sensorDTO = new SensorDTO();

            // Act
            var result = controller.RegisterNewSensor(sensorDTO).GetAwaiter().GetResult();

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.IsAssignableFrom<SensorDTO>(createdAtActionResult.Value);
        }

        [Fact]
        public void UpdateSensor_WithInvalidModel_Returns_BadRequestResult()
        {
            // Arrange
            var sensorServiceMock = new Mock<ISensorService>();

            var controller = new SensorsController(sensorServiceMock.Object);
            controller.ModelState.AddModelError("Error", "Model Error");
            var sensorDTO = new SensorDTO();

            // Act
            var result = controller.UpdateSensor(sensorDTO).GetAwaiter().GetResult();

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void UpdateSensor_WithInvalidModelId_Returns_BadRequestResult()
        {
            // Arrange
            var sensorServiceMock = new Mock<ISensorService>();

            var controller = new SensorsController(sensorServiceMock.Object);
            var sensorDTO = new SensorDTO { Id = -1 };

            // Act
            var result = controller.UpdateSensor(sensorDTO).GetAwaiter().GetResult();

            // Assert
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsAssignableFrom<int>(badRequestObjectResult.Value);
        }

        [Fact]
        public void UpdateSensor_WithNonexistingId_Returns_NotFoundResult()
        {
            // Arrange
            var sensorServiceMock = new Mock<ISensorService>();
            sensorServiceMock.Setup(service => service
                .GetSensorByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(GetNullSensor()));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(c => c.Warning(It.IsAny<string>()));

            var controller = new SensorsController(sensorServiceMock.Object);
            var sensorDTO = new SensorDTO { Id = 2 };

            // Act
            var result = controller.UpdateSensor(sensorDTO).GetAwaiter().GetResult();

            // Assert
            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.IsAssignableFrom<int>(notFoundObjectResult.Value);
        }

        [Fact]
        public void UpdateSensor_WithUnknownSensorType_Returns_ConflictResult()
        {
            // Arrange
            var sensorServiceMock = new Mock<ISensorService>();

            sensorServiceMock.Setup(service => service
                .GetSensorByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(GetSensor()));

            sensorServiceMock.Setup(service => service
                .UpdateSensorAsync(It.IsAny<SensorDTO>()))
                .Returns(Task.FromResult(false));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(c => c.Warning(It.IsAny<string>()));

            var controller = new SensorsController(sensorServiceMock.Object);
            var sensorDTO = new SensorDTO { Id = 1 };

            // Act
            var result = controller.UpdateSensor(sensorDTO).GetAwaiter().GetResult();

            // Assert
            var conflictResult = Assert.IsType<ConflictResult>(result);
        }

        [Fact]
        public void UpdateSensor_WithValidModel_Returns_OkResult()
        {
            // Arrange
            var sensorServiceMock = new Mock<ISensorService>();

            sensorServiceMock.Setup(service => service
                .GetSensorByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(GetSensor()));

            sensorServiceMock.Setup(service => service
                .UpdateSensorAsync(It.IsAny<SensorDTO>()))
                .Returns(Task.FromResult(true));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(c => c.Information(It.IsAny<string>()));

            var controller = new SensorsController(sensorServiceMock.Object);
            var sensorDTO = new SensorDTO { Id = 1 };

            // Act
            var result = controller.UpdateSensor(sensorDTO).GetAwaiter().GetResult();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<SensorDTO>(okResult.Value);
        }

        [Fact]
        public void DeleteSensor_WithInvalidModelId_Returns_NotFoundResult()
        {
            // Arrange
            var sensorServiceMock = new Mock<ISensorService>();
            sensorServiceMock.Setup(service => service
                .DeleteSensorByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(false));


            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(c => c.Warning(It.IsAny<string>()));

            var controller = new SensorsController(sensorServiceMock.Object);
            var sensorDTO = new SensorDTO();
            var id = 1;

            // Act
            var result = controller.DeleteSensor(id).GetAwaiter().GetResult();

            // Assert
            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.IsAssignableFrom<int>(notFoundObjectResult.Value);
        }

        [Fact]
        public void DeleteSensor_Returns_OkResult()
        {
            // Arrange
            var sensorServiceMock = new Mock<ISensorService>();

            sensorServiceMock.Setup(service => service
                .GetSensorByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(GetSensor()));

            sensorServiceMock.Setup(service => service
                .DeleteSensorByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(true));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(c => c.Information(It.IsAny<string>()));

            var controller = new SensorsController(sensorServiceMock.Object);
            var id = 1;

            // Act
            var result = controller.DeleteSensor(id).GetAwaiter().GetResult();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<int>(okObjectResult.Value);
        }
    }
}