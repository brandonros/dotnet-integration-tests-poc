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
            // translate our request to vendor request
            var vendorRequest = new VendorCreateAccountRequest {
                username = ourRequest.username,
                email = ourRequest.email
            };
            // serialize
            var jsonContent = JsonSerializer.Serialize(vendorRequest);
            var stringifiedRequestBody = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            // send to vendor
            var response = await _httpClient.PostAsync($"{_vendorUrl}/accounts/create", stringifiedRequestBody);
            // check response status
            if (response.StatusCode != HttpStatusCode.OK) {
                return StatusCode((int)response.StatusCode, "Invalid response code");
            }
            // get response body
            var responseContent = await response.Content.ReadAsStringAsync();
            // deserialize to VendorCreateAccountResponse
            var vendorCreateAccountResponse = JsonSerializer.Deserialize<VendorCreateAccountResponse>(responseContent);
            if (vendorCreateAccountResponse == null) {
                throw new Exception("Failed to deserialize response");
            }
            var ourResponse = new OurCreateAccountResponse {
                accountId = vendorCreateAccountResponse.accountId
            };
            return Ok(ourResponse);
        }
    }
}
