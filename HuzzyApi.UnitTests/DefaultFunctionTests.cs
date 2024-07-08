using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace HuzzyApi.UnitTests
{
    public class DefaultFunctionTests
    {
        private readonly Mock<ILogger<DefaultFunction>> _loggerMock;
        private readonly DefaultFunction _defaultFunction;

        public DefaultFunctionTests()
        {
            _loggerMock = new Mock<ILogger<DefaultFunction>>();
            _defaultFunction = new DefaultFunction(_loggerMock.Object);
        }

        [Fact]
        public async Task RunAsync_ReturnsOkObjectResult()
        {
            // Arrange
            var httpRequestMock = new Mock<HttpRequest>();

            // Act
            var result = await _defaultFunction.RunAsync(httpRequestMock.Object);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task RunAsync_ReturnsWelcomeMessage()
        {
            // Arrange
            var httpRequestMock = new Mock<HttpRequest>();

            // Act
            var result = await _defaultFunction.RunAsync(httpRequestMock.Object);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Welcome to Azure Functions!", okResult.Value);
        }

        [Fact]
        public async Task RunAsync_LogsInformation()
        {
            // Arrange
            var httpRequestMock = new Mock<HttpRequest>();

            // Act
            await _defaultFunction.RunAsync(httpRequestMock.Object);

            // Assert
            _loggerMock.Verify(l =>
            l.Log(LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, _) => v.ToString().Contains("C# HTTP trigger function processed a request.")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()
            ), Times.Once);
        }
    }
}
