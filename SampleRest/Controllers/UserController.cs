using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SampleRest.Entities;
using System.Net.Http.Headers;

namespace SampleRest.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private const string apiUrl = "https://randomuser.me/api/";


        [HttpGet]
        [Authorize]
        [Produces("application/json")]
        public async Task<IActionResult> Get()
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode(); // Ensure a successful response

                var responseBody = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<RandomUserAPIResult>(responseBody);
                if (apiResponse != null && apiResponse.results.Count > 0)
                {
                    var user = apiResponse.results[0];
                    return Ok(user);
                }
                else
                {
                    return StatusCode(404, $"No User Available");
                }
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"An error occurred while fetching user data: {ex.Message}");
            }
        }

    }
}
