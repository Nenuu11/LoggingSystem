using DistributedLoggingSystem.API.Controllers.V1;
using DistributedLoggingSystem.API.JWT;
using DistributedLoggingSystem.API.Models;
using DistributedLoggingSystem.BLL.DTOs;
using DistributedLoggingSystem.BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace DistributedLoggingSystem.Test
{
    public class LogsControllerTests
    {
        private readonly Mock<ILoggerService> _mockLoggerService;
        private readonly Mock<IJWTFactory> _mockJwtFactory;
        private readonly LogsController _controller;

        public LogsControllerTests()
        {
            _mockLoggerService = new Mock<ILoggerService>();
            _mockJwtFactory = new Mock<IJWTFactory>();
            _controller = new LogsController(_mockLoggerService.Object, _mockJwtFactory.Object);

            // Mock the User (ClaimsPrincipal) for authorization
            var claims = new[] { new Claim("exp", DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds().ToString()) };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = claimsPrincipal }
            };
        }

        [Fact]
        public async Task AddLogs_ValidToken_ReturnsOkResponse()
        {
            // Arrange
            var logDto = new LogDTO { Message = "Test log", Level = "Info", Service = "TestService" };
            _mockJwtFactory.Setup(x => x.IsValidToken(It.IsAny<long>())).Returns(true);
            _mockLoggerService.Setup(x => x.LogAsync(It.IsAny<LogDTO>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.AddLogs(logDto);

            // Assert
            var apiResponse = Assert.IsType<APIResponse>(result);
            Assert.Equal((int)HttpStatusCode.OK, apiResponse.StatusCode);
            Assert.Equal("Log written to all enabled backends.", apiResponse.Data);
        }

        [Fact]
        public async Task AddLogs_ExpiredToken_ReturnsUnauthorizedResponse()
        {
            // Arrange
            var logDto = new LogDTO { Message = "Test log", Level = "Info", Service = "TestService" };
            _mockJwtFactory.Setup(x => x.IsValidToken(It.IsAny<long>())).Returns(false);

            // Act
            var result = await _controller.AddLogs(logDto);

            // Assert
            var apiResponse = Assert.IsType<APIResponse>(result);
            Assert.Equal((int)HttpStatusCode.Unauthorized, apiResponse.StatusCode);
            Assert.Equal("token is expired, please generate another token", apiResponse.Data);
        }

        [Fact]
        public async Task GetLogs_ValidToken_ReturnsOkResponse()
        {
            // Arrange
            var logs = new List<LogDTO> { new LogDTO { Message = "Test log", Level = "Info", Service = "TestService" } };
            _mockJwtFactory.Setup(x => x.IsValidToken(It.IsAny<long>())).Returns(true);
            _mockLoggerService.Setup(x => x.GetLogs(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>()))
                .ReturnsAsync(logs);

            // Act
            var result = await _controller.GetLogs("TestService", "Info", null, null);

            // Assert
            var apiResponse = Assert.IsType<APIResponse>(result);
            Assert.Equal((int)HttpStatusCode.OK, apiResponse.StatusCode);
            Assert.Equal(logs, apiResponse.Data);
        }

        [Fact]
        public async Task GetLogs_ExpiredToken_ReturnsUnauthorizedResponse()
        {
            // Arrange
            _mockJwtFactory.Setup(x => x.IsValidToken(It.IsAny<long>())).Returns(false);

            // Act
            var result = await _controller.GetLogs("TestService", "Info", null, null);

            // Assert
            var apiResponse = Assert.IsType<APIResponse>(result);
            Assert.Equal((int)HttpStatusCode.Unauthorized, apiResponse.StatusCode);
            Assert.Equal("token is expired, please generate another token", apiResponse.Data);
        }

        [Fact]
        public async Task GetLog_ValidToken_ReturnsOkResponse()
        {
            // Arrange
            var log = new LogDTO { Message = "Test log", Level = "Info", Service = "TestService" };
            _mockJwtFactory.Setup(x => x.IsValidToken(It.IsAny<long>())).Returns(true);
            _mockLoggerService.Setup(x => x.GetLog(It.IsAny<int>())).ReturnsAsync(log);

            // Act
            var result = await _controller.GetLog(1);

            // Assert
            var apiResponse = Assert.IsType<APIResponse>(result);
            Assert.Equal((int)HttpStatusCode.OK, apiResponse.StatusCode);
            Assert.Equal(log, apiResponse.Data);
        }

        [Fact]
        public async Task GetLog_ExpiredToken_ReturnsUnauthorizedResponse()
        {
            // Arrange
            _mockJwtFactory.Setup(x => x.IsValidToken(It.IsAny<long>())).Returns(false);

            // Act
            var result = await _controller.GetLog(1);

            // Assert
            var apiResponse = Assert.IsType<APIResponse>(result);
            Assert.Equal((int)HttpStatusCode.Unauthorized, apiResponse.StatusCode);
            Assert.Equal("token is expired, please generate another token", apiResponse.Data);
        }
    }
}