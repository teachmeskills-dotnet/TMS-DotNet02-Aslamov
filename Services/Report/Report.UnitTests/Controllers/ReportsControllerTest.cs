using Microsoft.AspNetCore.Mvc;
using Moq;
using Report.API.Common.Interfaces;
using Report.API.Controllers;
using Report.API.DTO;
using Serilog;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Report.UnitTests.Controllers
{
    public class ReportsControllerTest : ConstrollerTestFixture
    {
        [Fact]
        public void GetReports_Returns_CollectionOfReportDTO()
        {
            // Arrange
            var profileServiceMock = new Mock<IReportService>();
            profileServiceMock.Setup(service => service
                .GetAllReportsAsync())
                .Returns(Task.FromResult(GetAllReports()));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(c => c.Information(It.IsAny<string>()));

            var controller = new ReportsController(profileServiceMock.Object, loggerMock.Object);

            // Act
            var result = controller.GetReports().GetAwaiter().GetResult();

            // Assert
            Assert.IsType<List<ReportDTO>>(result);
        }

        [Fact]
        public void GetReport_WithInvalidModel_Returns_BadRequestResult()
        {
            // Arrange
            var profileServiceMock = new Mock<IReportService>();
            profileServiceMock.Setup(service => service
                .GetReportByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(GetReport()));

            var loggerMock = new Mock<ILogger>();

            var controller = new ReportsController(profileServiceMock.Object, loggerMock.Object);
            controller.ModelState.AddModelError("Id", "InvalidId");

            var id = 1;

            // Act
            var result = controller.GetReport(id).GetAwaiter().GetResult();

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void GetReport_WithValidModelAndValidId_Returns_ReportDTO()
        {
            // Arrange
            var profileServiceMock = new Mock<IReportService>();
            profileServiceMock.Setup(service => service
                .GetReportByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(GetReport()));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(c => c.Information(It.IsAny<string>()));

            var controller = new ReportsController(profileServiceMock.Object, loggerMock.Object);
            var id = 1;

            // Act
            var result = controller.GetReport(id).GetAwaiter().GetResult();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<ReportDTO>(okObjectResult.Value);
        }

        [Fact]
        public void GetReport_WithValidModelAndInvalidId_Returns_NotFoundResult()
        {
            // Arrange
            var profileServiceMock = new Mock<IReportService>();
            profileServiceMock.Setup(service => service
                .GetReportByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(GetNullReport()));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(c => c.Warning(It.IsAny<string>()));

            var controller = new ReportsController(profileServiceMock.Object, loggerMock.Object);
            var id = 1;

            // Act
            var result = controller.GetReport(id).GetAwaiter().GetResult();

            // Assert
            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.IsAssignableFrom<int>(notFoundObjectResult.Value);
        }

        [Fact]
        public void RegisterNewReport_WithInvalidModel_Returns_BadRequestResult()
        {
            // Arrange
            var profileServiceMock = new Mock<IReportService>();
            var loggerMock = new Mock<ILogger>();

            var controller = new ReportsController(profileServiceMock.Object, loggerMock.Object);
            controller.ModelState.AddModelError("Error", "Model Error");
            var profileDTO = new ReportDTO();

            // Act
            var result = controller.RegisterNewReport(profileDTO).GetAwaiter().GetResult();

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void RegisterNewReport_WithValidExistingModel_Returns_ConflictResult()
        {
            // Arrange
            var profileServiceMock = new Mock<IReportService>();
            profileServiceMock.Setup(service => service
                .RegisterNewReportAsync(It.IsAny<ReportDTO>()))
                .Returns(Task.FromResult((0, false)));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(c => c.Warning(It.IsAny<string>()));

            var controller = new ReportsController(profileServiceMock.Object, loggerMock.Object);
            var profileDTO = new ReportDTO();

            // Act
            var result = controller.RegisterNewReport(profileDTO).GetAwaiter().GetResult();

            // Assert
            var conflictObjectResult = Assert.IsType<ConflictObjectResult>(result);
            Assert.IsAssignableFrom<int>(conflictObjectResult.Value);
        }

        [Fact]
        public void RegisterNewReport_WithValidModel_Returns_CreatedAtActionResult()
        {
            // Arrange
            var profileServiceMock = new Mock<IReportService>();
            profileServiceMock.Setup(service => service
                .RegisterNewReportAsync(It.IsAny<ReportDTO>()))
                .Returns(Task.FromResult((1, true)));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(c => c.Information(It.IsAny<string>()));

            var controller = new ReportsController(profileServiceMock.Object, loggerMock.Object);
            var profileDTO = new ReportDTO();

            // Act
            var result = controller.RegisterNewReport(profileDTO).GetAwaiter().GetResult();

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.IsAssignableFrom<ReportDTO>(createdAtActionResult.Value);
        }

        [Fact]
        public void UpdateReport_WithInvalidModel_Returns_BadRequestResult()
        {
            // Arrange
            var profileServiceMock = new Mock<IReportService>();
            var loggerMock = new Mock<ILogger>();

            var controller = new ReportsController(profileServiceMock.Object, loggerMock.Object);
            controller.ModelState.AddModelError("Error", "Model Error");
            var profileDTO = new ReportDTO();

            // Act
            var result = controller.UpdateReport(profileDTO).GetAwaiter().GetResult();

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void UpdateReport_WithInvalidModelId_Returns_BadRequestResult()
        {
            // Arrange
            var profileServiceMock = new Mock<IReportService>();
            var loggerMock = new Mock<ILogger>();

            var controller = new ReportsController(profileServiceMock.Object, loggerMock.Object);
            var sensorDTO = new ReportDTO { Id = 0 };

            // Act
            var result = controller.UpdateReport(sensorDTO).GetAwaiter().GetResult();

            // Assert
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsAssignableFrom<int>(badRequestObjectResult.Value);
        }

        [Fact]
        public void UpdateReport_WithNonexistingId_Returns_NotFoundResult()
        {
            // Arrange
            var profileServiceMock = new Mock<IReportService>();
            profileServiceMock.Setup(service => service
                .GetReportByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(GetNullReport()));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(c => c.Warning(It.IsAny<string>()));

            var controller = new ReportsController(profileServiceMock.Object, loggerMock.Object);
            var profileDTO = new ReportDTO { Id = 1 };

            // Act
            var result = controller.UpdateReport(profileDTO).GetAwaiter().GetResult();

            // Assert
            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.IsAssignableFrom<int>(notFoundObjectResult.Value);
        }

        [Fact]
        public void UpdateReport_WithUpdateError_Returns_ConflictResult()
        {
            // Arrange
            var profileServiceMock = new Mock<IReportService>();

            profileServiceMock.Setup(service => service
                .GetReportByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(GetReport()));

            profileServiceMock.Setup(service => service
                .UpdateReportAsync(It.IsAny<ReportDTO>()))
                .Returns(Task.FromResult(false));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(c => c.Warning(It.IsAny<string>()));

            var controller = new ReportsController(profileServiceMock.Object, loggerMock.Object);
            var profileDTO = new ReportDTO { Id = 1 };

            // Act
            var result = controller.UpdateReport(profileDTO).GetAwaiter().GetResult();

            // Assert
            var conflictResult = Assert.IsType<ConflictResult>(result);
        }

        [Fact]
        public void UpdateReport_WithValidModel_Returns_OkResult()
        {
            // Arrange
            var profileServiceMock = new Mock<IReportService>();

            profileServiceMock.Setup(service => service
                .GetReportByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(GetReport()));

            profileServiceMock.Setup(service => service
                .UpdateReportAsync(It.IsAny<ReportDTO>()))
                .Returns(Task.FromResult(true));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(c => c.Information(It.IsAny<string>()));

            var controller = new ReportsController(profileServiceMock.Object, loggerMock.Object);
            var profileDTO = new ReportDTO { Id = 1 };

            // Act
            var result = controller.UpdateReport(profileDTO).GetAwaiter().GetResult();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<ReportDTO>(okResult.Value);
        }

        [Fact]
        public void DeleteReport_WithInvalidModelId_Returns_NotFoundResult()
        {
            // Arrange
            var profileServiceMock = new Mock<IReportService>();
            profileServiceMock.Setup(service => service
                .DeleteReportByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(false));


            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(c => c.Warning(It.IsAny<string>()));

            var controller = new ReportsController(profileServiceMock.Object, loggerMock.Object);
            var profileDTO = new ReportDTO();
            var id = 1;

            // Act
            var result = controller.DeleteReport(id).GetAwaiter().GetResult();

            // Assert
            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.IsAssignableFrom<int>(notFoundObjectResult.Value);
        }

        [Fact]
        public void DeleteReport_Returns_OkResult()
        {
            // Arrange
            var profileServiceMock = new Mock<IReportService>();

            profileServiceMock.Setup(service => service
                .GetReportByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(GetReport()));

            profileServiceMock.Setup(service => service
                .DeleteReportByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(true));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(c => c.Information(It.IsAny<string>()));

            var controller = new ReportsController(profileServiceMock.Object, loggerMock.Object);
            var id = 1;

            // Act
            var result = controller.DeleteReport(id).GetAwaiter().GetResult();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<int>(okObjectResult.Value);
        }
    }
}