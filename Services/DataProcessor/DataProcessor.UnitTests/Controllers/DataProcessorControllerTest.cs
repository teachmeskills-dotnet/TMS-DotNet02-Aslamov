using DataProcessor.API.Common.Interfaces;
using DataProcessor.API.Controllers;
using DataProcessor.API.DTO;
using EventBus.Contracts.Commands;
using EventBus.Contracts.Common;
using EventBus.Contracts.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace DataProcessor.UnitTests.Controllers
{
    /// <summary>
    /// Define unit tests for the DataProcessorController.
    /// </summary>
    public class DataProcessorControllerTest : ControllerTestFixture
    {
        [Fact]
        public void ProcessData_WithInvalidModel_Returns_BadRequestResult()
        {
            // Arrange
            var dataProcessorServiceMock = new Mock<IDataProcessorService>();

            var loggerMock = new Mock<ILogger<DataProcessorController>>();

            var commandProducerMock = new Mock<ICommandProducer<IRegisterReport,IReportDTO>>();
            commandProducerMock.Setup(producer => producer
                .Send(It.IsAny<IReportDTO>()))
                .Returns(Task.FromResult(true));

            var controller = new DataProcessorController(dataProcessorServiceMock.Object, loggerMock.Object, commandProducerMock.Object);
            controller.ModelState.AddModelError("Model", "SomeError");

            var model = new DataDTO();

            // Act
            var result = controller.ProcessData(model).GetAwaiter().GetResult();

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void ProcessData_WithValidModelAndProcessingError_Returns_ConflictResult()
        {
            // Arrange
            var dataProcessorServiceMock = new Mock<IDataProcessorService>();
            dataProcessorServiceMock.Setup(service => service
                .ProcessData(It.IsAny<DataDTO>()))
                .Returns(Task.FromResult<(ReportDTO, bool)>((null, false)));

            var loggerMock = new Mock<ILogger<DataProcessorController>>();

            var commandProducerMock = new Mock<ICommandProducer<IRegisterReport, IReportDTO>>();
            commandProducerMock.Setup(producer => producer
                .Send(It.IsAny<IReportDTO>()))
                .Returns(Task.FromResult(true));

            var controller = new DataProcessorController(dataProcessorServiceMock.Object, loggerMock.Object, commandProducerMock.Object);
            var model = GetData();

            // Act
            var result = controller.ProcessData(model).GetAwaiter().GetResult();

            // Assert
            Assert.IsType<ConflictResult>(result);
        }

        [Fact]
        public void ProcessData_WithValidModel_Returns_OkResult()
        {
            // Arrange
            var dataProcessorServiceMock = new Mock<IDataProcessorService>();
            dataProcessorServiceMock.Setup(service => service
                .ProcessData(It.IsAny<DataDTO>()))
                .Returns(Task.FromResult<(ReportDTO, bool)>((GetReport(), true)));

            var loggerMock = new Mock<ILogger<DataProcessorController>>();

            var commandProducerMock = new Mock<ICommandProducer<IRegisterReport, IReportDTO>>();
            commandProducerMock.Setup(producer => producer
                .Send(It.IsAny<IReportDTO>()))
                .Returns(Task.FromResult(true));

            var controller = new DataProcessorController(dataProcessorServiceMock.Object, loggerMock.Object, commandProducerMock.Object);
            var model = GetData();

            // Act
            var result = controller.ProcessData(model).GetAwaiter().GetResult();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<ReportDTO>(okObjectResult.Value);
        }
    }
}