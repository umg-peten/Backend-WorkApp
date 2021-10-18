using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkApp.IServices;

namespace WorkApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : Controller
    {

        private readonly ITokenHandler _token;

        public WeatherForecastController(ITokenHandler token, ILogger<WeatherForecastController> logger)
        {
            this._token = token;
            _logger = logger;
        }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;



        [HttpGet]
        public IActionResult Get()
        {
            var rng = new Random();
            string token = _token.GenerateToken("123");
            return Ok(token);

        }
    }
}
