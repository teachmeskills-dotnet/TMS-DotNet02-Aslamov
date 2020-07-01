using Identity.API.Common.Interfaces;
using Identity.API.Controllers;
using Identity.API.DTO;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Identity.UnitTests.Controllers
{
    /// <summary>
    /// Define class for AccountController testing.
    /// </summary>
    public class AccountsControllerTest : ConstrollerTestFixture
    {
        [Fact]
        public void GetAccounts_Returns_CollectionOfAccountDTO()
        {
            // Arrange
            var accountServiceMock = new Mock<IAccountService>();
            accountServiceMock.Setup(service => service
                .GetAllAccountsAsync())
                .Returns(Task.FromResult(GetAllAccounts()));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(c => c.Information(It.IsAny<string>()));

            var controller = new AccountsController(accountServiceMock.Object, loggerMock.Object);

            // Act
            var result = controller.GetAccounts().GetAwaiter().GetResult();

            // Assert
            Assert.IsType<List<AccountDTO>>(result);
        }

        [Fact]
        public void GetAccount_WithInvalidModel_Returns_BadRequestResult()
        {
            // Arrange
            var accountServiceMock = new Mock<IAccountService>();
            accountServiceMock.Setup(service => service
                .GetAccountByIdAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(GetAccount()));

            var loggerMock = new Mock<ILogger>();

            var controller = new AccountsController(accountServiceMock.Object, loggerMock.Object);
            controller.ModelState.AddModelError("Id", "InvalidId");

            var id = new Guid();

            // Act
            var result = controller.GetAccount(id).GetAwaiter().GetResult();

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void GetAccount_WithValidModelAndValidId_Returns_AccountDTO()
        {
            // Arrange
            var accountServiceMock = new Mock<IAccountService>();
            accountServiceMock.Setup(service => service
                .GetAccountByIdAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(GetAccount()));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(c => c.Information(It.IsAny<string>()));

            var controller = new AccountsController(accountServiceMock.Object, loggerMock.Object);
            var id = new Guid();

            // Act
            var result = controller.GetAccount(id).GetAwaiter().GetResult();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<AccountDTO>(okObjectResult.Value);
        }

        [Fact]
        public void GetAccount_WithValidModelAndInvalidId_Returns_NotFoundResult()
        {
            // Arrange
            var accountServiceMock = new Mock<IAccountService>();
            accountServiceMock.Setup(service => service
                .GetAccountByIdAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(GetNullAccount()));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(c => c.Warning(It.IsAny<string>()));

            var controller = new AccountsController(accountServiceMock.Object, loggerMock.Object);
            var id = new Guid();

            // Act
            var result = controller.GetAccount(id).GetAwaiter().GetResult();

            // Assert
            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.IsAssignableFrom<Guid>(notFoundObjectResult.Value);
        }

        [Fact]
        public void Login_WithInvalidModel_Returns_BadRequestResult()
        {
            // Arrange
            var accountServiceMock = new Mock<IAccountService>();
            var loggerMock = new Mock<ILogger>();

            var controller = new AccountsController(accountServiceMock.Object, loggerMock.Object);
            controller.ModelState.AddModelError("Error", "Model Error");
            var loginDTO = GetLoginData();

            // Act
            var result = controller.Login(loginDTO).GetAwaiter().GetResult();

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Login_WhenAccountDoesNotExist_Returns_NotFoundResult()
        {
            // Arrange
            var accountServiceMock = new Mock<IAccountService>();
            accountServiceMock.Setup(service => service
                .LoginAsync(It.IsAny<LoginDTO>()))
                .Returns(Task.FromResult(GetNullToken()));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(c => c.Warning(It.IsAny<string>()));

            var controller = new AccountsController(accountServiceMock.Object, loggerMock.Object);
            var loginDTO = GetLoginData();

            // Act
            var result = controller.Login(loginDTO).GetAwaiter().GetResult();

            // Assert
            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void Login_WithValidModel_Returns_AcceptedResult()
        {
            // Arrange
            var accountServiceMock = new Mock<IAccountService>();
            accountServiceMock.Setup(service => service
                .LoginAsync(It.IsAny<LoginDTO>()))
                .Returns(Task.FromResult(GetToken()));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(c => c.Information(It.IsAny<string>()));

            var controller = new AccountsController(accountServiceMock.Object, loggerMock.Object);
            var loginDTO = GetLoginData();

            controller.ControllerContext = GetFakeContext();

            // Act
            var result = controller.Login(loginDTO).GetAwaiter().GetResult();

            // Assert
            var acceptedResult = Assert.IsType<AcceptedResult>(result);
            Assert.IsAssignableFrom<TokenDTO>(acceptedResult.Value);
        }

        [Fact]
        public void RegisterNewAccount_WithInvalidModel_Returns_BadRequestResult()
        {
            // Arrange
            var accountServiceMock = new Mock<IAccountService>();
            var loggerMock = new Mock<ILogger>();

            var controller = new AccountsController(accountServiceMock.Object, loggerMock.Object);
            controller.ModelState.AddModelError("Error", "Model Error");
            var accountDTO = new AccountDTO();

            // Act
            var result = controller.RegisterNewAccount(accountDTO).GetAwaiter().GetResult();

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void RegisterNewAccount_WithValidExistingModel_Returns_ConflictResult()
        {
            // Arrange
            var accountServiceMock = new Mock<IAccountService>();
            accountServiceMock.Setup(service => service
                .RegisterAsync(It.IsAny<AccountDTO>()))
                .Returns(Task.FromResult((Guid.NewGuid(), false, "Conflict")));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(c => c.Warning(It.IsAny<string>()));

            var controller = new AccountsController(accountServiceMock.Object, loggerMock.Object);
            var accountDTO = new AccountDTO();

            // Act
            var result = controller.RegisterNewAccount(accountDTO).GetAwaiter().GetResult();

            // Assert
            var conflictObjectResult = Assert.IsType<ConflictObjectResult>(result);
        }

        [Fact]
        public void RegisterNewAccount_WithValidModel_Returns_CreatedAtActionResult()
        {
            // Arrange
            var accountServiceMock = new Mock<IAccountService>();
            accountServiceMock.Setup(service => service
                .RegisterAsync(It.IsAny<AccountDTO>()))
                .Returns(Task.FromResult((Guid.NewGuid(), true, "Success")));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(c => c.Information(It.IsAny<string>()));

            var controller = new AccountsController(accountServiceMock.Object, loggerMock.Object);
            var accountDTO = new AccountDTO();

            // Act
            var result = controller.RegisterNewAccount(accountDTO).GetAwaiter().GetResult();

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.IsAssignableFrom<AccountDTO>(createdAtActionResult.Value);
        }

        [Fact]
        public void UpdateAccount_WithInvalidModel_Returns_BadRequestResult()
        {
            // Arrange
            var accountServiceMock = new Mock<IAccountService>();
            var loggerMock = new Mock<ILogger>();

            var controller = new AccountsController(accountServiceMock.Object, loggerMock.Object);
            controller.ModelState.AddModelError("Error", "Model Error");
            var accountDTO = new AccountDTO();

            // Act
            var result = controller.UpdateAccount(accountDTO).GetAwaiter().GetResult();

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void UpdateAccount_WhenAccountDoesNotExist_Returns_NotFoundResult()
        {
            // Arrange
            var accountServiceMock = new Mock<IAccountService>();
            accountServiceMock.Setup(service => service
                .GetAccountByEmailAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(GetNullAccount()));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(c => c.Warning(It.IsAny<string>()));

            var controller = new AccountsController(accountServiceMock.Object, loggerMock.Object);
            var accountDTO = GetAccount();

            // Act
            var result = controller.UpdateAccount(accountDTO).GetAwaiter().GetResult();

            // Assert
            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.IsAssignableFrom<Guid>(notFoundObjectResult.Value);
        }

        [Fact]
        public void UpdateAccount_WithUpdateError_Returns_ConflictResult()
        {
            // Arrange
            var accountServiceMock = new Mock<IAccountService>();

            accountServiceMock.Setup(service => service
                .GetAccountByEmailAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(GetAccount()));

            accountServiceMock.Setup(service => service
                .UpdateAccountAsync(It.IsAny<AccountDTO>()))
                .Returns(Task.FromResult(false));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(c => c.Warning(It.IsAny<string>()));

            var controller = new AccountsController(accountServiceMock.Object, loggerMock.Object);
            var accountDTO = GetAccount();

            // Act
            var result = controller.UpdateAccount(accountDTO).GetAwaiter().GetResult();

            // Assert
            var conflictResult = Assert.IsType<ConflictResult>(result);
        }

        [Fact]
        public void UpdateAccount_WithValidModel_Returns_OkResult()
        {
            // Arrange
            var accountServiceMock = new Mock<IAccountService>();

            accountServiceMock.Setup(service => service
                .GetAccountByEmailAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(GetAccount()));

            accountServiceMock.Setup(service => service
                .UpdateAccountAsync(It.IsAny<AccountDTO>()))
                .Returns(Task.FromResult(true));

            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(c => c.Information(It.IsAny<string>()));

            var controller = new AccountsController(accountServiceMock.Object, loggerMock.Object);
            var accountDTO = GetAccount();

            // Act
            var result = controller.UpdateAccount(accountDTO).GetAwaiter().GetResult();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<AccountDTO>(okResult.Value);
        }
    }
}