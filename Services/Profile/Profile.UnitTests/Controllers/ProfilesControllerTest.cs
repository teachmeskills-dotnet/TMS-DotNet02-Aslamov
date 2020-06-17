using Microsoft.AspNetCore.Mvc;
using Moq;
using Profile.API.Common.Interfaces;
using Profile.API.Controllers;
using Profile.API.DTO;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Profile.UnitTests.Controllers
{
    public class ProfilesControllerTest : ConstrollerTestFixture
    {
        [Fact]
        public void GetProfiles_Returns_CollectionOfProfileDTO()
        {
            // Arrange
            var profileServiceMock = new Mock<IProfileService>();
            profileServiceMock.Setup(service => service
                .GetAllProfilesAsync())
                .Returns(Task.FromResult(GetAllProfiles()));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(c => c.Information(It.IsAny<string>()));

            var controller = new ProfilesController(profileServiceMock.Object, loggerMock.Object);

            // Act
            var result = controller.GetProfiles().GetAwaiter().GetResult();

            // Assert
            Assert.IsType<List<ProfileDTO>>(result);
        }

        [Fact]
        public void GetProfile_WithInvalidModel_Returns_BadRequestResult()
        {
            // Arrange
            var profileServiceMock = new Mock<IProfileService>();
            profileServiceMock.Setup(service => service
                .GetProfileByIdAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(GetProfile()));

            var loggerMock = new Mock<ILogger>();

            var controller = new ProfilesController(profileServiceMock.Object, loggerMock.Object);
            controller.ModelState.AddModelError("Id", "InvalidId");

            var id = new Guid();

            // Act
            var result = controller.GetProfile(id).GetAwaiter().GetResult();

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void GetProfile_WithValidModelAndValidId_Returns_ProfileDTO()
        {
            // Arrange
            var profileServiceMock = new Mock<IProfileService>();
            profileServiceMock.Setup(service => service
                .GetProfileByIdAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(GetProfile()));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(c => c.Information(It.IsAny<string>()));

            var controller = new ProfilesController(profileServiceMock.Object, loggerMock.Object);
            var id = new Guid();

            // Act
            var result = controller.GetProfile(id).GetAwaiter().GetResult();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<ProfileDTO>(okObjectResult.Value);
        }

        [Fact]
        public void GetProfile_WithValidModelAndInvalidId_Returns_NotFoundResult()
        {
            // Arrange
            var profileServiceMock = new Mock<IProfileService>();
            profileServiceMock.Setup(service => service
                .GetProfileByIdAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(GetNullProfile()));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(c => c.Warning(It.IsAny<string>()));

            var controller = new ProfilesController(profileServiceMock.Object, loggerMock.Object);
            var id = new Guid();

            // Act
            var result = controller.GetProfile(id).GetAwaiter().GetResult();

            // Assert
            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.IsAssignableFrom<Guid>(notFoundObjectResult.Value);
        }

        [Fact]
        public void RegisterNewReportPost_WithInvalidModel_Returns_BadRequestResult()
        {
            // Arrange
            var profileServiceMock = new Mock<IProfileService>();
            var loggerMock = new Mock<ILogger>();

            var controller = new ProfilesController(profileServiceMock.Object, loggerMock.Object);
            controller.ModelState.AddModelError("Error", "Model Error");
            var profileDTO = new ProfileDTO();

            // Act
            var result = controller.RegisterNewProfile(profileDTO).GetAwaiter().GetResult();

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void RegisterNewProfilePost_WithValidExistingModel_Returns_ConflictResult()
        {
            // Arrange
            var profileServiceMock = new Mock<IProfileService>();
            profileServiceMock.Setup(service => service
                .RegisterNewProfileAsync(It.IsAny<ProfileDTO>()))
                .Returns(Task.FromResult((new Guid(), false)));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(c => c.Warning(It.IsAny<string>()));

            var controller = new ProfilesController(profileServiceMock.Object, loggerMock.Object);
            var profileDTO = new ProfileDTO();

            // Act
            var result = controller.RegisterNewProfile(profileDTO).GetAwaiter().GetResult();

            // Assert
            var conflictObjectResult = Assert.IsType<ConflictObjectResult>(result);
            Assert.IsAssignableFrom<Guid>(conflictObjectResult.Value);
        }

        [Fact]
        public void RegisterNewProfilePost_WithValidModel_Returns_CreatedAtActionResult()
        {
            // Arrange
            var profileServiceMock = new Mock<IProfileService>();
            profileServiceMock.Setup(service => service
                .RegisterNewProfileAsync(It.IsAny<ProfileDTO>()))
                .Returns(Task.FromResult((new Guid(), true)));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(c => c.Information(It.IsAny<string>()));

            var controller = new ProfilesController(profileServiceMock.Object, loggerMock.Object);
            var profileDTO = new ProfileDTO();

            // Act
            var result = controller.RegisterNewProfile(profileDTO).GetAwaiter().GetResult();

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.IsAssignableFrom<ProfileDTO>(createdAtActionResult.Value);
        }

        [Fact]
        public void UpdateProfile_WithInvalidModel_Returns_BadRequestResult()
        {
            // Arrange
            var profileServiceMock = new Mock<IProfileService>();
            var loggerMock = new Mock<ILogger>();

            var controller = new ProfilesController(profileServiceMock.Object, loggerMock.Object);
            controller.ModelState.AddModelError("Error", "Model Error");
            var profileDTO = new ProfileDTO();

            // Act
            var result = controller.UpdateProfile(profileDTO).GetAwaiter().GetResult();

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void UpdateProfile_WithInvalidModelId_Returns_BadRequestResult()
        {
            // Arrange
            var profileServiceMock = new Mock<IProfileService>();
            var loggerMock = new Mock<ILogger>();

            var controller = new ProfilesController(profileServiceMock.Object, loggerMock.Object);
            var sensorDTO = new ProfileDTO { Id = Guid.Empty };

            // Act
            var result = controller.UpdateProfile(sensorDTO).GetAwaiter().GetResult();

            // Assert
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsAssignableFrom<Guid>(badRequestObjectResult.Value);
        }

        [Fact]
        public void UpdateProfile_WithNonexistingId_Returns_NotFoundResult()
        {
            // Arrange
            var profileServiceMock = new Mock<IProfileService>();
            profileServiceMock.Setup(service => service
                .GetProfileByIdAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(GetNullProfile()));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(c => c.Warning(It.IsAny<string>()));

            var controller = new ProfilesController(profileServiceMock.Object, loggerMock.Object);
            var profileDTO = new ProfileDTO { Id = Guid.Parse("11111111-1111-1111-1111-111111111111") };

            // Act
            var result = controller.UpdateProfile(profileDTO).GetAwaiter().GetResult();

            // Assert
            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.IsAssignableFrom<Guid>(notFoundObjectResult.Value);
        }

        [Fact]
        public void UpdateProfile_WithUpdateError_Returns_ConflictResult()
        {
            // Arrange
            var profileServiceMock = new Mock<IProfileService>();

            profileServiceMock.Setup(service => service
                .GetProfileByIdAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(GetProfile()));

            profileServiceMock.Setup(service => service
                .UpdateProfileAsync(It.IsAny<ProfileDTO>()))
                .Returns(Task.FromResult(false));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(c => c.Warning(It.IsAny<string>()));

            var controller = new ProfilesController(profileServiceMock.Object, loggerMock.Object);
            var profileDTO = new ProfileDTO { Id = Guid.Parse("11111111-1111-1111-1111-111111111111") };

            // Act
            var result = controller.UpdateProfile(profileDTO).GetAwaiter().GetResult();

            // Assert
            var conflictResult = Assert.IsType<ConflictResult>(result);
        }

        [Fact]
        public void UpdateProfile_WithValidModel_Returns_OkResult()
        {
            // Arrange
            var profileServiceMock = new Mock<IProfileService>();

            profileServiceMock.Setup(service => service
                .GetProfileByIdAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(GetProfile()));

            profileServiceMock.Setup(service => service
                .UpdateProfileAsync(It.IsAny<ProfileDTO>()))
                .Returns(Task.FromResult(true));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(c => c.Information(It.IsAny<string>()));

            var controller = new ProfilesController(profileServiceMock.Object, loggerMock.Object);
            var profileDTO = new ProfileDTO { Id = Guid.Parse("11111111-1111-1111-1111-111111111111") };

            // Act
            var result = controller.UpdateProfile(profileDTO).GetAwaiter().GetResult();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<ProfileDTO>(okResult.Value);
        }

        [Fact]
        public void DeleteProfile_WithInvalidModelId_Returns_NotFoundResult()
        {
            // Arrange
            var profileServiceMock = new Mock<IProfileService>();
            profileServiceMock.Setup(service => service
                .DeleteProfileByIdAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(false));


            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(c => c.Warning(It.IsAny<string>()));

            var controller = new ProfilesController(profileServiceMock.Object, loggerMock.Object);
            var profileDTO = new ProfileDTO();
            var id = new Guid();

            // Act
            var result = controller.DeleteProfile(id).GetAwaiter().GetResult();

            // Assert
            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.IsAssignableFrom<Guid>(notFoundObjectResult.Value);
        }

        [Fact]
        public void DeleteProfile_Returns_OkResult()
        {
            // Arrange
            var profileServiceMock = new Mock<IProfileService>();

            profileServiceMock.Setup(service => service
                .GetProfileByIdAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(GetProfile()));

            profileServiceMock.Setup(service => service
                .DeleteProfileByIdAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(true));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(c => c.Information(It.IsAny<string>()));

            var controller = new ProfilesController(profileServiceMock.Object, loggerMock.Object);
            var id = new Guid();

            // Act
            var result = controller.DeleteProfile(id).GetAwaiter().GetResult();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<Guid>(okObjectResult.Value);
        }
    }
}