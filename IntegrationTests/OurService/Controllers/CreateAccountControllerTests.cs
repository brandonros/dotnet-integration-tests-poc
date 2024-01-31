using System.Net;
using System.Text.Json;
using NUnit.Framework;
using OurService.Model;

namespace IntegrationTests.OurService.Controllers
{
    [TestFixture]
    public class CreateAccountControllerTests
    {
        private HttpClient _client;

        [SetUp]
        public void Setup()
        {
            _client = new HttpClient
            {
                BaseAddress = new System.Uri("http://ourservice:5000")
            };
        }

        [Test]
        public async Task CreateAccount_ShouldReturn200()
        {
            // Create an instance of CreateAccountRequest with the data you want to send
            var createAccountRequest = new OurCreateAccountRequest
            {
                username = "testuser",
                email = "testuser@example.com"
            };

            // Serialize the request object to JSON
            var jsonContent = JsonSerializer.Serialize(createAccountRequest);

            // Create a StringContent with the JSON data
            var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

            // Send a POST request to your API endpoint
            var response = await _client.PostAsync("/accounts/create", content);

            // Check if the response status code is 200 OK
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            
            // TODO: check response body?
        }

        [TearDown]
        public void TearDown()
        {
            _client?.Dispose();
        }
    }
}
