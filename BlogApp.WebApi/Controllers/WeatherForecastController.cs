using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Entity;
using BlogApp.Repository.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BlogApp.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        private IBlogRepository _repository;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IBlogRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public List<Blog> Get()
        {
            return _repository.Blogs.ToList();
        }
    }
}
