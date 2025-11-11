using Microsoft.AspNetCore.Mvc;
using tech_test.Services;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http.HttpResults;

namespace tech_test.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly SoapClientService soapClientService;

        public AuthController(SoapClientService _soapClientService)
        {
            soapClientService = _soapClientService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.Username) || string.IsNullOrWhiteSpace(req.Password))
            {
                return BadRequest(new { success = false, message = "Username and password required" });
            }

            var xml = await soapClientService.LoginAsync(req.Username, req.Password);

            bool success = xml.Contains("Entity") || xml.Contains("Success");
            return Ok(new { success, raw = xml });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] object customer)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(customer);
            var xml = await soapClientService.RegisterAsync(json);
            return Ok(new { success = true, raw = xml });
        }
    }
    public record LoginRequest(string Username, string Password);
}