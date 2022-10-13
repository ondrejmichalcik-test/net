using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace Net.Scaffolder.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloWorldController : ControllerBase
    {
        private readonly ILogger<HelloWorldController> _logger;
        private readonly IConfiguration _configuration;

        public HelloWorldController(ILogger<HelloWorldController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public string Get()
        {
            //EXAMPLE: log
            _logger.LogTrace("Trace message");
            _logger.LogDebug("Debug message");
            _logger.LogInformation("Info message");
            _logger.LogWarning("Warning message");
            _logger.LogError("Error message");
            _logger.LogCritical("Critical message");
            
            // EXAMPLE: read the env variable directly
            var directGetEnvVar = Environment.GetEnvironmentVariable("CODENOW_EXAMPLE_ENVIRONMENT_VARIABLE");
            if(string.IsNullOrWhiteSpace(directGetEnvVar))
                _logger.LogError($"{nameof(directGetEnvVar)} is empty");
            _logger.LogInformation($"Directly read env var: {directGetEnvVar}");
            
            // EXAMPLE: use env variable inside the app settings
            var confValue = _configuration.GetValue<string>("CodeNowHelloWorldExampleUsageOfEnvVar");
            if(string.IsNullOrWhiteSpace(confValue))
                _logger.LogError($"{nameof(confValue)} is empty");
            _logger.LogInformation($"Directly read env var: {confValue}");

            return "Hello world";
        }
    }
}
