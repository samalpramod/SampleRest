using Login.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Security.Policy;

namespace Login.Controllers
{
    public class HomeController : Controller
    {
        private const string apiUrl = "http://localhost:5282/api/v1/User";

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] LoginModel model)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                Uri baseUri = new Uri(apiUrl);
                httpClient.BaseAddress = baseUri;
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.ConnectionClose = true;

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var authenticationString = $"{model.Username}:{model.Password}";
                var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));

                var requestMessage = new HttpRequestMessage();
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);

                var response = await httpClient.SendAsync(requestMessage);
                response.EnsureSuccessStatusCode(); // Ensure a successful response

                var responseBody = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<User>(responseBody);
                if (user != null)
                {
                    var viewHtml = await this.RenderViewAsync("UserDetails", user, true);
                    return Json(new { success = true, userdata = viewHtml });
                }
                else
                {
                    return Json(new { success = false, error = "Invalid Username or Password" });
                }
            }
            catch (HttpRequestException ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

      
    }

    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
