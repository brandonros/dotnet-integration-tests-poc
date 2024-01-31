using NUnit.Framework;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using OurService.Controllers;
using OurService.Model;
using System;
using System.Net.Http;
using Moq;
using RichardSzalay.MockHttp;

namespace OurService.Tests.Controllers
{
    public class MockHttpClientFactory : IHttpClientFactory
    {
        private readonly MockHttpMessageHandler _mockHttpMessageHandler;

        public MockHttpClientFactory(MockHttpMessageHandler mockHttpMessageHandler)
        {
            _mockHttpMessageHandler = mockHttpMessageHandler ?? throw new ArgumentNullException(nameof(mockHttpMessageHandler));
        }

        public System.Net.Http.HttpClient CreateClient(string name)
        {
            return _mockHttpMessageHandler.ToHttpClient();
        }
    }

    [TestFixture]
    public class CreateAccountControllerTests
    {
        private Mock<IHttpClientFactory> _mockHttpClientFactory;
        private HttpClient _client;
        private MockHttpMessageHandler _mockHttp;
        private CreateAccountController _controller;

        [SetUp]
        public void Setup()
        {
            _mockHttp = new MockHttpMessageHandler();

            // Mocking a specific URL and ensuring it matches the controller call
            _mockHttp.When("https://ourvendor.com/accounts/create")
                    .Respond("application/json", "{\"accountId\":\"12345\"}"); // Mock response

            // Injecting the mocked HttpClient into the controller
            _controller = new CreateAccountController(new MockHttpClientFactory(_mockHttp));
        }

        [Test]
        public async Task ShouldReturnOkResult()
        {
            // Arrange
            var request = new OurCreateAccountRequest { username = "test", email = "test@example.com" };

            // Act
            var result = await _controller.CreateAccount(request);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [TearDown]
        public void TearDown()
        {
            _client?.Dispose();
        }
    }
}
