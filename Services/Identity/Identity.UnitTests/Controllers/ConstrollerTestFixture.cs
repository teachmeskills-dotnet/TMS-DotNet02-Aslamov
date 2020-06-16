using Identity.API.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;

namespace Identity.UnitTests.Controllers
{
    /// <summary>
    /// Define base functions for AccountsController testing.
    /// </summary>
    public class ConstrollerTestFixture
    {
        /// <summary>
        /// Generate collection of Account DTO.
        /// </summary>
        /// <returns>Collection of Account DTO.</returns>
        public ICollection<AccountDTO> GetAllAccounts()
        {
            return new List<AccountDTO>()
            {
               new AccountDTO()
                {
                    Id = new Guid(),
                    Email = "test@gmail.com",
                    Password = "passw@rd123",
                    Username = "test",
                    Role = 1, // User
                    IsActive = true,
                },

                new AccountDTO()
                {
                    Id = new Guid(),
                    Email = "admin@gmail.com",
                    Password = "passw@rd123",
                    Username = "admin",
                    Role = 2, // Admin
                    IsActive = true,
                },
            };
        }

        /// <summary>
        /// Generate single Account DTO.
        /// </summary>
        /// <returns>Account DTO.</returns>
        public AccountDTO GetAccount()
        {
            return new AccountDTO()
            {
                Id = new Guid(),
                Email = "test@gmail.com",
                Password = "passw@rd123",
                Username = "test",
                Role = 1, // User
                IsActive = true,
            };
        }

        /// <summary>
        /// Get null Account DTO.
        /// </summary>
        /// <returns>Null object.</returns>
        public AccountDTO GetNullAccount() => null;

        /// <summary>
        /// Generate single Login DTO.
        /// </summary>
        /// <returns>Login DTO.</returns>
        public LoginDTO GetLoginData()
        {
            return new LoginDTO()
            {
                Email = "test@gmail.com",
                Password = "passw@rd123",
            };
        }

        /// <summary>
        /// Generate single Application Token.
        /// </summary>
        /// <returns>Token DTO.</returns>
        public TokenDTO GetToken()
        {
            return new TokenDTO()
            {
                Username = "admin",
                Role = "Admin",
                Token = "12345678900987654321"
            };
        }

        /// <summary>
        /// Get null Token DTO.
        /// </summary>
        /// <returns>Null object.</returns>
        public TokenDTO GetNullToken() => null;

        /// <summary>
        /// Get fake controller context.
        /// </summary>
        /// <returns></returns>
        public ControllerContext GetFakeContext()
        {
            var responseMock = new Mock<HttpResponse>();
            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.SetupGet(a => a.Response).Returns(responseMock.Object);

            var context = new ControllerContext { HttpContext = httpContextMock.Object };

            return context;
        }
    }
}