using Microsoft.AspNetCore.Mvc;
using System;
using Weather.Core.API;

namespace Weather.WebAPI.ASP.NET_Core.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherServices _weatherServices;
        public WeatherController(IWeatherServices weatherServices)
        {
            _weatherServices = weatherServices;
        }

        [HttpGet]
        public IActionResult GetCities()
        {
            return Ok(_weatherServices.GetCities());
        }

        [HttpGet("{id}/{time}")]
        public IActionResult GetWeather(string id, DateTime time)
        {   
            return Ok(_weatherServices.GetTemperature(id, time));
        }
    }
}
