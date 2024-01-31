using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using OurService.Model;
using System.Net;

namespace OurService.Controllers
{
    [ApiController]
    [Route("accounts")]
    public class CreateAccountController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly string _vendorUrl;

        public CreateAccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _vendorUrl = Environment.GetEnvironmentVariable("VENDOR_URL") ?? "https://ourvendor.com";
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateAccount([FromBody] OurCreateAccountRequest ourRequest)
        {
            // Translate our request to vendor request
            var vendorRequest = new VendorCreateAccountRequest
            {
                username = ourRequest.username,
                email = ourRequest.email
            };

            // Serialize request body
            var stringifiedRequestBody = JsonSerializer.Serialize(vendorRequest);
            var content = new StringContent(stringifiedRequestBody, Encoding.UTF8, "application/json");

            // Create HttpRequestMessage
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{_vendorUrl}/accounts/create"),
                Content = content
            };

            // Send to vendor
            var response = await _httpClient.SendAsync(httpRequestMessage);

            // Check response status
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return StatusCode((int)response.StatusCode, "Invalid response code");
            }

            // Get response body
            var responseContent = await response.Content.ReadAsStringAsync();

            // Deserialize to VendorCreateAccountResponse
            var vendorCreateAccountResponse = JsonSerializer.Deserialize<VendorCreateAccountResponse>(responseContent);
            if (vendorCreateAccountResponse == null)
            {
                throw new Exception("Failed to deserialize response");
            }

            // Translate to OurCreateAccountResponse
            var ourResponse = new OurCreateAccountResponse
            {
                accountId = vendorCreateAccountResponse.accountId
            };

            return Ok(ourResponse);
        }

    }
}
