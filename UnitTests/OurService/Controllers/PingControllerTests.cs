using NUnit.Framework;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using OurService.Controllers;

namespace OurService.Tests.Controllers
{
    public class PingControllerTests
    {
        [Test]
        public void Ping_ShouldReturnOkResult()
        {
            // Arrange
            var controller = new PingController();

            // Act
            var result = controller.Ping();

            // Assert
            result.Should().BeOfType<OkResult>();
        }
    }
}
