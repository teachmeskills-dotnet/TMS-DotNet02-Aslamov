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
    public class RecordsControllerTest : ConstrollerTestFixture
    {
        [Fact]
        public void GetRecords_Returns_CollectionOfSensorDTO()
        {
            // Arrange
            var recordServiceMock = new Mock<IRecordService>();
            recordServiceMock.Setup(service => service
                .GetAllRecordsAsync(null))
                .Returns(Task.FromResult(GetAllRecords()));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(c => c.Information(It.IsAny<string>()));

            var controller = new RecordsController(recordServiceMock.Object);

            // Act
            var result = controller.GetRecords(null).GetAwaiter().GetResult();

            // Assert
            Assert.IsType<List<RecordDTO>>(result);
        }

        [Fact]
        public void GetRecord_WithInvalidModel_Returns_BadRequestResult()
        {
            // Arrange
            var recordServiceMock = new Mock<IRecordService>();
            recordServiceMock.Setup(service => service
                .GetRecordByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(GetRecord()));

            var controller = new RecordsController(recordServiceMock.Object);
            controller.ModelState.AddModelError("Id", "InvalidId");

            var id = 1;

            // Act
            var result = controller.GetRecord(id).GetAwaiter().GetResult();

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void GetRecord_WithValidModelAndValidId_Returns_RecordDTO()
        {
            // Arrange
            var recordServiceMock = new Mock<IRecordService>();
            recordServiceMock.Setup(service => service
                .GetRecordByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(GetRecord()));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(c => c.Information(It.IsAny<string>()));

            var controller = new RecordsController(recordServiceMock.Object);
            var id = 1;

            // Act
            var result = controller.GetRecord(id).GetAwaiter().GetResult();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<RecordDTO>(okObjectResult.Value);
        }

        [Fact]
        public void GetRecord_WithValidModelAndInvalidId_Returns_NotFoundResult()
        {
            // Arrange
            var recordServiceMock = new Mock<IRecordService>();
            recordServiceMock.Setup(service => service
                .GetRecordByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(GetNullRecord()));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(c => c.Warning(It.IsAny<string>()));

            var controller = new RecordsController(recordServiceMock.Object);
            var id = 2;

            // Act
            var result = controller.GetRecord(id).GetAwaiter().GetResult();

            // Assert
            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.IsAssignableFrom<int>(notFoundObjectResult.Value);
        }

        [Fact]
        public void RegisterNewRecordPost_WithInvalidModel_Returns_BadRequestResult()
        {
            // Arrange
            var recordServiceMock = new Mock<IRecordService>();

            var controller = new RecordsController(recordServiceMock.Object);
            controller.ModelState.AddModelError("Error", "Model Error");
            var recordDTO= new RecordDTO();

            // Act
            var result = controller.RegisterNewRecord(recordDTO).GetAwaiter().GetResult();

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void RegisterNewRecordPost_WithValidExistingModel_Returns_ConflictResult()
        {
            // Arrange
            var recordServiceMock = new Mock<IRecordService>();
            recordServiceMock.Setup(service => service
                .RegisterNewRecordAsync(It.IsAny<RecordDTO>()))
                .Returns(Task.FromResult((1, false)));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(c => c.Warning(It.IsAny<string>()));

            var controller = new RecordsController(recordServiceMock.Object);
            var recordDTO = new RecordDTO();

            // Act
            var result = controller.RegisterNewRecord(recordDTO).GetAwaiter().GetResult();

            // Assert
            var conflictObjectResult = Assert.IsType<ConflictObjectResult>(result);
            Assert.IsAssignableFrom<int>(conflictObjectResult.Value);
        }

        [Fact]
        public void RegisterNewRecordPost_WithValidModel_Returns_CreatedAtActionResult()
        {
            // Arrange
            var recordServiceMock = new Mock<IRecordService>();
            recordServiceMock.Setup(service => service
                .RegisterNewRecordAsync(It.IsAny<RecordDTO>()))
                .Returns(Task.FromResult((1, true)));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(c => c.Information(It.IsAny<string>()));

            var controller = new RecordsController(recordServiceMock.Object);
            var recordDTO = new RecordDTO();

            // Act
            var result = controller.RegisterNewRecord(recordDTO).GetAwaiter().GetResult();

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.IsAssignableFrom<RecordDTO>(createdAtActionResult.Value);
        }

        [Fact]
        public void UpdateRecord_WithInvalidModel_Returns_BadRequestResult()
        {
            // Arrange
            var recordServiceMock = new Mock<IRecordService>();

            var controller = new RecordsController(recordServiceMock.Object);
            controller.ModelState.AddModelError("Error", "Model Error");
            var recordDTO = new RecordDTO();

            // Act
            var result = controller.UpdateRecord(recordDTO).GetAwaiter().GetResult();

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void UpdateRecord_WithInvalidModelId_Returns_BadRequestResult()
        {
            // Arrange
            var recordServiceMock = new Mock<IRecordService>();

            var controller = new RecordsController(recordServiceMock.Object);
            var recordDTO = new RecordDTO { Id = -1 };

            // Act
            var result = controller.UpdateRecord(recordDTO).GetAwaiter().GetResult();

            // Assert
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsAssignableFrom<int>(badRequestObjectResult.Value);
        }

        [Fact]
        public void UpdateRecord_WithNonexistingId_Returns_NotFoundResult()
        {
            // Arrange
            var recordServiceMock = new Mock<IRecordService>();
            recordServiceMock.Setup(service => service
                .GetRecordByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(GetNullRecord()));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(c => c.Warning(It.IsAny<string>()));

            var controller = new RecordsController(recordServiceMock.Object);
            var recordDTO = new RecordDTO { Id = 2 };

            // Act
            var result = controller.UpdateRecord(recordDTO).GetAwaiter().GetResult();

            // Assert
            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.IsAssignableFrom<int>(notFoundObjectResult.Value);
        }

        [Fact]
        public void UpdateRecord_WithNullValue_Returns_ConflictResult()
        {
            // Arrange
            var recordServiceMock = new Mock<IRecordService>();

            recordServiceMock.Setup(service => service
                .GetRecordByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(GetRecord()));

            recordServiceMock.Setup(service => service
                .UpdateRecordAsync(It.IsAny<RecordDTO>()))
                .Returns(Task.FromResult(false));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(c => c.Warning(It.IsAny<string>()));

            var controller = new RecordsController(recordServiceMock.Object);
            var recordDTO = new RecordDTO { Id = 1 };

            // Act
            var result = controller.UpdateRecord(recordDTO).GetAwaiter().GetResult();

            // Assert
            var conflictResult = Assert.IsType<ConflictResult>(result);
        }

        [Fact]
        public void UpdateRecord_WithValidModel_Returns_OkResult()
        {
            // Arrange
            var recordServiceMock = new Mock<IRecordService>();

            recordServiceMock.Setup(service => service
                .GetRecordByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(GetRecord()));

            recordServiceMock.Setup(service => service
                .UpdateRecordAsync(It.IsAny<RecordDTO>()))
                .Returns(Task.FromResult(true));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(c => c.Information(It.IsAny<string>()));

            var controller = new RecordsController(recordServiceMock.Object);
            var recordDTO = new RecordDTO { Id = 1 };

            // Act
            var result = controller.UpdateRecord(recordDTO).GetAwaiter().GetResult();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<RecordDTO>(okResult.Value);
        }

        [Fact]
        public void DeleteRecord_WithInvalidModelId_Returns_NotFoundResult()
        {
            // Arrange
            var recordServiceMock = new Mock<IRecordService>();
            recordServiceMock.Setup(service => service
                .DeleteRecordByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(false));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(c => c.Warning(It.IsAny<string>()));

            var controller = new RecordsController(recordServiceMock.Object);
            var recordDTO = new RecordDTO();
            var id = 1;

            // Act
            var result = controller.DeleteRecord(id).GetAwaiter().GetResult();

            // Assert
            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.IsAssignableFrom<int>(notFoundObjectResult.Value);
        }

        [Fact]
        public void DeleteRecord_Returns_OkResult()
        {
            // Arrange
            var recordServiceMock = new Mock<IRecordService>();

            recordServiceMock.Setup(service => service
                .GetRecordByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(GetRecord()));

            recordServiceMock.Setup(service => service
                .DeleteRecordByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(true));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(c => c.Information(It.IsAny<string>()));

            var controller = new RecordsController(recordServiceMock.Object);
            var id = 1;

            // Act
            var result = controller.DeleteRecord(id).GetAwaiter().GetResult();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<int>(okObjectResult.Value);
        }

        [Fact]
        public void DeleteRecords_Returns_OkResult()
        {
            // Arrange
            var recordServiceMock = new Mock<IRecordService>();

            recordServiceMock.Setup(service => service
                .DeleteAllRecordsAsync());

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(c => c.Information(It.IsAny<string>()));

            var controller = new RecordsController(recordServiceMock.Object);

            // Act
            var result = controller.DeleteRecords().GetAwaiter().GetResult();

            // Assert
            var okObjectResult = Assert.IsType<OkResult>(result);
        }
    }
}