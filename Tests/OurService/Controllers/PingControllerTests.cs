using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;

namespace Tests.OurService.Controllers
{
    [TestFixture]
    public class PingControllerTests
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
        public async Task Ping_ShouldReturn200()
        {
            var response = await _client.GetAsync("/ping");
            Assert.That(response.IsSuccessStatusCode, Is.True);
        }

        [TearDown]
        public void TearDown()
        {
            _client?.Dispose();
        }
    }
}
